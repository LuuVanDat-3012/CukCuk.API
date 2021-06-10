using MISA.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interface
{
    public interface ICustomerService : IBaseService<Customer>
    {
        /// <summary>
        /// Lấy khách hàng theo mã (code)
        /// </summary>
        /// <param name="customerCode">Mã khách hàng</param>
        /// <returns> 1 khách hàng</returns>
        ///  CreatedBy: LVDat (03/5/2021)
        Customer GetCustomerByCode(string customerCode);
        /// <summary>
        /// Lây data và lưu vào file excel
        /// </summary>
        /// <returns></returns>
        ActionServiceResult ExportToExcel();
    }
}
