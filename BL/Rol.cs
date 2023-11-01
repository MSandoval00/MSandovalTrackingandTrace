using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string Tipo { get; set; }
        public List<object> Roles { get; set; }
        public static List<object> GetAll()
        {
            List<object> listRoles = new List<object>();
            try
            {
                using (DL.MSandovalTrackingandTraceEntities context=new DL.MSandovalTrackingandTraceEntities())
                {
                    var query = context.RolGetAll().ToList();
                    foreach (var row in query)
                    {
                        Rol rol = new Rol();
                        rol.IdRol = row.IdRol;
                        rol.Tipo = row.Tipo;
                        listRoles.Add(rol);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listRoles;
        }
       
    }
}
