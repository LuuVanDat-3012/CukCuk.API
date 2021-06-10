using System;
using System.Collections.Generic;
using System.Text;
using static MISA.ApplicationCore.Entity.Enumeration;

namespace MISA.ApplicationCore.Entity
{
    public class BaseEntity
    {
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string  CreatedBy { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        /// <summary>
        /// NGười sửa
        /// </summary>
        public string ModifiedBy { get; set; }
        /// <summary>
        /// Trạng thái object
        /// </summary>
        public EditMode EditMode { get; set; }


    }
}
