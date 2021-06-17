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
        int GetDataPaging(string sqlCommand, DynamicParameters param, CommandType commandType);
    }
}
