using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entity
{
    /// <summary>
    /// Lớp mô tả các trường sai định dạng khi thêm mới, sửa
    /// </summary>
    /// CreatedBy: LVDat(19/06/2021)
    public class FieldNotValid
    {
        /// <summary>
        /// Tên thuộc tính sai định dạng
        /// </summary>
        public string fieldName { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public  string msg { get; set; }
    }
}
