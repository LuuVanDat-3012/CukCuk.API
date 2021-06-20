using Dapper;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface;
using MISA.ApplicationCore.Interface.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Service
{
    public class ProvinceService: BaseService<Province>, IProvinceService
    {
        public IBaseRepository<Province> _baseRepository;
        public ProvinceService(IBaseRepository<Province> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public ActionServiceResult GetProvinceByCountry(Guid countryId)
        {
            var param = new DynamicParameters();
            param.Add("@CountryId", countryId.ToString());
            var result = _baseRepository.Get($"Proc_GetProvinceWithCountry", param, commandType: System.Data.CommandType.StoredProcedure);
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
