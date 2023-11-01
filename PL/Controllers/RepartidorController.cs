using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class RepartidorController : Controller
    {
        // GET: Repartidor
        [HttpGet]
        public ActionResult GetAll()
        {
            BL.Repartidor repartidor = new BL.Repartidor();
            repartidor.Repartidores = new List<object>();
            ServiceReferenceRepartidor.RepartidorClient repartidorWCF = new ServiceReferenceRepartidor.RepartidorClient();
            var result = repartidorWCF.GetAll();
            if (result.Objects!=null)
            {
                repartidor.Repartidores = result.Objects.ToList();
            }
            else
            {
                ViewBag.Message = result.ErrorMessage;
            }
            return View(repartidor);
        }
        [HttpGet]
        public ActionResult Form(int? IdRepartidor)
        {
            BL.Repartidor repartidor = new BL.Repartidor();
            repartidor.UnidadEntrega=new BL.UnidadEntrega();
            ServiceReferenceRepartidor.RepartidorClient repartidorWCF=new ServiceReferenceRepartidor.RepartidorClient();
            var resultRepartidor = repartidorWCF.UnidadEntregaGetAll();
            if (IdRepartidor !=null)//update
            {
                var result = repartidorWCF.GetById(IdRepartidor.Value);
                if (result !=null)
                {
                    repartidor = (BL.Repartidor)result.Object;
                    repartidor.UnidadEntrega.UnidadEntregas = resultRepartidor.Objects.ToList();
                }
            }
            else//Add
            {
                repartidor.UnidadEntrega.UnidadEntregas = resultRepartidor.Objects.ToList();
            }
            return View(repartidor);
        }
        [HttpPost]
        public ActionResult Form(BL.Repartidor repartidor)
        {
            HttpPostedFileBase file = Request.Files["Imagen"];
            if (file.ContentLength > 0)
            {
                repartidor.Fotografía = ConvertirABase64(file);
            }
            ServiceReferenceRepartidor.RepartidorClient repartidorWCF = new ServiceReferenceRepartidor.RepartidorClient();
            if (repartidor.IdRepartidor==0)//ADD
            {
                var result = repartidorWCF.Add(repartidor);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Se ingreso el repartidor";

                }
                else
                {
                    ViewBag.Mensaje = "No se ingreso el repartidor";
                }
            }
            else
            {
                var result = repartidorWCF.Update(repartidor);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Se actualizo el repartidor";
                }
                else
                {
                    ViewBag.Mensaje = "No se actualizo el repartidor";
                }
            }
            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult Delete(int IdRepartidor)
        {
            ServiceReferenceRepartidor.RepartidorClient repartidorWCF = new ServiceReferenceRepartidor.RepartidorClient();
            var result = repartidorWCF.Delete(IdRepartidor);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Se elimino el repartidor";
            }
            else
            {
                ViewBag.Mensaje = "No se elimino el repartidor";
            }
            return PartialView("Modal");
        }
        public string ConvertirABase64(HttpPostedFileBase Foto)
        {
            System.IO.BinaryReader reader=new System.IO.BinaryReader(Foto.InputStream);
            byte[] data = reader.ReadBytes((int)Foto.ContentLength);
            string imagen=Convert.ToBase64String(data);
            return imagen;
        }
    }
}