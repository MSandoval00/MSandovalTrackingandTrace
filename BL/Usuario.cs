using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string UserName { get; set; }
        public byte[] Password { get; set; }
        public BL.Rol Rol { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public List<object> Usuarios { get; set; }
        public bool Correct { get; set; }
        public string ErrorMessage { get; set; }
        public object Object { get; set; }
        public List<object> Objects { get; set; }
        public Exception Ex { get; set; }
        public static BL.Usuario GetAll()
        {
            BL.Usuario result = new BL.Usuario();
            try
            {
                using (DL.MSandovalTrackingandTraceEntities context = new DL.MSandovalTrackingandTraceEntities())
                {
                    var query = context.UsuarioGetAll().ToList();
                    result.Objects = new List<object>();
                    foreach (var row in query)
                    {
                        BL.Usuario usuario = new BL.Usuario();
                        usuario.IdUsuario = row.IdUsuario;
                        usuario.UserName=row.UserName;
                        usuario.Password=row.Password;
                        usuario.Rol=new BL.Rol();
                        usuario.Rol.IdRol=row.IdRol;
                        usuario.Rol.Tipo=row.Tipo;
                        usuario.Email=row.Email;
                        usuario.Nombre=row.Nombre;
                        usuario.ApellidoPaterno=row.ApellidoPaterno;
                        usuario.ApellidoMaterno=row.ApellidoMaterno;

                        result.Objects.Add(usuario);
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static BL.Usuario GetById(int IdUsuario)
        {
            BL.Usuario result = new BL.Usuario();
            try
            {
                using (DL.MSandovalTrackingandTraceEntities context = new DL.MSandovalTrackingandTraceEntities())
                {
                    var query = context.UsuarioGetById(IdUsuario).Single();
                    result.Object = new List<object>();
                    if (query != null)
                    {
                        BL.Usuario usuario=new BL.Usuario();
                        usuario.IdUsuario=query.IdUsuario;
                        usuario.UserName=query.UserName;
                        usuario.Password=query.Password;
                        usuario.Rol=new Rol();
                        usuario.Rol.IdRol=query.IdRol;
                        usuario.Rol.Tipo=query.Tipo;
                        usuario.Email=query.Email;
                        usuario.Nombre=query.Nombre;
                        usuario.ApellidoPaterno=query.ApellidoPaterno;
                        usuario.ApellidoMaterno=query.ApellidoMaterno;
                        result.Object=usuario;
                        result.Correct=true;

                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage=ex.Message;
                result.Ex=ex;
            }
            return result;
        }
        public static  BL.Usuario UsuarioGetByEmail(string Email)
        {
            Usuario result=new Usuario();
            try
            {
                using (DL.MSandovalTrackingandTraceEntities context= new DL.MSandovalTrackingandTraceEntities())
                {
                    var query = context.UsuarioGetByEmail(Email).FirstOrDefault();
                    result.Object=new List<object>();
                    if (query!=null)
                    {
                        Usuario usuario=new Usuario();
                        usuario.IdUsuario=query.IdUsuario;
                        usuario.Email=  query.Email;
                        usuario.Password= query.Password;
                        result.Object = usuario;
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        

    }
}
