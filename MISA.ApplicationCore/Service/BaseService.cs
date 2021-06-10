using Dapper;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using static MISA.ApplicationCore.Entity.Enumeration;

namespace MISA.ApplicationCore.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity>
    {
        IBaseRepository<TEntity> _baseRepository;
        protected string _tableName;
        #region Constructor
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _tableName = typeof(TEntity).Name;
        }
        #endregion
        public virtual ActionServiceResult AddEntity(TEntity entity)
        {
            ActionServiceResult actionServiceResult = new ActionServiceResult();
            var isValidate = this.BaseValidate(entity);
            var param = this.MappingDataType(entity);

            if (isValidate.Count == 0)
            {
                var result = _baseRepository.ExecuteNonQuery($"Proc_Add{_tableName}", param, commandType: CommandType.StoredProcedure);
                return new ActionServiceResult()
                {
                    Success = true,
                    MISAcode = MISAcode.Success,
                    Message = "Thêm mới thành công !!!",
                    data = result
                };
            }
            return new ActionServiceResult()
            {
                Success = false,
                MISAcode = MISAcode.Validate,
                Message = "Thêm mới không thành công !!!",
                FieldNotValids = isValidate,
                data = -1
            };
        }
        public ActionServiceResult DeleteEntity(Guid entityId)
        {

            var param = new DynamicParameters();
            param.Add("@Id", entityId.ToString());
            var result = _baseRepository.ExecuteNonQuery($"Proc_Delete{_tableName}", param, commandType: CommandType.StoredProcedure);
            if (result == 0)
            {
                return new ActionServiceResult()
                {
                    Success = false,
                    MISAcode = MISAcode.Validate,
                    Message = "Không tìm thấy  bản ghi cần xoá !!!",
                    data = 0
                };
            }
            return new ActionServiceResult()
            {
                Success = true,
                MISAcode = MISAcode.Validate,
                Message = "Xoá thành công !!!",
                data = result
            };
        }
        public virtual ActionServiceResult GetEntities(int pageIndex, int pageSize, string filter)
        {

            var param = new DynamicParameters();
            param.Add("@PageIndex", pageIndex);
            param.Add("@PageSize", pageSize);
            param.Add("@Filter", filter);
            return new ActionServiceResult()
            {
                Message = "Thành công",
                Success = true,
                MISAcode = Enumeration.MISAcode.Success,
                data = _baseRepository.Get($"Proc_Get{_tableName}Paging", param, commandType: CommandType.StoredProcedure)
            };
        }

        public ActionServiceResult GetEntityById(Guid entityId)
        {
            var param = new DynamicParameters();
            param.Add("@CustomerId", entityId.ToString());
            var entity = _baseRepository.Get($"Proc_Get{_tableName}ById", param, commandType: CommandType.StoredProcedure);
            if (entity != null)
            {
                return new ActionServiceResult()
                {
                    Message = "Thành công",
                    Success = true,
                    MISAcode = Enumeration.MISAcode.Success,
                    data = entity
                };
            }
            else
            {
                return new ActionServiceResult()
                {
                    Message = "Không tìm thấy",
                    Success = true,
                    MISAcode = Enumeration.MISAcode.Howllow,
                    data = null
                };
            }
        }
        public virtual ActionServiceResult UpdateEntity(TEntity entity)
        {
            var param = this.MappingDataType(entity);
            var isValid = BaseValidate(entity);
            if (isValid.Count == 0)
            {
                var result = _baseRepository.ExecuteNonQuery($"Proc_Update{_tableName}", param, CommandType.StoredProcedure);
                return new ActionServiceResult()
                {
                    Message = "Cập nhật bản ghi thành công !!!",
                    Success = true,
                    MISAcode = Enumeration.MISAcode.Success,
                    data = result
                };
            }
            return new ActionServiceResult()
            {
                Message = "Cập nhật bản ghi không  thành công !!!",
                Success = false,
                MISAcode = Enumeration.MISAcode.Validate,
                FieldNotValids = isValid,
                data = null
            };
        }
        /// <summary>
        /// Validate dữ liệu
        /// </summary>
        /// <param name="entity">Đối tượng validate</param>
        /// <returns>! danh sách các property không chính xác</returns>
        protected List<FieldNotValid> BaseValidate(TEntity entity)
        {
            List<string> msg = new List<string>();
            List<FieldNotValid> fieldNotValids = new List<FieldNotValid>();
            //Đọc các property
            var properties = entity.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Validate property bắt buộc nhập
                var propertyRequired = property.GetCustomAttributes(typeof(Required), true);
                if (propertyRequired.Length > 0)
                {
                    var message = (propertyRequired[0] as Required).Msg;
                    var value = property.GetValue(entity);
                    var propertyTypeName = property.PropertyType.Name;
                    var propertyName = property.Name;
                    if (propertyTypeName.Equals("String"))
                        if (value.ToString() == string.Empty || value == null)
                        {
                            fieldNotValids.Add(new FieldNotValid()
                            {
                                fieldName = propertyName,
                                msg = message
                            });
                        }

                    if (propertyTypeName.Equals("DateTime"))
                    {
                        DateTime dateTime = default(DateTime);
                        if (value.ToString() == dateTime.ToString())
                        {
                            fieldNotValids.Add(new FieldNotValid()
                            {
                                fieldName = propertyName,
                                msg = message
                            });

                        }
                    }
                }
                // Validate property theo độ dài
                var propertieLength = property.GetCustomAttributes(typeof(Length), true);
                if (propertieLength.Length > 0)
                {
                    var message = (propertieLength[0] as Length).Msg;
                    var maxLength = (propertieLength[0] as Length).MaxLength;
                    var value = property.GetValue(entity);
                    var propertyTypeName = property.PropertyType.Name;
                    var propertyName = property.Name;
                    if (propertyTypeName.Equals("String"))
                        if (value.ToString().Length > maxLength)
                        {
                            fieldNotValids.Add(new FieldNotValid()
                            {
                                fieldName = propertyName,
                                msg = message + " không vượt quá " + maxLength + " ký tự !!!"
                            });
                        }
                }
                // Validate các property không âm
                var propertyNegative = property.GetCustomAttributes(typeof(NotNegative), true);
                if (propertyNegative.Length > 0)
                {
                    var propertyType = property.PropertyType;
                    var value = property.GetValue(entity);
                    var message = (propertyNegative[0] as NotNegative).Msg;
                    var propertyName = property.Name;
                    if ((int)value < 0 || (int)value > 2)
                        fieldNotValids.Add(new FieldNotValid()
                        {
                            fieldName = propertyName,
                            msg = message
                        });
                }
                //Validate ngày tháng năm
                var propertyTime = property.GetCustomAttributes(typeof(ValidateTime), true);
                if (propertyTime.Length > 0)
                {
                    var value = property.GetValue(entity);
                    var message = (propertyTime[0] as ValidateTime).Msg;
                    var propertyName = property.Name;
                    var startDate = new DateTime(2001, 01, 01, 0, 0, 0);
                    var endDate = new DateTime(2021, 01, 01, 0, 0, 0);
                    if ((DateTime)value < startDate || (DateTime)value > endDate)
                        fieldNotValids.Add(new FieldNotValid()
                        {
                            fieldName = propertyName,
                            msg = message
                        });
                }
            }
            return fieldNotValids;
        }

        public DynamicParameters MappingDataType<Tentity>(Tentity entity)
        {
            var properties = entity.GetType().GetProperties();
            var param = new DynamicParameters();
            foreach (var property in properties)
            {
                var propertyname = property.Name;
                var propertyvalue = property.GetValue(entity);
                var propertytype = property.PropertyType;
                if (propertytype == typeof(Guid) || propertytype == typeof(Guid?))
                {
                    param.Add($"@{propertyname}", propertyvalue, DbType.String);
                }
                else
                {
                    param.Add($"@{propertyname}", propertyvalue);
                }
            }
            return param;
        }
        public ActionServiceResult SaveData(List<TEntity> entities)
        {
            if (entities?.Count > 0)
            {
                PreSave(entities);
                // Khai báo các list để chưa các entity có EditMode tương ứng
                List<Guid> entityDelete = new List<Guid>();
                // Lặp và gọi các hàm tương ứng tương ứng
                foreach (var entity in entities)
                {
                    var oEditMode = entity.GetType().GetProperty("EditMode").GetValue(entity);
                    var editMode = Convert.ToInt32(oEditMode);
                    if (editMode == (int)EditMode.Add)
                    {
                        return AddEntity(entity);
                    }
                    else if (editMode == (int)EditMode.Delete)
                    {
                        var entityId = entity.GetType().GetProperty(_tableName + "Id").GetValue(entity);
                        entityDelete.Add((Guid)entityId);
                    }
                    else if (editMode == (int)EditMode.Edit)
                    {
                  
                        return UpdateEntity(entity);
                    }
                }
                if (entityDelete.Count != 0)
                {
                    return DeleteMultiple(entityDelete.ToArray());
                }
            }
            return new ActionServiceResult()
            {
            };
        }
        public bool GetData(int pageIndex, int pageSize, string filter)
        {
            return true;
        }
        /// <summary>
        /// Hàm custom trước khi save
        /// </summary>
        /// <param name=""></param>
        public virtual void PreSave(List<TEntity> entities)
        {

        }
        /// <summary>
        /// Hàm xóa nhiều bản ghi
        /// </summary>
        /// <param name="listId">Danh sách các ID cần xóa</param>
        /// <returns></returns>
        public ActionServiceResult DeleteMultiple(Guid[] listId)
        {
            var affectedRows = 0;
            using (var transaction = new TransactionScope())
            {
                foreach (Guid id in listId)
                {
                    var result = this.DeleteEntity(id);
                    affectedRows += (int)result.data;
                }
                if (affectedRows == listId.Length)
                {
                    transaction.Complete();
                    return new ActionServiceResult()
                    {
                        Success = true,
                        Message = "Xoá thành công !!!",
                        MISAcode = Enumeration.MISAcode.Success,
                        data = affectedRows
                    };
                }
                else
                {
                    transaction.Dispose();
                    return new ActionServiceResult()
                    {
                        Success = true,
                        Message = "Xóa không thành công !!!",
                        MISAcode = Enumeration.MISAcode.Success,
                        data = 0
                    };
                }

            }
        }
    }
}

























// LVDat