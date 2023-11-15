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
                    Session["FechaEntrega"] = paquete.Entrega.FechaEntrega;
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
        public ActionResult GetCodigoRastreo(BL.Paquete paquete)
        {
            paquete.EstatusEntrega=new BL.EstatusEntrega();
            paquete.Repartidor = new BL.Repartidor();
            paquete.Entrega = new BL.Entrega();
            paquete.Usuario=new BL.Usuario();

            ViewBag.IdPaquete = paquete.IdPaquete;
            ViewBag.Detalle = paquete.Detalle;
            ViewBag.Peso = paquete.Peso;
            ViewBag.DireccionOrigen = paquete.DireccionOrigen;
            ViewBag.DireccionEntrega = paquete.DireccionEntrega;
            ViewBag.FechaEstimadaEntrega = paquete.FechaEstimadaEntrega;
            ViewBag.CodigoRastreo = paquete.CodigoRastreo;
            ViewBag.IdEntrega = paquete.Entrega.IdEntrega;
            ViewBag.FechaEntrega = paquete.Entrega.FechaEntrega;
            ViewBag.IdRepartidor = paquete.Repartidor.IdRepartidor;
            ViewBag.Telefono = paquete.Repartidor.Telefono;
            ViewBag.IdEstatus= paquete.EstatusEntrega.IdEstatus;
            ViewBag.Estatus= paquete.EstatusEntrega.Estatus;
            ViewBag.IdUsuario = paquete.Usuario.IdUsuario;
            ViewBag.Nombre= paquete.Usuario.Nombre;
            ViewBag.ApellidoPaterno= paquete.Usuario.ApellidoPaterno;
            ViewBag.ApellidoMaterno= paquete.Usuario.ApellidoMaterno;
           
            return View();
        }
    }
}