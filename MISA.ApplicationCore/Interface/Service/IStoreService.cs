using MISA.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interface.Service
{
   public interface IStoreService : IBaseService<Store>
    {
        /// <summary>
        /// Lấy ra 1 cửa hàng theo mã
        /// </summary>
        /// <param name="storeCode">MÃ cửa hàng</param>
        /// <returns>1 cửa hàng được tìm thấy hoặc null</returns>
        /// CreatedBy: LVDat (19/06/2021)
        Store GetStoreByStoreCode(string storeCode);

        /// <summary>
        /// Lấy ra 1 danh sách cửa hàng theo các yêu cầu được truyền vào
        /// </summary>
        /// <param name="objectFilter">1 đối tượng filter</param>
        /// <returns> Danh sách cửa hàng theo các yêu cầu được truyền vào</returns>
        /// CreatedBy: LVDat (19/06/2021)
        ActionServiceResult GetStoreFilter(ObjectFilter objectFilter);
    }
}
