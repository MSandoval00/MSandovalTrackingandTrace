using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UnidadEntrega
    {
        public int IdUnidad { get; set; }
        public string NumeroPlaca { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string AnioFabricacion { get; set; }
        public BL.EstatusUnidad EstatusUnidad { get; set; }
        public List<object> UnidadEntregas { get; set; }
        
        public static List<object> GetAll()
        {
            List<object> listUnidadEntrega = new List<object>();
            try
            {
                using (SqlConnection context =new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "UnidadEntregaGetAll";
                    SqlCommand cmd=new SqlCommand(query,context);
                    cmd.CommandType =CommandType.StoredProcedure;
                    
                    SqlDataAdapter adapter=new SqlDataAdapter(cmd);
                    DataTable tablaUnidadEntrega=new DataTable();
                    adapter.Fill(tablaUnidadEntrega);

                    if (tablaUnidadEntrega.Rows.Count>0)
                    {
                        listUnidadEntrega = new List<object>();
                        foreach (DataRow row in tablaUnidadEntrega.Rows)
                        {
                            UnidadEntrega unidadEntrega = new UnidadEntrega();
                            unidadEntrega.EstatusUnidad=new BL.EstatusUnidad();

                            unidadEntrega.IdUnidad=int.Parse(row[0].ToString());
                            unidadEntrega.NumeroPlaca=row[1].ToString();
                            unidadEntrega.Modelo=row[2].ToString();
                            unidadEntrega.Marca=row[3].ToString();
                            unidadEntrega.AnioFabricacion=row[4].ToString();
                            unidadEntrega.EstatusUnidad.IdEstatus = int.Parse(row[5].ToString());
                            unidadEntrega.EstatusUnidad.Estatus=row[6].ToString();

                            listUnidadEntrega.Add(unidadEntrega);
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
            return listUnidadEntrega;
        }
        public static object GetById(int IdUnidad)
        {
            object ObjectUnidadEntrega=new object();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "UnidadEntregaGetById";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tablaUnidadEntrega = new DataTable();

                    SqlParameter[] collection = new SqlParameter[1];
                    collection[0] = new SqlParameter("@IdUnidad", SqlDbType.Int);
                    collection[0].Value = IdUnidad;
                    cmd.Parameters.AddRange(collection);

                    adapter.Fill(tablaUnidadEntrega);
                    if (tablaUnidadEntrega.Rows.Count>0)
                    {
                        DataRow row = tablaUnidadEntrega.Rows[0];
                        UnidadEntrega unidadEntrega = new UnidadEntrega();
                        unidadEntrega.EstatusUnidad = new EstatusUnidad();
                        unidadEntrega.IdUnidad = int.Parse(row[0].ToString());
                        unidadEntrega.NumeroPlaca=row[1].ToString();
                        unidadEntrega.Modelo=row[2].ToString();
                        unidadEntrega.Marca=row[3].ToString();
                        unidadEntrega.AnioFabricacion=row[4].ToString();
                        unidadEntrega.EstatusUnidad.IdEstatus=int.Parse(row[5].ToString());
                        unidadEntrega.EstatusUnidad.Estatus=row[6].ToString();
                        ObjectUnidadEntrega = unidadEntrega;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ObjectUnidadEntrega;
        }
        public static bool Add(UnidadEntrega unidadEntrega)
        {
            bool UnidadEntregaCorrect=new bool();
            try
            {
                using (SqlConnection connect =new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    var query = "UnidadEntregaAdd";
                    SqlCommand cmd=new SqlCommand(query,connect);
                    SqlParameter[] collection = new SqlParameter[5];
                    cmd.CommandType = CommandType.StoredProcedure;

                    collection[0] = new SqlParameter("@NumeroPlaca", SqlDbType.VarChar);
                    collection[0].Value = unidadEntrega.NumeroPlaca;
                    collection[1] = new SqlParameter("@Modelo", SqlDbType.VarChar);
                    collection[1].Value = unidadEntrega.Modelo;
                    collection[2] = new SqlParameter("@Marca",SqlDbType.VarChar);
                    collection[2].Value = unidadEntrega.Marca;
                    collection[3] = new SqlParameter("@AnioFabricacion", SqlDbType.VarChar);
                    collection[3].Value = unidadEntrega.AnioFabricacion;
                    collection[4] = new SqlParameter("@IdEstatusUnidad", SqlDbType.Int);
                    collection[4].Value = unidadEntrega.EstatusUnidad.IdEstatus;

                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();
                    int filasAfectadas=cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        UnidadEntregaCorrect = true;
                    }
                    else
                    {
                        UnidadEntregaCorrect=false;
                        Console.WriteLine("Error no se puedo insertar");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return UnidadEntregaCorrect;
        }
        public static bool Update(UnidadEntrega unidadEntrega)
        {
            bool UnidadEntregaCorrect=new bool();
            try
            {
                using (SqlConnection context=new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    var query = "UnidadEntregaUpdate";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType=CommandType.StoredProcedure;
                    SqlParameter[] collection = new SqlParameter[6];

                    collection[0] = new SqlParameter("@IdUnidad", SqlDbType.Int);
                    collection[0].Value = unidadEntrega.IdUnidad;
                    collection[1] = new SqlParameter("@NumeroPlaca", SqlDbType.VarChar);
                    collection[1].Value = unidadEntrega.NumeroPlaca;
                    collection[2] = new SqlParameter("@Modelo", SqlDbType.VarChar);
                    collection[2].Value = unidadEntrega.Modelo;
                    collection[3] = new SqlParameter("@Marca", SqlDbType.VarChar);
                    collection[3].Value = unidadEntrega.Marca;
                    collection[4] = new SqlParameter("@AnioFabricacion", SqlDbType.VarChar);
                    collection[4].Value = unidadEntrega.AnioFabricacion;
                    collection[5] = new SqlParameter("@IdEstatusUnidad", SqlDbType.Int);
                    collection[5].Value = unidadEntrega.EstatusUnidad.IdEstatus;
                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();
                    int filasAfectadas=cmd.ExecuteNonQuery();
                    if (filasAfectadas>0)
                    {
                        UnidadEntregaCorrect = true;
                    }
                    else
                    {
                        UnidadEntregaCorrect = false;
                        Console.WriteLine("No se pudo actualizar la unidad de entrega");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return UnidadEntregaCorrect;
        }
        public static bool Delete(int IdUnidad)
        {
            bool UnidadEntregaCorrect=new bool();
            try
            {
                using (SqlConnection context =new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    var query = "UnidadEntregaDelete";
                    SqlCommand cmd =new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter[] collection = new SqlParameter[1];
                    collection[0] = new SqlParameter("@IdUnidad", SqlDbType.Int);
                    collection[0].Value = IdUnidad;
                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();

                    int filasAfectadas= cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        UnidadEntregaCorrect = true;
                    }else
                    {
                        UnidadEntregaCorrect=false;
                        Console.WriteLine("Error no se pudo eliminar la unidad");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return UnidadEntregaCorrect;

        }
    }
}
