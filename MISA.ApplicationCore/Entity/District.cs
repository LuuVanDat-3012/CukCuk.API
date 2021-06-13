using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entity
{
    /// <summary>
    /// Lớp mô tả thông tin quận huyện
    /// </summary>
    /// CreatedBy: LVDat (11/06/2021)
    public class District
    {
        /// <summary>
        /// Mã quận huyện
        /// </summary>
        public Guid DistrictId { get; set; }
        /// <summary>
        /// Tên quận huyện
        /// </summary>
        public string DistrictName { get; set; }
        /// <summary>
        /// Mã tỉnh thành
        /// </summary>
        public Guid ProvinceId { get; set; }
    }
}
