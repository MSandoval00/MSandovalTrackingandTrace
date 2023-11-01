using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SLWebApi.Controllers
{
    [RoutePrefix("api/unidadentrega")]
    public class UnidadEntregaController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var result= BL.UnidadEntrega.GetAll();
            if (result!=null)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [HttpGet]
        [Route("UnidadEstatus")]
        public IHttpActionResult EstatusUnidadGetAll()
        {
            var result=BL.EstatusUnidad.GetAll();
            if (result!=null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("{IdUnidad}")]
        public IHttpActionResult GetById(int IdUnidad)
        {
            var result= BL.UnidadEntrega.GetById(IdUnidad);
            if (result!=null)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody]BL.UnidadEntrega unidadEntrega)
        {
            var result= BL.UnidadEntrega.Add(unidadEntrega);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete]
        [Route("{IdUnidad}")]
        public IHttpActionResult Delete(int IdUnidad)
        {
            var result=BL.UnidadEntrega.Delete(IdUnidad);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut]
        [Route("{IdUnidad}")]
        public IHttpActionResult Update([FromBody]BL.UnidadEntrega unidadEntrega,int IdUnidad)
        {
            var result = BL.UnidadEntrega.Update(unidadEntrega);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}