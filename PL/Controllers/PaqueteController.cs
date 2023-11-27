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
            //BL.Usuario usuario=new BL.Usuario();
            //usuario.Nombre = "";
            //BL.EstatusEntrega estatusEntrega=new BL.EstatusEntrega();
            //estatusEntrega.IdEstatus = 0;

            List<object> resultPaquetes = BL.Paquete.GetAllSin();//usuario estatusentrega
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
            }
            else
            {
                ViewBag.Message=resultPaquetes.ToString();
            }
            return View(paquete);
        }


        [HttpPost]
        public ActionResult CrearPaquete(BL.Paquete paquete, string email,BL.EstatusEntrega estatusEntrega, BL.Usuario usuario)
        {
            //paquete.Repartidor = new BL.Repartidor();
            if (paquete.Repartidor.Usuario.Nombre!=null || estatusEntrega.IdEstatus!=0)
            {
                if (paquete.Repartidor.Usuario.Nombre==null)
                {
                    paquete.Repartidor.Usuario.Nombre = "";
                }
                if (estatusEntrega.IdEstatus == 0)
                {
                    estatusEntrega.IdEstatus = 0;
                }
                usuario.Nombre = paquete.Repartidor.Usuario.Nombre;
                estatusEntrega.IdEstatus = paquete.EstatusEntrega.IdEstatus;
                List<object> resultado = BL.Paquete.GetAll(usuario,estatusEntrega);
                paquete.Paquetes = new List<object>();
                paquete.Paquetes = resultado;

                //Lista de EstatusEntrega
                List<object> resultEstatusEntrega = BL.EstatusEntrega.GetAll();
                paquete.EstatusEntrega = new BL.EstatusEntrega();
                paquete.EstatusEntrega.EstatusEntregas = new List<object>();
                if (resultEstatusEntrega!=null)
                {
                    paquete.EstatusEntrega.EstatusEntregas=resultEstatusEntrega;
                }
                else
                {
                    ViewBag.Message = resultEstatusEntrega.ToString();
                }

                return View(paquete);
            }
            else if (paquete.Repartidor.Usuario.Nombre==null && estatusEntrega.IdEstatus==0)
            {
                ////Vacias
                //usuario.Nombre = "";
                //estatusEntrega.IdEstatus=0;
                
                //GetAll
                List<object> resultado = BL.Paquete.GetAllSin();
                //Lista de EstatusEntrega
                List<object> resultEstatusEntrega = BL.EstatusEntrega.GetAll();

                paquete.Paquetes = new List<object>();
                //instancia
                paquete.EstatusEntrega = new BL.EstatusEntrega();
                //paquete.Repartidor.Usuario = new BL.Usuario();

                paquete.EstatusEntrega.EstatusEntregas = new List<object>();
                if (resultado != null)
                {
                    paquete.Paquetes = resultado;
                    paquete.EstatusEntrega.EstatusEntregas = resultEstatusEntrega;
                }
                else
                {
                    ViewBag.Message = resultado.ToString();
                }

                return View(paquete);
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

        public BL.Apoyo GetPaquete(BL.Apoyo apoyo)
        {
            var paqueteSession = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Paquete"));
            foreach (var obj in paqueteSession)
            {
                BL.Paquete objPaquete=Newtonsoft.Json.JsonConvert.DeserializeObject<BL.Paquete>
            }
        }
        public ActionResult GeneratePDF()
        {
            BL.Paquete paquete = new BL.Paquete();
            paquete.Paquetes = new List<object>();

        }
    }
}