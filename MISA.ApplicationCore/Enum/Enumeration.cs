using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entity
{
    public class Enumeration
    {
        /// <summary>
        /// Mã code trả về client
        /// 200: Thành công
        /// 1000: Exception
        /// 500: Lỗi server
        /// 400: Lỗi date
        /// 205: Không tìm thấy nội dung
        /// </summary>
        /// CreatedBy: LVDat (11/06/2021)
        public enum MISAcode
        {
            Success = 200,
            Exception = 1000,
            Error = 500,
            Validate = 400,
            Howllow = 205
        }
        /// <summary>
        /// Trạng thái của đối tượng
        /// 0: Không thao tác
        /// 1: Thêm mới
        /// 2: Sửa
        /// 3: Xem
        /// 4: Xoá
        /// </summary>
        /// CreatedBy: LVDat (11/06/2021)
        public enum EditMode
        {
            None = 0,
            Add = 1,
            Edit = 2,
            View = 3,
            Delete = 4
        }

        /// <summary>
        /// Trạng thái hoạt động của cửa hàng
        /// 0: Ngừng hoạt động
        /// 1: Đang hoạt động
        /// </summary>
        /// CreatedBy: LVDat (11/06/2021)
        public enum StoreStatus
        {
            DeActive = 0,
            Active = 1
        }

        /// <summary>
        /// Trạng thái tìm kiếm
        /// 1: Chứa
        /// 2: Bằng
        /// 3: Bắt đầu bằng
        /// 4: Kết thúc bằng
        /// 5: Không chứa
        /// </summary>
        public enum OptionFilter
        {
            Contain = 1,
            Equal = 2,
            StartWith = 3,
            EndWith = 4,
            NoContain = 5
        }
    }
}
