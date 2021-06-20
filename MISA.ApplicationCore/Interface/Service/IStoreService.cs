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
        Store GetStoreByStoreCode(string storeCode);

        ActionServiceResult GetStoreByFilter( string storeCode,  string storeName,  string address,  string phoneNumber,  int? status,
             int pageIndex,  int pageSize);
        ActionServiceResult GetStoreFilter(ObjectFilter objectFilter);
    }
}
