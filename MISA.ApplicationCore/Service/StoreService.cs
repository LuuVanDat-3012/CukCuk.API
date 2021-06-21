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
        /// Hàm sửa thông tin 1 sửa hàng
        /// </summary>
        /// <param name="store">Cửa hàng cần sửa</param>
        /// <returns> Số dòng bị ảnh hưởng</returns>
        public override ActionServiceResult UpdateEntity(Store store)
        {
            // Kiểm tra validate hợp lệ chưa
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
        public ActionServiceResult GetStoreFilter(ObjectFilter objectFilter)
        {
            var param = this.MappingDataType<ObjectFilter>(objectFilter);
            param.Add("TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var result = _baseRepository.Get($"Proc_FilterStore", param, commandType: CommandType.StoredProcedure);
            var totalRecord = param.Get<int>("TotalRecord");
            var toltalPage = (int)Math.Ceiling(Convert.ToDouble(totalRecord / objectFilter.PageSize)) + 1 ;
            return new ActionServiceResult()
            {
                Success = true,
                MISAcode = Enumeration.MISAcode.Success,
                Message = "Lấy dữ liệu thành công !!!",
                Data = result,
                TotalPage = toltalPage,
                TotalRecord = totalRecord
            };

        }
        #endregion

    }
}
