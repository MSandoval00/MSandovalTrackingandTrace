using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EstatusUnidad
    {
        public int IdEstatus { get; set; }
        public string Estatus { get; set; }
        public List<object> EstatusUnidads { get; set; }
        public static List<object> GetAll()
        {
            List<object> ListEstatusUnidad = new List<object>();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "EstatusUnidadGetAll";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tablaEstatusUnidad = new DataTable();
                    adapter.Fill(tablaEstatusUnidad);

                    if (tablaEstatusUnidad.Rows.Count > 0)
                    {
                        ListEstatusUnidad = new List<object>();
                        foreach (DataRow row in tablaEstatusUnidad.Rows)
                        {
                            EstatusUnidad estatusUnidad = new EstatusUnidad();

                            estatusUnidad.IdEstatus = int.Parse(row[0].ToString());
                            estatusUnidad.Estatus = row[1].ToString();
                            ListEstatusUnidad.Add(estatusUnidad);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No hay registro por mostrar");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ListEstatusUnidad;
        }

    }
}
