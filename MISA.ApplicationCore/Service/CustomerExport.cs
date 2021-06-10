using Dapper;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface;
using MISA.ApplicationCore.Interface.Service;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MISA.ApplicationCore.Service
{
    public class CustomerExport: ICustomerExport
    {
        #region Constructor
        IBaseRepository<Customer> _baseRepository;
        protected string _tableName;
        public CustomerExport(IBaseRepository<Customer> baseRepository)
        {
            _baseRepository = baseRepository;
            _tableName = "Customer";
        }
        #endregion
        #region Method
        /// <summary>
        /// Tạo file excel
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private Stream CreateExcelFile(Stream stream = null, int pageIndex= 1, int pageSize = 60, string filter = "")
        {
            using (var excelPackage = new ExcelPackage(stream ?? new MemoryStream()))
            {
                var list = GetCustomers(pageIndex, pageSize, filter);
                // Tạo author cho file Excel
                excelPackage.Workbook.Properties.Author = "Hanker";
                // Tạo title cho file Excel
                excelPackage.Workbook.Properties.Title = "EPP test background";
                // thêm tí comments vào làm màu 
                excelPackage.Workbook.Properties.Comments = "This is my fucking generated Comments";
                // Add Sheet vào file Excel
                excelPackage.Workbook.Worksheets.Add("First Sheet");
                // Lấy Sheet bạn vừa mới tạo ra để thao tác 
                var workSheet = excelPackage.Workbook.Worksheets[1];
                // Đổ data vào Excel file
                workSheet.Cells[1, 1].LoadFromCollection(list, true, TableStyles.Dark9);
                // BindingFormatForExcel(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }
        /// <summary>
        /// hầm lấy dữ liệu
        /// </summary>
        /// <param name="pageIndex">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi / trang</param>
        /// <param name="filter">Thông tin tìm kiếm ( nếu có)</param>
        /// <returns>Danh sách các phần tử</returns>
        public List<Customer> GetCustomers(int pageIndex, int pageSize, string filter)
        {
            var param = new DynamicParameters();
            param.Add("@PageIndex", pageIndex);
            param.Add("@PageSize", pageSize);
            param.Add("@Filter", filter);
            var customers = _baseRepository.Get($"GetCustomer", param, commandType: System.Data.CommandType.StoredProcedure);
            return (List<Customer>)customers;
        }

        public ActionServiceResult ExportData(int pageIndex, int pageSize, string filter)
        {
            //// Gọi lại hàm để tạo file excel
            //var stream = CreateExcelFile();
            //// Tạo buffer memory strean để hứng file excel
            //var buffer = stream as MemoryStream;
            //BinaryWrite(buffer.ToArray());
            //// Send tất cả ouput bytes về phía clients
            //Response.Flush();
            //Response.End();
            return null;
        }
        #endregion
    }
}
