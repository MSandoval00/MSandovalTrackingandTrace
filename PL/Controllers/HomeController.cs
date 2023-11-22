using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Index(string CodigoRastreo)
        {
            BL.Paquete paquete = new BL.Paquete();
            if (CodigoRastreo != null)
            {
                object result = BL.Paquete.GetByCodigoRastreo(CodigoRastreo);
                if (result != null)
                {
                    paquete = (BL.Paquete)result;
                    Session["IdPaquete"] = int.Parse(paquete.IdPaquete.ToString());
                    Session["Detalle"] = paquete.Detalle;
                    Session["Peso"] = float.Parse(paquete.Peso.ToString());
                    Session["DireccionOrigen"] = paquete.DireccionOrigen;
                    Session["DireccionEntrega"] = paquete.DireccionEntrega;
                    Session["FechaEstimadaEntrega"] = DateTime.Parse(paquete.FechaEstimadaEntrega.ToString());
                    Session["CodigoRastreo"] = paquete.CodigoRastreo;
                    Session["IdEntrega"] = paquete.Entrega.IdEntrega;
                    //Session["FechaEntrega"] = paquete.Entrega.FechaEntrega;
                    Session["IdRepartidor"] = paquete.Repartidor.IdRepartidor;
                    Session["Telefono"] = paquete.Repartidor.Telefono;
                    Session["IdEstatus"] = paquete.EstatusEntrega.IdEstatus;
                    Session["Estatus"] = paquete.EstatusEntrega.Estatus;
                    Session["IdUsuario"] = paquete.Usuario.IdUsuario;
                    Session["Nombre"] = paquete.Usuario.Nombre;
                    Session["ApellidoPaterno"] = paquete.Usuario.ApellidoPaterno;
                    Session["ApellidoMaterno"] = paquete.Usuario.ApellidoMaterno;                

                    return RedirectToAction("GetCodigoRastreo", "Home");
                }
            }
            return View(paquete);
        }
        [HttpGet]
        public ActionResult GetCodigoRastreo()
        {
            return View();
        }
    }
}