using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Web.Controllers
{
    public class DistrictController: BaseEntitiesController<District>
    {
        IDistrictService _districtService;
        public DistrictController(IDistrictService districtService): base(districtService)
        {
            _districtService = districtService;
        }
        [HttpGet("/Districts")]
        public IActionResult GetDistrict([FromQuery] Guid provinceId)
        {
            return Ok(_districtService.GetDistrictByProvince(provinceId));
        }
    }
}
