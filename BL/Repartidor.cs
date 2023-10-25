using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Repartidor
    {
        public int IdRepartidor { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public BL.UnidadEntrega UnidadEntrega { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Fotografía { get; set; }
        public List<object> Repartidores { get; set; }
        public bool Correct { get; set; }
        public string ErrorMessage { get; set; }
        public Exception Ex { get; set; }
        public Object Object { get; set; }
        public List<object> Objects { get; set; }

        public static Repartidor GetAll()
        {
            Repartidor result= new Repartidor();
            try
            {
                using (DL.MSandovalTrackingandTraceEntities context= new DL.MSandovalTrackingandTraceEntities())
                {
                    var query = (from tablaRepartidor in context.Repartidors
                                 join tablaUnidadEntrega in context.UnidadEntregas
                                 on tablaRepartidor.IdUnidadAsignada equals tablaUnidadEntrega.IdUnidad
                                 select new
                                 {
                                     IdRepartidor=tablaRepartidor.IdRepartidor,
                                     Nombre=tablaRepartidor.Nombre,
                                     ApellidoPaterno=tablaRepartidor.ApellidoPaterno,
                                     ApellidoMaterno=tablaRepartidor.ApellidoMaterno,
                                     IdUnidadAsignada=tablaUnidadEntrega.IdUnidad,
                                     Telefono=tablaRepartidor.Telefono,
                                     FechaIngreso=tablaRepartidor.FechaInreso,
                                     Fotografia=tablaRepartidor.Fotografia
                                 });
                    result.Objects = new List<object>();
                    if (query!=null&&query.Count()>0)
                    {
                        foreach (var datos in query)
                        {
                            Repartidor repartidor = new Repartidor();
                            repartidor.UnidadEntrega = new UnidadEntrega();

                            repartidor.IdRepartidor = datos.IdRepartidor;
                            repartidor.Nombre=datos.Nombre;
                            repartidor.ApellidoPaterno=datos.ApellidoPaterno;
                            repartidor.ApellidoMaterno=datos.ApellidoMaterno;
                            repartidor.UnidadEntrega.IdUnidad = datos.IdUnidadAsignada;
                            repartidor.Telefono = datos.Telefono;
                            repartidor.FechaIngreso = DateTime.Parse(datos.FechaIngreso.ToString());
                            repartidor.Fotografía = datos.Fotografia;

                            result.Objects.Add(repartidor);

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron datos del repartidor.";
                    }

                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage=ex.Message;
                result.Ex = ex;     
            }
            return result;
        }
    }
}
