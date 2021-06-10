using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entity
{
    /// <summary>
    /// Bắt buộc nhập
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Required: Attribute
    {
        public string Msg;
        public Required(string msg)
        {
            this.Msg = msg;
        }
    }
    /// <summary>
    /// Vượt quá độ dài quy định (20 ký tự)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Length : Attribute
    {
        public string Msg;
        public int MaxLength;
        public Length(string msg, int maxLength)
        {
            this.Msg = msg;
            this.MaxLength = maxLength;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckDuplicate: Attribute
    {

    }
    /// <summary>
    /// Không âm
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotNegative : Attribute
    {
        public string Msg;
        public NotNegative(string msg)
        {
            this.Msg = msg;
        }
    }
    /// <summary>
    /// Thời gian không nằm trong hkoangr cho phép 01-01-2001 đến 01-01-2021
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateTime : Attribute
    {
        public string Msg;
        public ValidateTime(string msg)
        {
            this.Msg = msg;
        }
    }
    public class EntityBase
    {

    }
}
