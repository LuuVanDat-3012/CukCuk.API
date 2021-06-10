using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Interface;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MISA.Infrastructure
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        #region Declare
        protected IDbConnection _dbConnection;
        protected string _tableName;
        string _connectionString = string.Empty;
        IConfiguration _configuration;
        #endregion
        #region Constructor
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
             _connectionString = configuration.GetConnectionString("DefaultString");
            _dbConnection = new MySqlConnection(_connectionString);
            _tableName = typeof(TEntity).Name;
        }


        #endregion

        #region Method
        public int ExecuteNonQuery(string sqlCommand, DynamicParameters param, CommandType commandType)
        {
            return _dbConnection.Execute(sqlCommand, param, commandType: commandType);
        }

        public IEnumerable<TEntity> Get(string sqlCommand, DynamicParameters param, CommandType commandType)
        {
            return _dbConnection.Query<TEntity>(sqlCommand, param, commandType: commandType);
        }

        public int GetQuality(string sqlCommand, DynamicParameters param, CommandType commandType)
        {
            return Convert.ToInt32(_dbConnection.QueryFirst<int>(sqlCommand, param, commandType: commandType));
        }


        #endregion
    }
}
