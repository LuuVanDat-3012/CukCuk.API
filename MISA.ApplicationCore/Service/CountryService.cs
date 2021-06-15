using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface;
using MISA.ApplicationCore.Interface.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Service
{
    public class CountryService : BaseService<Country>, ICountryService
    {
        public IBaseRepository<Country> _baseRepository;
        public CountryService(IBaseRepository<Country> baseRepository): base(baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public ActionServiceResult GetCounryByStore(Guid storeId)
        {
            return null;
        }
    }
}
