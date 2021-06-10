using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entity
{
    public class Enumeration
    {
        public enum MISAcode
        {
            Success = 200,
            Exception = 1000,
            Error = 500,
            Validate = 400,
            Howllow = 205
        }
        public enum EditMode
        {
            None = 0,
            Add = 1,
            Edit = 2,
            View = 3,
            Delete = 4
        }
    }
}
