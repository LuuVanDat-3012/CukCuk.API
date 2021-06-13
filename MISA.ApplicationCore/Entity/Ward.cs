using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entity
{
    /// <summary>
    /// Lớp mô tả thông tin xã/phường
    /// </summary>
    /// CreatedBy: LVDat (11/06/2021)
    public class Ward
    {
        /// <summary>
        /// Mã xã/phường
        /// </summary>
        public Guid WardId { get; set; }
        /// <summary>
        /// Tên xã/phường
        /// </summary>
        public string WardName { get; set; }
        /// <summary>
        /// Mã quận huyện
        /// </summary>
        public Guid DistrictId { get; set; }
    }
}
