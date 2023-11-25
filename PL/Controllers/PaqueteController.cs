using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
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
            BL.Usuario usuario=new BL.Usuario();
            usuario.Nombre = "";
            BL.EstatusEntrega estatusEntrega=new BL.EstatusEntrega();
            estatusEntrega.Estatus = "";

            List<object> resultPaquetes = BL.Paquete.GetAll(usuario,estatusEntrega);//usuario estatusentrega
            List<object> resultEstatusEntrega = BL.EstatusEntrega.GetAll();

            BL.Paquete paquete=new BL.Paquete();
            paquete.EstatusEntrega=new BL.EstatusEntrega();
            paquete.Usuario = new BL.Usuario();
            paquete.Paquetes = new List<object>();
            paquete.EstatusEntrega.EstatusEntregas = new List<object>();

            if (resultPaquetes!=null)
            {
                paquete.Paquetes=resultPaquetes;
                paquete.EstatusEntrega.EstatusEntregas = resultEstatusEntrega;
                //usuario
                paquete.Usuario.Nombre=usuario.Nombre;
                paquete.EstatusEntrega.Estatus = estatusEntrega.Estatus;
            }
            else
            {
                ViewBag.Message=resultPaquetes.ToString();
            }
            return View(paquete);
        }


        [HttpPost]
        public ActionResult CrearPaquete(BL.Paquete paquete, string email,BL.Usuario usuario,BL.EstatusEntrega estatusEntrega)
        {
            if (paquete!=null && email!=null)
            {
                if (usuario.Nombre==null)
                {
                    usuario.Nombre = "";
                }
                if (estatusEntrega.Estatus==null)
                {
                    estatusEntrega.Estatus = "";
                }
                List<object> resultado = BL.Paquete.GetAll(usuario,estatusEntrega);
                usuario =new  BL.Usuario();
                usuario.Usuarios=resultado;

                return View()
            }
            else
            {
                //mail , string email
                string emailOrigen = "msandovalg00@gmail.com";
                MailMessage mailMessage = new MailMessage(emailOrigen, email, "Se registro paquete", "<p>Correo para asignar paquete</p>");
                mailMessage.IsBodyHtml = true;

                var path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "C:\\Users\\digis\\OneDrive\\Documents\\Mauricio Sandoval\\MSandovalTrackingandTrace\\PL\\Content\\Template\\Email.html");
                string contenidoHTML = System.IO.File.ReadAllText(path, Encoding.Default); ;

                mailMessage.Body = contenidoHTML;
                string url = "http://localhost:6292/";

                mailMessage.Body = mailMessage.Body.Replace("{Url}", url);
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, "wsgwmslplimaicmr");

                smtpClient.Send(mailMessage);
                smtpClient.Dispose();

                //ViewBag.Modal = "show";
                //ViewBag.Mensaje = "Se ha enviado el correo de confirmación a tu correo electronico";

                //Add
                if (paquete.IdPaquete == 0)//Add
                {
                    bool result = BL.Paquete.Add(paquete);
                    if (result)
                    {
                        ViewBag.Mensaje = "Se registro el paquete, y se ha enviado el correo de confirmación a tu correo electronico proporcionado.";
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
}