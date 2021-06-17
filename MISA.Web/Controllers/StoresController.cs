using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface;
using MISA.ApplicationCore.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Web.Controllers
{
    public class StoresController : BaseEntitiesController<Store>
    {
        IStoreService _storeService;
        public StoresController(IStoreService storeService) : base(storeService)
        {
            _storeService = storeService;
        }
        [HttpGet("filter")]
        public IActionResult GetStoreByFilter([FromQuery] string storeCode, [FromQuery] string storeName, [FromQuery] string address, [FromQuery] string phoneNumber, [FromQuery] int status,
            [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
           var result = _storeService.GetStoreByFilter(storeCode, storeName, address, phoneNumber, status,
              pageIndex, pageSize);
            return Ok(result);
        }
    }
}
