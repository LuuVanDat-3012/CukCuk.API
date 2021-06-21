using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MISA.ApplicationCore.Entity.Enumeration;

namespace MISA.ApplicationCore.Entity
{
    public class ActionServiceResult
    {
        /// <summary>
        /// Trạng thái trả về
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Mã trả về
        /// </summary>
        public MISAcode MISAcode { get; set; }
        /// <summary>
        /// Danh sách các trường sai định dạng
        /// </summary>
        public List<FieldNotValid> FieldNotValids { get; set; } = new List<FieldNotValid>();
        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// Tổng số trang
        /// </summary>
        public object TotalPage { get; set; }
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int TotalRecord { get; set; }
    }
}
