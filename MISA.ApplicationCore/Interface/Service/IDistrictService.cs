using MISA.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interface.Service
{
    public interface IDistrictService: IBaseService<District>
    {
        /// <summary>
        /// Lấy ra danh sách quận/huyện theo mã tỉnh/thành phố
        /// </summary>
        /// <param name="provinceId">Mã tỉnh thành</param>
        /// <returns>1 ActionServiceResult bao gồm danh sách quận/huyện (data)</returns>
        /// CratedBy: LVDat (15/06/2021)
        ActionServiceResult GetDistrictByProvince(Guid provinceId);
    }
}
