using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class UnidadEntregaController : Controller
    {
        // GET: UnidadEntrega
        [HttpGet]
        public ActionResult GetAll()
        {
            BL.UnidadEntrega unidadEntrega = new BL.UnidadEntrega();
            
            using (var client =new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);
                var responseTask = client.GetAsync("unidadentrega");
                responseTask.Wait();
                var resultService=responseTask.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsStringAsync();
                    readTask.Wait();
                    List<BL.UnidadEntrega> resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BL.UnidadEntrega>>(readTask.Result);
                    List<object> resultUnidadEntrega = resultItemList.Cast<object>().ToList();
                    unidadEntrega.UnidadEntregas = resultUnidadEntrega;
                }
            }
            return View(unidadEntrega);
        }
        [HttpGet]
        public ActionResult Form(int? IdUnidad)
        {
            BL.UnidadEntrega unidadEntrega = new BL.UnidadEntrega();
            unidadEntrega.EstatusUnidad = new BL.EstatusUnidad();
            unidadEntrega.UnidadEntregas = new List<object>();
            unidadEntrega.EstatusUnidad.EstatusUnidads = new List<object>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);
                var responseTask = client.GetAsync("unidadentrega/UnidadEstatus");
                responseTask.Wait();
                var resultService = responseTask.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsStringAsync();
                    readTask.Wait();
                    List<BL.EstatusUnidad> resultListItem = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BL.EstatusUnidad>>(readTask.Result);
                    List<object> resultEstatusUnidad = resultListItem.Cast<object>().ToList();
                    unidadEntrega.EstatusUnidad.EstatusUnidads = resultEstatusUnidad;
                }
            }
            var listaEstatus = unidadEntrega.EstatusUnidad.EstatusUnidads;
            if (IdUnidad!=null)//Update
            {
                using (var clientunidad = new HttpClient())
                {
                    clientunidad.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);
                    var responseTaskUnidad = clientunidad.GetAsync("unidadentrega/" + IdUnidad);
                    responseTaskUnidad.Wait();
                    var resultServiceUnidad = responseTaskUnidad.Result;
                    if (resultServiceUnidad.IsSuccessStatusCode)
                    {
                        var readTaskUnidad = resultServiceUnidad.Content.ReadAsStringAsync();
                        readTaskUnidad.Wait();
                        BL.UnidadEntrega unidadEntregaResult = Newtonsoft.Json.JsonConvert.DeserializeObject<BL.UnidadEntrega>(readTaskUnidad.Result);
                        unidadEntrega = unidadEntregaResult;
                    }
                }
            }
            else//Add
            {
                using (var client=new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);
                    var responseTask = client.GetAsync("unidadentrega/UnidadEstatus");
                    responseTask.Wait();
                    var resultService = responseTask.Result;
                    if (resultService.IsSuccessStatusCode)
                    {
                        var readTask = resultService.Content.ReadAsStringAsync();
                        readTask.Wait();
                        List<BL.EstatusUnidad> resultListItem = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BL.EstatusUnidad>>(readTask.Result);
                        List<object> resultEstatusUnidad = resultListItem.Cast<object>().ToList();
                        unidadEntrega.EstatusUnidad.EstatusUnidads = resultEstatusUnidad;
                    }
                }

            }
            unidadEntrega.EstatusUnidad.EstatusUnidads=listaEstatus; 
            return View(unidadEntrega);
        }
        [HttpPost]
        public ActionResult Form(BL.UnidadEntrega unidadEntrega)
        {
            if (unidadEntrega.IdUnidad==0)
            {
                using (var client=new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"] + "unidadentrega");
                    var postTask = client.PostAsJsonAsync(client.BaseAddress,unidadEntrega);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Se agregó la unidad correctamente";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se pudo agregar la unidad";
                    }
                }
            }
            else//Update
            {
                using (var client =new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);
                    var putTask=client.PutAsJsonAsync("unidadEntrega/" + unidadEntrega.IdUnidad,unidadEntrega);
                    putTask.Wait();
                    var result=putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Se actualizo la unidad";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se pudo actualizar la unidad";
                    }
                }
            }
            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult Delete(int IdUnidad)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);
                var deleteTask = client.DeleteAsync("unidadEntrega/" + IdUnidad);
                deleteTask.Wait();
                var result=deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Se elimino correctamente la unidad";
                }
                else
                {
                    ViewBag.Mensaje = "No se pudo eliminar la unidad";
                }
            }
            return PartialView("Modal");
        }
    }
}