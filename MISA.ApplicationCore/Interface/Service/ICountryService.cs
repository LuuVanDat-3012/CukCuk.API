using MISA.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interface.Service
{
    public interface ICountryService : IBaseService<Country>
    {
        /// <summary>
        /// Lấy ra  quốc gia theo mã cửa hàng
        /// </summary>
        /// <param name="storeId">Mã cửa hàng</param>
        /// <returns>1 ActionServiceResult bao gồm thông tin quốc gia (data)</returns>
        /// CratedBy: LVDat (15/06/2021)
        ActionServiceResult GetCounryByStore(Guid storeId);
    }
}
