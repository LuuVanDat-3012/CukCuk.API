using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseEntitiesController<TEntity> : ControllerBase
    {
        IBaseService<TEntity> _baseService;
        public BaseEntitiesController(IBaseService<TEntity> baseService)
        {
            _baseService = baseService;
        }
        // GET: api/<BaseEntitiesController>
        [HttpGet]
        public IActionResult Get([FromQuery] int pageIndex,[FromQuery] int pageSize, [FromQuery] string filter)
        {
            return Ok(_baseService.GetEntities(pageIndex, pageSize, filter));
        }

        // GET api/<BaseEntitiesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(_baseService.GetEntityById(Guid.Parse(id)));
        }

        // POST api/<BaseEntitiesController>
        [HttpPost]
        public IActionResult Post([FromBody] List<TEntity> entities)
        {
            return Ok(_baseService.SaveData(entities));
        }

    }
}
