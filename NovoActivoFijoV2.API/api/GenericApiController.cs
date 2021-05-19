// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NovoNStandSpeedWayV2.API.api
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using COMMON.Entidades;
    using COMMON.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public abstract class GenericApiController<T> : ControllerBase where T : BaseDTO
    {
        readonly IGenericManager<T> manager;

        public IGenericManager<T> GetManager
        {
            get
            {
                return manager;
            }
        }

        public GenericApiController(IGenericManager<T> manager)
        {
            this.manager = manager;
        }

        // GET: api/<GenericApiController>
        [HttpGet]

        public ActionResult<IEnumerable<T>> Get()
        {
            try
            {
                return Ok(manager.ObtenerTodos);

            }
            catch (Exception)
            {
                return BadRequest(manager.Error);
            }
        }

        // GET api/<GenericApiController>/5
        [HttpGet("{id}")]
        //[AllowAnonymous]
        public ActionResult<T> Get(string id)
        {
            try
            {
                return Ok(manager.BuscarPorId(id));
            }
            catch (Exception)
            {
                return BadRequest(manager.Error);
            }
        }

        // POST api/<GenericApiController>
        [HttpPost]
        public ActionResult<T> Post([FromBody] T value)
        {
            try
            {
                return Ok(manager.Insertar(value));
            }
            catch (Exception)
            {
                return BadRequest(manager.Error);
            }
        }

        // PUT api/<GenericApiController>/5
        [HttpPut]
        public ActionResult<T> Put([FromBody] T value)
        {
            try
            {
                return Ok(manager.Actualizar(value));

            }
            catch (Exception)
            {
                return BadRequest(manager.Error);
            }

        }

        // DELETE api/<GenericApiController>/5
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(string id)
        {
            try
            {
                return Ok(manager.Eliminar(id));

            }
            catch (Exception)
            {
                return BadRequest(manager.Error);
            }
        }

    }
}

