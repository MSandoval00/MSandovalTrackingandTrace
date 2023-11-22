using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
        public ActionResult CrearPaquete(BL.Paquete paquete, string email)
        {
            //mail , string email
            string emailOrigen = "msandovalg00@gmail.com";
            MailMessage mailMessage = new MailMessage(emailOrigen, email, "Se registro paquete", "<p>Correo para asignar paquete</p>");
            mailMessage.IsBodyHtml = true;

            string contenidoHTML = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Content", "Template", "Email.html"));

            mailMessage.Body = contenidoHTML;
            string url = "http://localhost:6292/" + HttpUtility.UrlEncode(email);

            mailMessage.Body = mailMessage.Body.Replace("{Url}", url);
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, "wsgwmslplimaicmr");

            smtpClient.Send(mailMessage);
            smtpClient.Dispose();

            ViewBag.Modal = "show";
            ViewBag.Mensaje = "Se ha enviado el correo de confirmación a tu correo electronico";

            //Add
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
        //mail y inyeccion de dependencias
        

    }
}