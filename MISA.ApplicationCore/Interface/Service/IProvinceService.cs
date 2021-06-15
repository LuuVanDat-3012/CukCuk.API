using MISA.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interface.Service
{
   public  interface IProvinceService: IBaseService<Province>
    {
        /// <summary>
        /// Lấy ra danh sách tỉnh/thành phố theo mã quốc gia
        /// </summary>
        /// <param name="countryId">MÃ quốc gia</param>
        /// <returns>1 ActionServiceResult bao gồm 1 danh sách tỉnh thành (data)</returns>
        /// <returns>1 ActionServiceResult bao gồm thông tin quốc gia (data)</returns>
        /// CratedBy: LVDat (15/06/2021)
        ActionServiceResult GetProvinceByCountry(Guid countryId);
    }
}
