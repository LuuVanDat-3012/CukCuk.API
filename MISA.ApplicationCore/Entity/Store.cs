using System;
using System.Collections.Generic;
using System.Text;
using static MISA.ApplicationCore.Entity.Enumeration;

namespace MISA.ApplicationCore.Entity
{
    /// <summary>
    /// Thông tin cửa hàng
    /// </summary>
    /// CreatedBy: LVDat (11/06/2021)
    public class Store: BaseEntity
    {
        /// <summary>
        /// Id cửa hàng
        /// </summary>
        public Guid StoreId { get; set; }
        /// <summary>
        /// Mã cửa hàng
        /// </summary>
        [Required("Mã cửa hàng không được để trống")]
        public string StoreCode { get; set; }
        /// <summary>
        /// Tên sửa hàng
        /// </summary>
        [Required("Tên cửa hàng không được để trống")]
        public string StoreName { get; set; }
        /// <summary>
        /// Địa chỉ cửa hàng
        /// </summary>
        [Required("Địa chỉ không được để trống")]
        public string Address { get; set; }
        /// <summary>
        /// Số điện thoại (thông tin liên hệ)
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string StoreTaxCode { get; set; }
        /// <summary>
        /// Mã quốc gia
        /// </summary>
        [Required("Mã quốc gia không được để trống")]
        public Guid CountryId { get; set; }
        /// <summary>
        /// Mã tỉnh/thành phố
        /// </summary>
        [Required("Mã tỉnh thành không được để trống")]
        public Guid ProvinceId { get; set; }
        /// <summary>
        /// Mã quận/huyện
        /// </summary>
        [Required("Mã quận/huyện không được để trống")]
        public Guid DistrictId { get; set; }
        /// <summary>
        /// Mã xã/phường
        /// </summary>
        [Required("Mã xã/phường không được để trống")]
        public Guid WardId { get; set; }
        /// <summary>
        /// Phố
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// Trạng thái của cửa hàng
        /// </summary>
        public StoreStatus Status { get; set; }
    }
}
