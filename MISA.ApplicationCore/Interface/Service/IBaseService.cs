using MISA.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interface
{
    public interface IBaseService<TEntity>
    {
        /// <summary>
        /// Hàm phân trang 1 danh sách đối tượng
        /// </summary>
        /// <param name="pageIndex">Vị trí trang</param>
        /// <param name="pageSize">Số lượng đối tượng trong 1 trang</param>
        /// <param name="filter">Thông tin tìm kiếm ( nếu có)</param>
        /// <returns></returns>
        ActionServiceResult GetEntities(int pageIndex, int pageSize, string filter);
        /// <summary>
        /// Lấy theo id
        /// </summary>
        /// <returns>1 đối tượng</returns>
        /// CreatedBy: LVDat (26/5/2021)
        ActionServiceResult GetEntityById(Guid entityId);
        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity">đối tượng thêm mới</param>
        /// <returns>Số dòng ảnh hưởng</returns>
        ///  CreatedBy: LVDat (26/5/2021)
        ActionServiceResult AddEntity(TEntity entity);
        /// <summary>
        /// Sửa thông tin đối tượng
        /// </summary>
        /// <param name="entity">Thông tin mới</param>
        /// <returns>Số dòng ảnh hưởng</returns>
        /// CreatedBy: LVDat (26/5/2021)
        ActionServiceResult UpdateEntity(TEntity entity);
        /// <summary>
        /// Xoá theo id
        /// </summary>
        /// <param name="entityId">Id cần xoá</param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        /// CreatedBy: LVDat (26/5/2021)
        ActionServiceResult DeleteEntity(Guid entityId);
        /// <summary>
        /// Hàm điều hướng các editmode 
        /// None = 0,
        /// Add = 1,
        /// Edit = 2,
        /// Delete = 4
        /// </summary>
        /// <param name="entities">Danh sách đối tượng cần thao tác</param>
        /// <returns>ActionServiceResult</returns>
        ActionServiceResult SaveData(List<TEntity> entities);
    }
}
