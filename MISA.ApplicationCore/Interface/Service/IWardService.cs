using MISA.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interface.Service
{
    public interface IWardService: IBaseService<Ward>
    {
        /// <summary>
        /// Lấy ra 1 danh sách xã phường theo quận/huyện
        /// </summary>
        /// <param name="districtId">Mã quận/huyện</param>
        /// <returns>1 ActionServiceResult bao gồm 1 danh sách xã/phường (data)</returns>
        /// CratedBy: LVDat (15/06/2021)
        ActionServiceResult GetWardByDistrict(Guid districtId);
    }
}
