using Dapper;
using MISA.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace MISA.ApplicationCore.Interface
{
    public interface IBaseRepository<TEntity>
    {
        ///// <summary>
        ///// Lấy toàn bộ danh sách
        ///// </summary>
        ///// <returns>danh sách đối tượng</returns>
        ///// CreatedBy: LVDat (20/5/2021)
        /////List<TEntity> GetEntities();
        ///// <summary>
        ///// Lấy đối tượng theo ID
        ///// </summary>
        ///// <returns>1 đối tượng</returns>
        ///// CreatedBy: LVDat (20/5/2021)
        //TEntity GetEntityById(Guid entityId);
        ///// <summary>
        ///// Thêm mới
        ///// </summary>
        ///// <returns>Số dòng ảnh hưởng</returns>
        ///// CreatedBy: LVDat (20/5/2021)
        //int AddEntity(TEntity entity);
        ///// <summary>
        ///// Sửa thông tin đối tượng
        ///// </summary>
        ///// <returns>Số dòng ảnh hưởng</returns>
        ///// CreatedBy: LVDat (20/5/2021)
        //int UpdateEntity(TEntity entity);
        ///// <summary>
        ///// Xoá đối tượng
        ///// </summary>
        ///// <returns>Số dòng ảnh hưởng</returns>
        ///// CreatedBy: LVDat (20/5/2021)
        //int DeleteEntity(Guid TEntityId);
        //TEntity GetEntityByProperty(string propertyName, object propertyValue);
        ///// <summary>
        ///// Phân trang 
        ///// </summary>
        ///// <param name="pagingModel">Sô</param>
        ///// <returns></returns>
        //List<TEntity> PagingEntity(PagingModel pagingModel);

        /// <summary>
        /// Hàm thực hiện lấy dữ liệu 
        /// </summary>
        /// <param name="sqlCommand">Câu lệnh hoặc Proc</param>
        /// <param name="param">Tham số</param>
        /// <param name="commandType">Kiểu command</param>
        /// <returns>1 danh sách entity</returns>
        /// CreatedBy: LVDat (03/06/2021)
        IEnumerable<TEntity> Get(string sqlCommand, DynamicParameters param, CommandType commandType);
        /// <summary>
        /// Hàm thực hiện thêm, xoá ,sửa
        /// </summary>
        /// <param name="sqlCommand">Câu lệnh hoặc Proc</param>
        /// <param name="param">Tham số</param>
        /// <param name="commandType">Kiểu command</param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        /// CreatedBy: LVDat (03/06/2021)
        int ExecuteNonQuery(string sqlCommand, DynamicParameters param, CommandType commandType);
        /// <summary>
        /// Hàm thực hiện lấy ra tổng số bản ghi
        /// </summary>
        /// <param name="sqlCommand">Câu lệnh hoặc Proc</param>
        /// <param name="param">Tham số</param>
        /// <param name="commandType">Kiểu command</param>
        /// <returns> Số bản ghi</returns>
        /// CreateBy: LVDat (07/06/2021)
        int GetQuality(string sqlCommand, DynamicParameters param, CommandType commandType);
    }
}
