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
        /// <summary>
        /// Lấy danh sách các đói tượng
        /// </summary>
        /// <param name="pageIndex">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi/ trang</param>
        /// <param name="filter">Điều kiện</param>
        /// <returns>Danh sách bản ghi</returns>
        /// CreatedBy: Lưu Văn Đạt(15/06/2021)
        [HttpGet]
        public IActionResult Get([FromQuery] int pageIndex,[FromQuery] int pageSize, [FromQuery] string filter)
        {
            return Ok(_baseService.GetEntities(pageIndex, pageSize, filter));
        }
        /// <summary>
        /// Lấy đối tượng theo id
        /// </summary>
        /// <param name="id">Id của đối tượng</param>
        /// <returns>! ddooois tượng</returns>
        /// CreatedBy: Lưu Văn Đạt(15/06/2021)
        // GET api/<BaseEntitiesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(_baseService.GetEntityById(Guid.Parse(id)));
        }

        /// <summary>
        /// Thêm, xóa, sửa theo edirMode truyền vào
        /// </summary>
        /// <param name="entities">Đối tượng cần thao tác</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        ///  CreatedBy: Lưu Văn Đạt(15/06/2021)
        // POST api/<BaseEntitiesController>
        [HttpPost]
        public IActionResult Post([FromBody] List<TEntity> entities)
        {
            return Ok(_baseService.SaveData(entities));
        }

    }
}
