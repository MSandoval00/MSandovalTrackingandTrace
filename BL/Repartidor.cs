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
        public BL.UnidadEntrega UnidadEntrega { get; set; }
        public string Telefono { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string Fotografía { get; set; }
        public BL.Usuario Usuario { get; set; }
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
                                 join tablaUsuario in context.Usuarios
                                 on tablaRepartidor.IdUsuario equals tablaUsuario.IdUsuario
                                 
                                 select new
                                 {
                                     IdRepartidor=tablaRepartidor.IdRepartidor,
                                     IdUnidadAsignada=tablaUnidadEntrega.IdUnidad,
                                     Telefono=tablaRepartidor.Telefono,
                                     FechaIngreso=tablaRepartidor.FechaIngreso,
                                     Fotografia=tablaRepartidor.Fotografia,
                                     IdUsuario=tablaUsuario.IdUsuario,
                                     Nombre=tablaUsuario.Nombre,
                                     ApellidoPaterno=tablaUsuario.ApellidoPaterno,
                                     ApellidoMaterno=tablaUsuario.ApellidoMaterno
                                 });
                    listRepartidor = new List<object>();
                    if (query!=null&&query.Count()>0)
                    {
                        foreach (var datos in query)
                        {
                            Repartidor repartidor = new Repartidor();
                            repartidor.UnidadEntrega = new UnidadEntrega();
                            repartidor.Usuario=new Usuario();
                            repartidor.IdRepartidor = datos.IdRepartidor;
                            repartidor.UnidadEntrega.IdUnidad = datos.IdUnidadAsignada;
                            repartidor.Telefono = datos.Telefono;
                            repartidor.FechaIngreso = DateTime.Parse(datos.FechaIngreso.ToString());
                            repartidor.Fotografía = datos.Fotografia;
                            repartidor.Usuario.IdUsuario = int.Parse(datos.IdUsuario.ToString());
                            repartidor.Usuario.Nombre = datos.Nombre;
                            repartidor.Usuario.ApellidoPaterno = datos.ApellidoPaterno;
                            repartidor.Usuario.ApellidoMaterno = datos.ApellidoMaterno;
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
                                 join tablaUsuario in context.Usuarios
                                 on tablaRepartidor.IdUsuario equals tablaUsuario.IdUsuario
                                 where tablaRepartidor.IdRepartidor==IdRepartidor
                                 select new
                                 {
                                     IdRepartidor = tablaRepartidor.IdRepartidor,
                                     IdUnidadAsignada = tablaUnidadEntrega.IdUnidad,
                                     Telefono = tablaRepartidor.Telefono,
                                     FechaIngreso = tablaRepartidor.FechaIngreso,
                                     Fotografia = tablaRepartidor.Fotografia,
                                     IdUsuario=tablaRepartidor.IdUsuario,
                                     Nombre=tablaUsuario.Nombre,
                                     ApellidoPaterno=tablaUsuario.ApellidoPaterno,
                                     ApellidoMaterno=tablaUsuario.ApellidoMaterno
                                 });
                    if (query!=null&& query.Count()>0)
                    {
                        BL.Repartidor repartidor=new BL.Repartidor();
                        repartidor.UnidadEntrega=new BL.UnidadEntrega();
                        repartidor.Usuario = new Usuario();

                        var queryDatos=query.ToList().Single();
                        repartidor.IdRepartidor = queryDatos.IdRepartidor;
                        repartidor.UnidadEntrega.IdUnidad = queryDatos.IdUnidadAsignada;
                        repartidor.Telefono=queryDatos.Telefono;
                        repartidor.FechaIngreso = DateTime.Parse(queryDatos.FechaIngreso.ToString());
                        repartidor.Fotografía = queryDatos.Fotografia;
                        repartidor.Usuario.IdUsuario = int.Parse(queryDatos.IdUsuario.ToString());
                        repartidor.Usuario.Nombre = queryDatos.Nombre;
                        repartidor.Usuario.ApellidoPaterno = queryDatos.ApellidoPaterno;
                        repartidor.Usuario.ApellidoMaterno = queryDatos.ApellidoMaterno;
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
                    nuevoRepartidor.IdUnidadAsignada=repartidor.UnidadEntrega.IdUnidad;
                    nuevoRepartidor.Telefono=repartidor.Telefono;
                    nuevoRepartidor.FechaIngreso=repartidor.FechaIngreso;
                    nuevoRepartidor.Fotografia = repartidor.Fotografía;
                    nuevoRepartidor.IdUsuario = repartidor.Usuario.IdUsuario;
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
                        query.IdUnidadAsignada = repartidor.UnidadEntrega.IdUnidad;
                        query.Telefono = repartidor.Telefono;
                        query.FechaIngreso=repartidor.FechaIngreso;
                        query.Fotografia = repartidor.Fotografía;
                        query.IdUsuario = repartidor.Usuario.IdUsuario;

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
        public static object UsuarioGetById(int IdUsuario)
        {
            object ObjectRepartidor = new object();
            try
            {
                using (DL.MSandovalTrackingandTraceEntities context = new DL.MSandovalTrackingandTraceEntities())
                {
                    var query = (from tablaRepartidor in context.Repartidors
                                 join tablaUnidadEntrega in context.UnidadEntregas
                                 on tablaRepartidor.IdUnidadAsignada equals tablaUnidadEntrega.IdUnidad
                                 join tablaUsuario in context.Usuarios
                                 on tablaRepartidor.IdUsuario equals tablaUsuario.IdUsuario
                                 
                                 where tablaRepartidor.IdUsuario == IdUsuario
                                 select new
                                 {
                                     //Repartidor
                                     IdRepartidor = tablaRepartidor.IdRepartidor,
                                     IdUnidadAsignada = tablaUnidadEntrega.IdUnidad,
                                     Telefono = tablaRepartidor.Telefono,
                                     IdUsuario = tablaRepartidor.IdUsuario,
                                     //Usuario
                                     Nombre = tablaUsuario.Nombre,
                                     ApellidoPaterno = tablaUsuario.ApellidoPaterno,
                                     ApellidoMaterno = tablaUsuario.ApellidoMaterno,
                                     //UnidadEntrega
                                     NumeroPlaca=tablaUnidadEntrega.NumeroPlaca,
                                     Modelo=tablaUnidadEntrega.Modelo,
                                     Marca=tablaUnidadEntrega.Marca,
                                     AnioFabricacion=tablaUnidadEntrega.AnioFabricacion,
                                     //EstatusUnidad
                                     IdEstatus=tablaUnidadEntrega.EstatusUnidad.IdEstatus,
                                     Estatus=tablaUnidadEntrega.EstatusUnidad.Estatus

                                 });
                    if (query != null && query.Count() > 0)
                    {
                        BL.Repartidor repartidor = new BL.Repartidor();
                        repartidor.UnidadEntrega = new BL.UnidadEntrega();
                        repartidor.Usuario = new Usuario();
                        repartidor.UnidadEntrega.EstatusUnidad = new EstatusUnidad();

                        var queryDatos = query.ToList().Single();
                        //Repartidor
                        repartidor.IdRepartidor = queryDatos.IdRepartidor;
                        repartidor.UnidadEntrega.IdUnidad = queryDatos.IdUnidadAsignada;
                        repartidor.Telefono = queryDatos.Telefono;
                        repartidor.Usuario.IdUsuario = int.Parse(queryDatos.IdUsuario.ToString());
                        //Usuario
                        repartidor.Usuario.Nombre = queryDatos.Nombre;
                        repartidor.Usuario.ApellidoPaterno = queryDatos.ApellidoPaterno;
                        repartidor.Usuario.ApellidoMaterno = queryDatos.ApellidoMaterno;
                        //UnidadEntrega
                        repartidor.UnidadEntrega.NumeroPlaca = queryDatos.NumeroPlaca;
                        repartidor.UnidadEntrega.Modelo = queryDatos.Modelo;
                        repartidor.UnidadEntrega.Marca = queryDatos.Marca;
                        repartidor.UnidadEntrega.AnioFabricacion = queryDatos.AnioFabricacion;
                        //EstatusUnidad
                        repartidor.UnidadEntrega.EstatusUnidad.IdEstatus=queryDatos.IdEstatus;
                        repartidor.UnidadEntrega.EstatusUnidad.Estatus = queryDatos.Estatus;
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
    }
}
