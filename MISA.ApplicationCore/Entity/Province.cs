using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entity
{
    /// <summary>
    /// Lớp mô tả thông tin tỉnh thành
    /// </summary>
    /// CreateBy: LVDat (11/6/2021)
    public class Province: BaseEntity
    {
        /// <summary>
        /// Mã tỉnh thành
        /// </summary>
        public Guid ProvinceId { get; set; }
        /// <summary>
        /// Tên tỉnh thành
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// Mã quốc gia
        /// </summary>
        public Guid CountryId { get; set; }
    }
}
