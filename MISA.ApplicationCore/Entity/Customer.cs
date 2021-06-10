using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entity
{
    /// <summary>
    /// Thông tin khách hàng
    /// </summary>
    /// CreatedBy: LVDat (18/05/2021)
    public class Customer: BaseEntity
    {
        /// <summary>
        /// Id khách hàng
        /// </summary>
        public Guid CustomerId { get; set; }
        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [CheckDuplicate]
        [Required("Mã khách hàng không được để trống")]
        [Length("Mã khách hàng", 20)]
        public string CustomerCode { get; set; }
        /// <summary>
        /// Họ và tên
        /// </summary>
        public string Fullname { get; set; }
        /// <summary>
        /// Giới tinh 0-nữ, 1-nam, 2-khác
        /// </summary>
        [Required("Giới tính không được để trống")]
        [NotNegative("Giới tính chỉ nhận 3 giá trị 0, 1, 2")]
        public int? Gender { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        [Required("Ngày sinh không để trống")]
        [ValidateTime("Ngày sinh không nằm trong khoảng giá trị cho phép (01/01/2001 - 01/01/2021)")]
        public DateTime Birthday { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Địa chỉ email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// MÃ nhóm khách hàng
        /// </summary>
        public Guid CustomerGroupId { get; set; }
        /// <summary>
        /// Tên nhóm khách hàng
        /// </summary>
        public string CustomerGroupName { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// MÃ thẻ thành viên
        /// </summary>
        public string MemberCardCode { get; set; }
        /// <summary>
        /// mã số thuế
        /// </summary>
        public string TaxCode { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Tên công ty
        /// </summary>
        public string Company { get; set; }
}

}
