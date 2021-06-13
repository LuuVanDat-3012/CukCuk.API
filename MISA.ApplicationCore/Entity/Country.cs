using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entity
{
    /// <summary>
    /// Lớp thông tin quốc gia
    /// </summary>
    /// CreateBy: LVDat (11/6/2021)
    public class Country: BaseEntity
    {
        /// <summary>
        /// Mã quốc gia
        /// </summary>
        public Guid CountryId { get; set; }
        /// <summary>
        /// Tên quốc gia
        /// </summary>
        public string CountryName { get; set; }
    }
}
