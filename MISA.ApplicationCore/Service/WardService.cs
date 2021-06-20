using Dapper;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface;
using MISA.ApplicationCore.Interface.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Service
{
    public class WardService : BaseService<Ward>, IWardService
    {
        IBaseRepository<Ward> _baseRepository;
        public WardService(IBaseRepository<Ward> baseRepository): base(baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public ActionServiceResult GetWardByDistrict(Guid districtId)
        {
            var param = new DynamicParameters();
            param.Add("@DistrictId", districtId.ToString());
            var result = _baseRepository.Get($"Proc_GetWardWithDistrict", param, commandType: System.Data.CommandType.StoredProcedure);
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
