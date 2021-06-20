using Dapper;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface;
using MISA.ApplicationCore.Interface.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MISA.ApplicationCore.Service
{
    public class StoreService : BaseService<Store>, IStoreService
    {
        #region Constructor
        IBaseRepository<Store> _baseRepository;
        public StoreService(IBaseRepository<Store> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }
        #endregion

        #region Method
        /// <summary>
        /// Hàm thêm mới 1 store
        /// </summary>
        /// <param name="store">Thông tin store</param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        /// CreatedBy: LVDat (12/06/2021)
        public override ActionServiceResult AddEntity(Store store)
        {
            // Kiểm tra các trường đã validate đúng chưa
            // Đúng: fields == null
            // Sai : fields != null
            var fields = this.BaseValidate(store);
            if (fields.Count != 0)
            {
                return new ActionServiceResult()
                {
                    Success = false,
                    MISAcode = Enumeration.MISAcode.Validate,
                    Message = "Không đúng định dạng !!",
                    FieldNotValids = fields,
                    Data = -1
                };
            }
            else
            {
                // Kiểm tra trùng mã
                var storeDuplicate = GetStoreByStoreCode(store.StoreCode);
                if (storeDuplicate != null)
                {
                    return new ActionServiceResult()
                    {
                        Success = false,
                        MISAcode = Enumeration.MISAcode.Validate,
                        Message = "Mã cửa hàng đã tồn tại trong hệ thống !!!",
                        Data = -1
                    };
                }
                else
                {
                    return base.AddEntity(store);
                }
            }
        }
        /// <summary>
        /// Phân trang danh sách cửa hàng
        /// </summary>
        /// <param name="pageIndex">Ví trí trang</param>
        /// <param name="pageSize">Số bản ghi/trang</param>
        /// <param name="filter">Thông tin tìm kiếm (nếu có)</param>
        /// <returns>Danh sách cửa hàng</returns>
        /// CreatedBy: LVDat (12/06/2021)
        public override ActionServiceResult GetEntities(int pageIndex, int pageSize, string filter)
        {
            if (filter == null || filter == string.Empty)
            {
                filter = "";
            }
            // Lấy ra số lượng bản ghi
            var paramQuality = new DynamicParameters();
            paramQuality.Add("@Filter", filter);
            var quality = _baseRepository.GetDataPaging($"Proc_GetData{_tableName}Paging", paramQuality, commandType: CommandType.StoredProcedure);
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
                Data = _baseRepository.Get($"Proc_GetStorePaging", param, commandType: CommandType.StoredProcedure)
            };
        }
        public override ActionServiceResult UpdateEntity(Store store)
        {
            var isValid = base.BaseValidate(store);
            if (isValid.Count > 0)
            {
                return new ActionServiceResult()
                {
                    Success = false,
                    MISAcode = Enumeration.MISAcode.Validate,
                    Message = "Sai định dạng !!!",
                    FieldNotValids = isValid,
                    Data = -1
                };
            }
            else
            {
                // Kiểm tra có sửa id  cửa hàng không
                var storeOld = (List<Store>)base.GetEntityById(store.StoreId).Data;
                if (storeOld?.Count == 0)
                {
                    return new ActionServiceResult()
                    {
                        Success = false,
                        MISAcode = Enumeration.MISAcode.Validate,
                        Message = "Id của cửa hàng không tồn tại trong hệ thống !!!",
                        Data = -1
                    };
                }
                else
                {
                    // Kiểm tra xem có sửa mã của sửa hàng không
                    // Nếu sửa thì check xem mã mới đã tồn tại chưa
                    var storeCodeOld = storeOld[0].StoreCode;
                    if (store.StoreCode == storeCodeOld)
                    {
                        return base.UpdateEntity(store);
                    }
                    else
                    {
                        // Kiểm tra mã cửa hàng
                        var customerNew = GetStoreByStoreCode(store.StoreCode);
                        if (customerNew != null)
                            return new ActionServiceResult()
                            {
                                Success = false,
                                MISAcode = Enumeration.MISAcode.Validate,
                                Message = "Mã cửa hàng đã tồn tại trong hệ thống !!!",
                                Data = -1
                            };
                    }
                }
                return base.UpdateEntity(store);
            }
        }
        /// <summary>
        /// Tìm kiếm cửa hàng theo mã cửa hàng
        /// </summary>
        /// <param name="storeCode">Mã cửa hàng</param>
        /// <returns>1 cửa hàng hoặc null</returns>
        /// CreatedBy: LVDat (12/06/2021)
        public Store GetStoreByStoreCode(string storeCode)
        {
            var param = new DynamicParameters();
            param.Add("@StoreCode", storeCode);

            var customers = _baseRepository.Get($"Proc_GetStoreByStoreCode", param, commandType: CommandType.StoredProcedure).ToList();
            if (customers.Count == 0)
            {
                return null;
            }
            return customers[0];
        }
        /// <summary>
        /// Hàm lấy ra danh sách store theo các tiêu chí
        /// Lấy ra 1 danh sách các phần tử theo 1 dữ liệu filter đầu tiên
        /// Sau đó lọc qua các filter còn lại 
        /// </summary>
        /// <param name="listFilter"></param>
        /// <param name="listOption"></param>
        /// <returns>1 danh sách store theo filter</returns>
        public ActionServiceResult GetStoreByFilter(string storeCode, string storeName, string address, string phoneNumber, int? status,
             int pageIndex, int pageSize)
        { 
            if(status == 3)
            {
                status = null;
            }
            var param = new DynamicParameters();
            param.Add("@StoreCode", storeCode);
            param.Add("@StoreName", storeName);
            param.Add("@Address", address);
            param.Add("@PhoneNumber", phoneNumber);
            param.Add("@Status", status);
            param.Add("@PageIndex", pageIndex);
            param.Add("@PageSize", pageSize);
            var totalRecord = _baseRepository.GetDataPaging($"Proc_GetDataStore", param, commandType: CommandType.StoredProcedure);
            var totalPage = (int)Math.Ceiling(Convert.ToDouble(totalRecord / pageSize)) + 1;
      
            var result = _baseRepository.Get($"Proc_GetStoreFilter", param, commandType: CommandType.StoredProcedure);
            return new ActionServiceResult()
            {
                Success = true,
                Message = "Lấy dữ liệu thành công !!!",
                Data = result,
                TotalRecord = totalRecord,
                TotalPage = totalPage
            };
        }

        public ActionServiceResult GetStoreFilter(ObjectFilter objectFilter)
        {
            var param = new DynamicParameters();
            return null;
        }
        #endregion

    }
}
