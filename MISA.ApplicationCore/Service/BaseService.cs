using Dapper;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Resources;
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
        #region Method
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
                    Message = ApplicationCore.Properties.Resources.addSuccess,
                    Data = result
                };
            }
            return new ActionServiceResult()
            {
                Success = false,
                MISAcode = MISAcode.Validate,
                Message = ApplicationCore.Properties.Resources.validateError,
                FieldNotValids = isValidate,
                Data = -1
            };
        }
        public ActionServiceResult DeleteEntity(Guid entityId)
        {
            var paramName = "@" + _tableName + "Id";
            var param = new DynamicParameters();
            param.Add(paramName, entityId.ToString());
            var result = _baseRepository.ExecuteNonQuery($"Proc_Delete{_tableName}", param, commandType: CommandType.StoredProcedure);
            if (result == 0)
            {
                return new ActionServiceResult()
                {
                    Success = false,
                    MISAcode = MISAcode.Validate,
                    Message = ApplicationCore.Properties.Resources.recordNotFound,
                    Data = 0
                };
            }
            return new ActionServiceResult()
            {
                Success = true,
                MISAcode = MISAcode.Validate,
                Message = ApplicationCore.Properties.Resources.deleteSuccess,
                Data = result
            };
        }
        public virtual ActionServiceResult GetEntities(int pageIndex, int pageSize, string filter)
        {
            var param = new DynamicParameters();
            param.Add("@Filter", filter);
            return new ActionServiceResult()
            {
                Message = ApplicationCore.Properties.Resources.getSuccess,
                Success = true,
                MISAcode = Enumeration.MISAcode.Success,
                Data = _baseRepository.Get($"Proc_Get{_tableName}s", null, commandType: CommandType.StoredProcedure)
            };
        }

        public ActionServiceResult GetEntityById(Guid entityId)
        {
            var paramName = "@" + _tableName + "Id";
            var param = new DynamicParameters();
            param.Add(paramName, entityId.ToString());
            var entity = _baseRepository.Get($"Proc_Get{_tableName}By{_tableName}Id", param, commandType: CommandType.StoredProcedure);
            if (entity != null)
            {
                return new ActionServiceResult()
                {
                    Message = ApplicationCore.Properties.Resources.getSuccess,
                    Success = true,
                    MISAcode = Enumeration.MISAcode.Success,
                    Data = entity
                };
            }
            else
            {
                return new ActionServiceResult()
                {
                    Message = ApplicationCore.Properties.Resources.notFound,
                    Success = true,
                    MISAcode = Enumeration.MISAcode.Howllow,
                    Data = null
                };
            }
        }

        public virtual ActionServiceResult UpdateEntity(TEntity entity)
        {
            var param = MappingDataType(entity);
            var isValid = BaseValidate(entity);
            if (isValid.Count == 0)
            {
                var result = _baseRepository.ExecuteNonQuery($"Proc_Update{_tableName}", param, CommandType.StoredProcedure);
                return new ActionServiceResult()
                {
                    Message = ApplicationCore.Properties.Resources.updateSuccess,
                    Success = true,
                    MISAcode = Enumeration.MISAcode.Success,
                    Data = result
                };
            }
            return new ActionServiceResult()
            {
                Message = ApplicationCore.Properties.Resources.updateNotSuscess,
                MISAcode = Enumeration.MISAcode.Validate,
                FieldNotValids = isValid,
                Data = null
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
                    // Kiểm tra với kiểu dữ liệu string
                    if (propertyTypeName.Equals("String"))
                    {
                        if(value == null)
                        {
                            fieldNotValids.Add(new FieldNotValid()
                            {
                                fieldName = propertyName,
                                msg = message
                            });
                        }
                        else if (value.ToString() == string.Empty || value.ToString() == null)
                        {
                            fieldNotValids.Add(new FieldNotValid()
                            {
                                fieldName = propertyName,
                                msg = message
                            });
                        }
                    }
                    // Kiểm tra với kiểu dữ liệu datetime
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
                    // Kiểm tra với kiểu dữ liệu Guid
                    if (propertyTypeName.Equals("Guid"))
                    {
                        var defaultGuid = "00000000-0000-0000-0000-000000000000";
                        if (value.ToString() == defaultGuid || value == null)
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
                    var startYear = (propertyTime[0] as ValidateTime).StartYear;
                    var endYear = (propertyTime[0] as ValidateTime).EndYear;
                    var propertyName = property.Name;
                    if ((DateTime)value < new DateTime(startYear, 01, 01) || (DateTime)value > new DateTime(endYear, 01, 01))
                        fieldNotValids.Add(new FieldNotValid()
                        {
                            fieldName = propertyName,
                            msg = message
                        });
                }
                // Validate email
                var propertyEmail = property.GetCustomAttributes(typeof(ValidateEmail), true);
                if (propertyEmail.Length > 0)
                {
                    var value = property.GetValue(entity);
                    var message = (propertyEmail[0] as ValidateEmail).Msg;
                    var validEmail = IsValidEmail(value.ToString());
                    var propertyName = property.Name;
                    if (validEmail == false)
                    {
                        fieldNotValids.Add(new FieldNotValid()
                        {
                            fieldName = propertyName,
                            msg = message
                        });
                    }
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
                    // Nếu editMode  = 1 (Thêm) gọi vào hàm thêm
                    var editMode = Convert.ToInt32(oEditMode);
                    if (editMode == (int)EditMode.Add)
                    {
                        return AddEntity(entity);
                    }
                    // Nếu editMode  = 4 (Xóa) gọi vào hàm xóa
                    else if (editMode == (int)EditMode.Delete)
                    {
                        var entityId = entity.GetType().GetProperty(_tableName + "Id").GetValue(entity);
                        entityDelete.Add((Guid)entityId);
                    }
                    // Nếu editMode  = 3 (Thêm) gọi vào hàm sửa
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
                    affectedRows += (int)result.Data;
                }
                if (affectedRows == listId.Length)
                {
                    transaction.Complete();
                    return new ActionServiceResult()
                    {
                        Success = true,
                        Message = "Xoá thành công !!!",
                        MISAcode = Enumeration.MISAcode.Success,
                        Data = affectedRows
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
                        Data = 0
                    };
                }

            }
        }
        /// <summary>
        /// Hàm validate email
        /// </summary>
        /// <param name="email">Email cần validae</param>
        /// <returns>bool</returns>
        /// CreatedBy: LVDat(16/9/2021)
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}

























// LVDat