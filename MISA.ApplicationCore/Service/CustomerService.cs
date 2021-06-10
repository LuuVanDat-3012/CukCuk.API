using Dapper;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface;
using MISA.ApplicationCore.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows;
using System;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace MISA.ApplicationCore
{
    public class CustomerService : BaseService<Customer>, ICustomerService

    {
        IBaseRepository<Customer> _baseRepository;

        #region Constructor
        public CustomerService(IBaseRepository<Customer> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }
        #endregion
        #region method
        /// <summary>
        /// Hàm thêm mới khách hàng vào csdl
        /// -1 lỗi validate
        /// -2 trùng mã
        ///  1 thành công
        /// </summary>
        /// <param name="customer">Thông tin thêm mới</param>
        /// <returns></returns>
        public override ActionServiceResult AddEntity(Customer customer)
        {

            var fields = this.BaseValidate(customer);
            if (fields.Count != 0)
            {
                return new ActionServiceResult()
                {
                    Success = false,
                    MISAcode = Enumeration.MISAcode.Validate,
                    Message = "Không đúng định dạng !!",
                    FieldNotValids = fields,
                    data = -1
                };
            }
            else
            {
                //validate thông tin
                var isvalid = true;
                //validate thông tin
                var customerDuplicate = GetCustomerByCode(customer.CustomerCode);
                if (customerDuplicate != null)
                {
                    return new ActionServiceResult()
                    {
                        Success = false,
                        MISAcode = Enumeration.MISAcode.Validate,
                        Message = "Mã khách hàng đã tồn tại !!!",
                        data = -1
                    };
                }
                else { 
                    return base.AddEntity(customer);
                }
            }
        }


        public override ActionServiceResult UpdateEntity(Customer customer)
        {
            var customerDuplicate = GetCustomerByCode(customer.CustomerCode);
            if(customerDuplicate == null)
            {
                return base.UpdateEntity(customer);
            }
            else
            {
                return new ActionServiceResult()
                {
                    Success = false,
                    MISAcode = Enumeration.MISAcode.Validate,
                    Message = "Mã khách hàng đã tồn tại !!!",
                    data = -1
                };
            }
            
        }

     

        public Customer GetCustomerByCode(string customerCode)
        {
            var param = new DynamicParameters();
            param.Add("@customerCode", customerCode);
            var customers = _baseRepository.Get($"Proc_GetCustomerByCode", param, commandType: CommandType.StoredProcedure).ToList();
            if(customers.Count == 0)
            {
                return null;
            }
            return customers[0];
        }

        public override ActionServiceResult GetEntities(int pageIndex, int pageSize, string filter)
        {
            if (filter == null || filter == string.Empty)
            {
                filter = "";
            }
            // Lấy ra số lượng bản ghi
            var paramQuality = new DynamicParameters();
            paramQuality.Add("@value", filter);
            var quality = _baseRepository.GetQuality($"Proc_GetQuality{_tableName}", paramQuality, commandType: CommandType.StoredProcedure);
            // Lấy ra số trang
            var totalPage = Math.Ceiling(Convert.ToDouble(quality) / 30);
            var param = new DynamicParameters();
            param.Add("@PageIndex", pageIndex);
            param.Add("@PageSize", pageSize);
            param.Add("@Filter", filter);
            return new ActionServiceResult()
            {
                Message = "Lấy dữ liệu thành công",
                Success = true,
                MISAcode = Enumeration.MISAcode.Success,
                TotalPage = totalPage,
                PageNum = pageIndex,
                data = _baseRepository.Get($"Proc_GetCustomerPaging", param, commandType: CommandType.StoredProcedure)
            };
            return base.GetEntities(pageIndex, pageSize, filter);
        }

        public ActionServiceResult ExportToExcel()
        {
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                return null;
            }


            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Cells[1, 1] = "ID";
            xlWorkSheet.Cells[1, 2] = "Name";
            xlWorkSheet.Cells[2, 1] = "1";
            xlWorkSheet.Cells[2, 2] = "One";
            xlWorkSheet.Cells[3, 1] = "2";
            xlWorkSheet.Cells[3, 2] = "Two";



            xlWorkBook.SaveAs("C:\\Users\\lvdat\\Documents\\lvdat.xlsx", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            return new ActionServiceResult()
            {
            };
        }




        #endregion
    }
}
