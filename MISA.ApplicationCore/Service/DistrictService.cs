using Dapper;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface;
using MISA.ApplicationCore.Interface.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Service
{
    public class DistrictService : BaseService<District>, IDistrictService
    {
        IBaseRepository<District> _baseRepository;
        public DistrictService(IBaseRepository<District> baseRepository): base(baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public ActionServiceResult GetDistrictByProvince(Guid provinceId)
        {

            var param = new DynamicParameters();
            param.Add("@Id", provinceId.ToString());
            var result = _baseRepository.Get($"Proc_GetDistrictWithProvince", param, commandType: System.Data.CommandType.StoredProcedure);
            return new ActionServiceResult()
            {
                Success = true,
                Message = "Lấy dữ liệu thành công",
                MISAcode = Enumeration.MISAcode.Success,
                Data = result
            };
        }
    }
}
