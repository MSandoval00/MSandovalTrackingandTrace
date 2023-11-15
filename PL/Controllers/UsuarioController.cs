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
        [HttpGet]
        public ActionResult GetAll()
        {
            List<object> resultUsuarios = BL.Usuario.GetAll();
            BL.Usuario usuario=new BL.Usuario();
            usuario.Usuarios=new List<object>();
            if (resultUsuarios!=null)
            {
                usuario.Usuarios = resultUsuarios;
            }
            else
            {
                ViewBag.Message=resultUsuarios.ToString();
            }
            return View(usuario);
        }
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

            BL.Usuario result = (BL.Usuario)BL.Usuario.UsuarioGetByEmail(Email);
            Session["Rol"] = result.Rol.Tipo;
            Session["IdUsuario"] = result.IdUsuario;
            if (result.Email!=null)
            {
                BL.Usuario usuario=(BL.Usuario)result;
                var storedPassword=usuario.Password;
                if (inputBytes.SequenceEqual(storedPassword))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {

            BL.Usuario usuario = new BL.Usuario();
            usuario.Rol = new BL.Rol();
            List<object> resultRol = BL.Rol.GetAll();
            if (IdUsuario!=null)//Update
            {
                object resultUsuario = BL.Usuario.GetById(IdUsuario.Value);
                if (resultUsuario != null)
                {
                    usuario = (BL.Usuario)resultUsuario;
                    usuario.Rol.Roles = resultRol;                   
                }
            }
            else//Add
            {
                usuario.Rol.Roles = resultRol;
            }
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Form(BL.Usuario usuario)
        {            
            if (usuario.IdUsuario==0)
            {
                bool result = BL.Usuario.Add(usuario);
                if (result)
                {
                    ViewBag.Mensaje = "Se agrego correctamente el ussuario";
                }
                else
                {
                    ViewBag.Mensaje = "No se pudo agregar el usuario";
                }
            }
            else
            {
                bool result = BL.Usuario.Update(usuario);
                if (result)
                {
                    ViewBag.Mensaje = "Se actualizo correctamente el usuario";
                }
                else
                {
                    ViewBag.Mensaje = "No se actualizo el usuario";
                }
            }
            return PartialView("Modal");
        }
        public ActionResult Delete(int IdUsuario)
        {
            bool result = BL.Usuario.Delete(IdUsuario);
            if (result)
            {
                ViewBag.Mensaje = "Usuario eliminado correctamente";
            }
            else
            {
                ViewBag.Mensaje = "El usuario no se pudo eliminar correctamente";
            }
            return PartialView("Modal");
        }
    }
}