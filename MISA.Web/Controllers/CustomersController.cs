using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Web.Controllers
{
    public class CustomersController: BaseEntitiesController<Customer>
    {
        ICustomerService _customerService;
        public CustomersController(ICustomerService customerService) : base(customerService)
        {
            _customerService = customerService;
        }
        [HttpGet("/export")]
        public ActionServiceResult ExportToExcel() {

            return _customerService.ExportToExcel();
        }

    }
    

}
