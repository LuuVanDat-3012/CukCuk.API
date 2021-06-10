using MISA.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interface.Service
{
    public interface ICustomerExport
    {
        ActionServiceResult ExportData(int pageIndex, int pageSize, string filter);
    }
}
