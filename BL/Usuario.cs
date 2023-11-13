using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
        public static List<object> GetAll()
        {
            List<object> listaUsuario = new List<object>();
            try
            {
                using (DL.MSandovalTrackingandTraceEntities context = new DL.MSandovalTrackingandTraceEntities())
                {
                    var query = context.UsuarioGetAll().ToList();
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

                        listaUsuario.Add(usuario);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listaUsuario;
        }
        public static object GetById(int IdUsuario)
        {
            object ObjectUsuario = new object();
            try
            {
                using (DL.MSandovalTrackingandTraceEntities context = new DL.MSandovalTrackingandTraceEntities())
                {
                    var query = context.UsuarioGetById(IdUsuario).Single();
                    ObjectUsuario = new List<object>();
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
                        ObjectUsuario=usuario;

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ObjectUsuario;
        }
        public static object UsuarioGetByEmail(string Email)
        {  
            object ObjectUsuario = new object();
            //bool ObjectUsusarioCorrect =new bool();
  
            try
            {
                using (DL.MSandovalTrackingandTraceEntities context= new DL.MSandovalTrackingandTraceEntities())
                {
                    var query = context.UsuarioGetByEmail(Email).FirstOrDefault();
                    ObjectUsuario=new List<object>();
                    if (query!=null)
                    {
                        Usuario usuario=new Usuario();
                        usuario.IdUsuario=query.IdUsuario;
                        usuario.Email=  query.Email;
                        usuario.Password= query.Password;
                        usuario.Rol = new Rol();
                        usuario.Rol.IdRol=query.IdRol;
                        usuario.Rol.Tipo=query.Tipo;
                        ObjectUsuario = usuario;
                        //ObjectUsusarioCorrect=true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ObjectUsuario;
        }
        public static bool Add(Usuario usuario)
        {
            bool CorrectUsuario=new bool();
            try
            {
                using (DL.MSandovalTrackingandTraceEntities context =new DL.MSandovalTrackingandTraceEntities())
                {
                    var query = context.UsuarioAdd(usuario.UserName,
                        usuario.Password,
                        usuario.Rol.IdRol,
                        usuario.Email,
                        usuario.Nombre,
                        usuario.ApellidoPaterno,
                        usuario.ApellidoMaterno);
                    if (query>0)
                    {
                        CorrectUsuario = true;
                    }
                    else
                    {
                        CorrectUsuario=false;
                        Console.WriteLine("El usuario no fue agregado");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return CorrectUsuario;
        }
        public static bool Update(Usuario usuario)
        {
            bool CorrectUsuario = new bool();
            try
            {
                using (DL.MSandovalTrackingandTraceEntities context=new DL.MSandovalTrackingandTraceEntities())
                {
                    var query = context.UsuarioUpdate(
                        usuario.IdUsuario,
                        usuario.UserName,
                        usuario.Password,
                        usuario.Rol.IdRol,
                        usuario.Email,
                        usuario.Nombre,
                        usuario.ApellidoPaterno,
                        usuario.ApellidoMaterno);
                    if (query>0)
                    {
                        CorrectUsuario = true;
                    }
                    else
                    {
                        CorrectUsuario=false;
                        Console.WriteLine("No se pudo actualizar el usuario");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return CorrectUsuario;
        }
        public static bool Delete(int IdUsuario)
        {
            bool correctUsuario = new bool();
            try
            {
                using (DL.MSandovalTrackingandTraceEntities context=new DL.MSandovalTrackingandTraceEntities())
                {
                    var query = context.UsuarioDelete(IdUsuario);
                    if (query>0)
                    {
                        correctUsuario = true;
                    }
                    else
                    {
                        correctUsuario=false;
                        Console.WriteLine("No se pudo eliminar el usuario");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return correctUsuario;
        }
        //public static string DecryptData(string password)
        //{
        //    password = password.Replace(" ", "+");
        //    int mod4 = password.Length % 4;
        //    if (mod4 > 0)
        //    {
        //        password += new string('=', 4 - mod4);
        //    }
        //    byte[] encryptedBytes = Convert.FromBase64String(password);
        //    MemoryStream ms = new MemoryStream();
        //    CryptoStream decStream = new CryptoStream(ms, (ICryptoTransform)TripleDESCryptoServiceProvider.Create(), CryptoStreamMode.Write);
        //    decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
        //    decStream.FlushFinalBlock();
        //    return Encoding.UTF8.GetString(ms.ToArray());
        //}

    }
}
