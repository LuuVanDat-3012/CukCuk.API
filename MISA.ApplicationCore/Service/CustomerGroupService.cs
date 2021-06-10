using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Service
{
    public class CustomerGroupService : BaseService<CustomerGroup>, ICustomerGroupService
    {
        IBaseRepository<CustomerGroup> _baseRepository;

        #region Constructor
        public CustomerGroupService(IBaseRepository<CustomerGroup> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }
        #endregion

    }
}
