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
        public DateTime? FechaIngreso { get; set; }
        public string Fotografía { get; set; }
        public List<object> Repartidores { get; set; }

       

        public static List<object>  GetAll()
        {
            //Repartidor result= new Repartidor();
            List<object> listRepartidor = new List<object>();
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
                                     FechaIngreso=tablaRepartidor.FechaIngreso,
                                     Fotografia=tablaRepartidor.Fotografia
                                 });
                    listRepartidor = new List<object>();
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
                            listRepartidor.Add(repartidor);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No se encontraron los datos del repartidor");
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listRepartidor;
        }
        public static object GetById(int IdRepartidor)
        {
            object ObjectRepartidor = new object();
            try
            {
                using (DL.MSandovalTrackingandTraceEntities context = new DL.MSandovalTrackingandTraceEntities())
                {
                    var query = (from tablaRepartidor in context.Repartidors
                                 join tablaUnidadEntrega in context.UnidadEntregas
                                 on tablaRepartidor.IdUnidadAsignada equals tablaUnidadEntrega.IdUnidad
                                 where tablaRepartidor.IdRepartidor==IdRepartidor
                                 select new
                                 {
                                     IdRepartidor = tablaRepartidor.IdRepartidor,
                                     Nombre = tablaRepartidor.Nombre,
                                     ApellidoPaterno = tablaRepartidor.ApellidoPaterno,
                                     ApellidoMaterno = tablaRepartidor.ApellidoMaterno,
                                     IdUnidadAsignada = tablaUnidadEntrega.IdUnidad,
                                     Telefono = tablaRepartidor.Telefono,
                                     FechaIngreso = tablaRepartidor.FechaIngreso,
                                     Fotografia = tablaRepartidor.Fotografia
                                 });
                    if (query!=null&& query.Count()>0)
                    {
                        BL.Repartidor repartidor=new BL.Repartidor();
                        repartidor.UnidadEntrega=new BL.UnidadEntrega();

                        var queryDatos=query.ToList().Single();
                        repartidor.IdRepartidor = queryDatos.IdRepartidor;
                        repartidor.Nombre = queryDatos.Nombre;
                        repartidor.ApellidoPaterno=queryDatos.ApellidoPaterno;
                        repartidor.ApellidoMaterno=queryDatos.ApellidoMaterno;
                        repartidor.UnidadEntrega.IdUnidad = queryDatos.IdUnidadAsignada;
                        repartidor.Telefono=queryDatos.Telefono;
                        repartidor.FechaIngreso = DateTime.Parse(queryDatos.FechaIngreso.ToString());
                        repartidor.Fotografía = queryDatos.Fotografia;
                        ObjectRepartidor = repartidor;

                    }
                    else
                    {
                        Console.WriteLine("No hay registros que coincidan");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ObjectRepartidor;
        }
        public static bool Add(Repartidor repartidor)
        {
            bool Correct=new bool();
            try
            {
                using (DL.MSandovalTrackingandTraceEntities context=new DL.MSandovalTrackingandTraceEntities())
                {
                    DL.Repartidor nuevoRepartidor = new DL.Repartidor();
                    nuevoRepartidor.Nombre=repartidor.Nombre;
                    nuevoRepartidor.ApellidoPaterno = repartidor.ApellidoPaterno;
                    nuevoRepartidor.ApellidoMaterno = repartidor.ApellidoMaterno;
                    nuevoRepartidor.IdUnidadAsignada=repartidor.UnidadEntrega.IdUnidad;
                    nuevoRepartidor.Telefono=repartidor.Telefono;
                    nuevoRepartidor.FechaIngreso=repartidor.FechaIngreso;
                    nuevoRepartidor.Fotografia = repartidor.Fotografía;
                    context.Repartidors.Add(nuevoRepartidor);
                    context.SaveChanges();

                }
                Correct=true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Correct = false;
            }
            return Correct;
        }
        public static bool Update(Repartidor repartidor)
        {
            bool Correct=new bool();
            try
            {
                using (DL.MSandovalTrackingandTraceEntities context=new DL.MSandovalTrackingandTraceEntities())
                {
                    var query = (from tableRepartidor in context.Repartidors
                                 where tableRepartidor.IdRepartidor == repartidor.IdRepartidor
                                 select tableRepartidor).SingleOrDefault();
                    if (query != null)
                    {
                        query.Nombre = repartidor.Nombre;
                        query.ApellidoPaterno=repartidor.ApellidoPaterno;
                        query.ApellidoMaterno=repartidor.ApellidoMaterno;
                        query.IdUnidadAsignada = repartidor.UnidadEntrega.IdUnidad;
                        query.Telefono = repartidor.Telefono;
                        query.FechaIngreso=repartidor.FechaIngreso;
                        query.Fotografia = repartidor.Fotografía;

                        context.SaveChanges();
                        Correct=true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Correct=false;

            }
            return Correct;
        }
        public static bool Delete(int IdRepartidor)
        {
            bool Correct = new bool();
            try
            {
                using (DL.MSandovalTrackingandTraceEntities context=new DL.MSandovalTrackingandTraceEntities())
                {
                    var query = (from tablaRepartidor in context.Repartidors
                                 where tablaRepartidor.IdRepartidor == IdRepartidor
                                 select tablaRepartidor).First();
                    context.Repartidors.Remove(query);
                    context.SaveChanges();
                    Correct = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Correct = false;
            }
            return Correct;
        }
    }
}
