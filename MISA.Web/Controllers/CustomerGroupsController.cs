using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface;
using MISA.Web.Controllers;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.service.Controllers
{
    public class CustomerGroupsController : BaseEntitiesController<CustomerGroup>
    {
        IBaseService<CustomerGroup> _baseService;
        public CustomerGroupsController(IBaseService<CustomerGroup> baseService) : base(baseService)
        {
            _baseService = baseService;
        }
    }
}
