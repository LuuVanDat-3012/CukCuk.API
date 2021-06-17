using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Web.Controllers
{
    public class WardsController: BaseEntitiesController<Ward>
    {
        IWardService _wardService;
        public WardsController(IWardService wardService): base(wardService)
        {
            _wardService = wardService;
        }
        [HttpGet("get/byDistrict")]
        public IActionResult GetWards([FromQuery] Guid districtId)
        {
            return Ok(_wardService.GetWardByDistrict(districtId));
        }

    }
}
