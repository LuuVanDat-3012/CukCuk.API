using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Web.Controllers
{
    public class WardController: BaseEntitiesController<Ward>
    {
        IWardService _wardService;
        public WardController(IWardService wardService): base(wardService)
        {
            _wardService = wardService;
        }
        [HttpGet("/Wards")]
        public IActionResult GetWards([FromQuery] Guid districtId)
        {
            return Ok(_wardService.GetWardByDistrict(districtId));
        }

    }
}
