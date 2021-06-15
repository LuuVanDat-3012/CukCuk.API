using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Web.Controllers
{
    public class ProvincesController: BaseEntitiesController<Province>
    {
        IProvinceService _provinceService;
        public ProvincesController(IProvinceService provinceService) : base(provinceService)
        {
            _provinceService = provinceService;
        }
        [HttpGet("get/byCountry")]
        public IActionResult GetProvince([FromQuery] Guid countryId)
        {
            var result = _provinceService.GetProvinceByCountry(countryId);
            return Ok(result);
        }
    }
}
