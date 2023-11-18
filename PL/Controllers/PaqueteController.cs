using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class PaqueteController : Controller
    {
        // GET: Paquete
        public ActionResult Cards()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CrearPaquete()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CrearPaquete(BL.Paquete paquete)
        {
            if (paquete.IdPaquete==0)//Add
            {
                bool result = BL.Paquete.Add(paquete);
                if (result)
                {
                    ViewBag.Mensaje = "Se registro el paquete";
                }
                else
                {
                    ViewBag.Mensaje = "No se pudo registrar el paquete";
                }
            }

            return View("Modal");
        }
    }
}