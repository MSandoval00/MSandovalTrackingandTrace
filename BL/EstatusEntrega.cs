using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EstatusEntrega
    {
        public int IdEstatus { get; set; }
        public string Estatus { get; set; }
        public List<object> EstatusEntregas { get; set; }

        public static List<object> GetAll()
        {
            List<object> listEstatusEntregas = new List<object>();
            try
            {
                using (DL.MSandovalTrackingandTraceEntities context =new DL.MSandovalTrackingandTraceEntities())
                {
                    var query = context.EstatusEntregaGetAll().ToList();
                    foreach (var row in query)
                    {
                        EstatusEntrega estatusEntrega = new EstatusEntrega();
                        estatusEntrega.IdEstatus = row.IdEstatus;
                        estatusEntrega.Estatus = row.Estatus;
                        listEstatusEntregas.Add(estatusEntrega);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listEstatusEntregas;
        }
    }
}
