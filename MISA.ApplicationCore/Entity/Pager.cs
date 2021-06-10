using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entity
{
    /// <summary>
    /// Hàm 
    /// </summary>
   public class Pager
    {
        public Pager()
        {
            PropertySearch = string.Empty;
            Offset = 20;
            Limit = 0;
        }
        /// <summary>
        /// Thuuojc tính cần tìm kiếm
        /// </summary>
        public string PropertySearch { get; set; }
        /// <summary>
        /// Số bản ghi cần lấy
        /// </summary>
        public int Offset  { get; set; }
        /// <summary>
        /// Tên trang
        /// </summary>
        public int Limit { get; set; }

    }
}
