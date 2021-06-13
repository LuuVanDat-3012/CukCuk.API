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
                    data = -1
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
                        Message = "Mã khách hàng đã tồn tại !!!",
                        data = -1
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
                data = _baseRepository.Get($"Proc_GetStorePaging", param, commandType: CommandType.StoredProcedure)
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
                    data = -1
                };
            }
            else
            {
                // Kiểm tra có sửa id  cửa hàng không
                var storeOld = (List<Customer>)base.GetEntityById(store.StoreId).data;
                if (storeOld?.Count == 0)
                {
                    return new ActionServiceResult()
                    {
                        Success = false,
                        MISAcode = Enumeration.MISAcode.Validate,
                        Message = "Id của cửa hàng không tồn tại trong hệ thống !!!",
                        data = -1
                    };
                }
                else
                {
                    // Kiểm tra xem có sửa mã của sửa hàng không
                    // Nếu sửa thì check xem mã mới đã tồn tại chưa
                    var customerCodeOld = storeOld[0].CustomerCode;
                    if (store.StoreCode == customerCodeOld)
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
                                data = -1
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
        #endregion

    }
}
