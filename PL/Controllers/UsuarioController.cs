using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            byte[] inputBytes= Encoding.UTF8.GetBytes(Password);

            BL.Usuario result = BL.Usuario.UsuarioGetByEmail(Email);
            if (result.Correct)
            {
                BL.Usuario usuario=(BL.Usuario)result.Object;
                var storedPassword=usuario.Password;
                if (inputBytes.SequenceEqual(storedPassword))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
    }
}