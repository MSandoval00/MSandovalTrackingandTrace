using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Paquete
    {
        public int IdPaquete { get; set; }
        public string Detalle { get; set; }
        public float Peso { get; set; }
        public string DireccionOrigen { get; set; }
        public string DireccionEntrega { get; set; }
        public DateTime FechaEstimadaEntrega { get; set; }
        public string CodigoRastreo { get; set; }
        public List<object> Paquetes { get; set; }

        public BL.Entrega Entrega { get; set; }
        public BL.Repartidor Repartidor { get; set; }
        public BL.EstatusEntrega EstatusEntrega { get; set; }
        public BL.Usuario Usuario { get; set; }
        public static object GetByCodigoRastreo(string CodigoRastreo)
        {
            object objCodigoRastreo = new object();
            try
            {
                using (DL.MSandovalTrackingandTraceEntities context=new DL.MSandovalTrackingandTraceEntities())
                {
                    var query = context.EntregaGetByCodigoRastreo(CodigoRastreo).Single();
                    objCodigoRastreo = new List<object>();
                    if (query!=null)
                    {
                        Paquete paquete = new Paquete();
                        paquete.Entrega=new Entrega();
                        paquete.Repartidor=new Repartidor();
                        paquete.EstatusEntrega = new EstatusEntrega();
                        paquete.Usuario = new Usuario();
                        
                        paquete.IdPaquete = query.IdPaquete;
                        paquete.Detalle=query.Detalle;
                        paquete.Peso =float.Parse(query.Peso.ToString());
                        paquete.DireccionOrigen=query.DireccionOrigen;
                        paquete.DireccionEntrega = query.DireccionEntrega;
                        paquete.FechaEstimadaEntrega = DateTime.Parse(query.FechaEstimadaEntrega.ToString());
                        paquete.CodigoRastreo = query.CodigoRastreo;

                        //Entrega
                        paquete.Entrega.IdEntrega = query.IdEntrega;
                        //paquete.Entrega.FechaEntrega = DateTime.Parse(query.FechaEntrega.ToString());

                        //Repartidor
                        paquete.Repartidor.IdRepartidor = query.IdRepartidor;
                        paquete.Repartidor.Telefono = query.Telefono;

                        //EstatusEntrega
                        paquete.EstatusEntrega.IdEstatus = query.IdEstatus;
                        paquete.EstatusEntrega.Estatus=query.Estatus;

                        //Usuario
                        paquete.Usuario.IdUsuario = query.IdUsuario;
                        paquete.Usuario.Nombre = query.Nombre;
                        paquete.Usuario.ApellidoPaterno = query.ApellidoPaterno;
                        paquete.Usuario.ApellidoMaterno = query.ApellidoMaterno;
                        

                        objCodigoRastreo = paquete;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return objCodigoRastreo;
        }
        public static bool Add(Paquete paquete)
        {
            bool Correct=new bool();
            try
            {
                using (DL.MSandovalTrackingandTraceEntities context=new DL.MSandovalTrackingandTraceEntities())
                {
                    var query = context.PaqueteAdd(paquete.Detalle,
                        paquete.Peso,
                        paquete.DireccionOrigen,
                        paquete.DireccionEntrega);
                    if (query>0)
                    {
                        Correct = true;
                    }
                    else
                    {
                        Correct = false;
                        Console.WriteLine("El paquete no fue agregado");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Correct;
        }
        public static List<object> GetAll(Usuario usuario, EstatusEntrega estatusEntrega)
        {
            List<object> listPaquetes=new List<object> ();
            try
            {
                using (DL.MSandovalTrackingandTraceEntities context=new DL.MSandovalTrackingandTraceEntities())
                {
                    var query = (from tablaPaquete in context.Paquetes
                                 join tablaEntrega in context.Entregas
                                 on tablaPaquete.IdPaquete equals tablaEntrega.IdPaquete//Paquete
                                 join tablaRepartidor in context.Repartidors
                                 on tablaEntrega.IdRepartidor equals tablaRepartidor.IdRepartidor//Repartidor
                                 join tablaEstatusEntrega in context.EstatusEntregas
                                 on tablaEntrega.IdEstatusEntrega equals tablaEstatusEntrega.IdEstatus//EstatusEntrega
                                 join tablaUsuario in context.Usuarios
                                 on tablaRepartidor.IdUsuario equals tablaUsuario.IdUsuario//Usuario
                                 join tablaUnidadEntrega in context.UnidadEntregas
                                 on tablaRepartidor.IdUnidadAsignada equals tablaUnidadEntrega.IdUnidad//UnidadEntrega
                                where tablaUsuario.Nombre.Contains(usuario.Nombre) || tablaEstatusEntrega.Estatus.Contains(estatusEntrega.Estatus)
                                 select new
                                 {
                                     //Paquete
                                     IdPaquete=tablaPaquete.IdPaquete,
                                     DireccionOrigen=tablaPaquete.DireccionOrigen,
                                     DireccionEntrega=tablaPaquete.DireccionEntrega,
                                     FechaEstimadaEntrega=tablaPaquete.FechaEstimadaEntrega,
                                     
                                     //Repartidor
                                     IdRepartidor=tablaRepartidor.IdRepartidor,

                                     //EstatusEntrega 
                                     Estatus=tablaEstatusEntrega.Estatus,
                                     
                                     //Usuario
                                     Nombre=tablaUsuario.Nombre,
                                     ApellidoPaterno=tablaUsuario.ApellidoPaterno,
                                     ApellidoMaterno=tablaUsuario.ApellidoMaterno,

                                     //UnidadEntrega
                                     IdUnidad=tablaUnidadEntrega.IdUnidad,
                                     NumeroPlaca=tablaUnidadEntrega.NumeroPlaca,
                                 });
                    listPaquetes = new List<object>();
                    if (query!=null &&query.Count()>0)
                    {
                        foreach (var datos in query)
                        {
                            Paquete paquete = new Paquete();
                            paquete.Repartidor = new Repartidor();
                            paquete.Usuario=new Usuario();
                            paquete.EstatusEntrega=new EstatusEntrega();
                            paquete.Repartidor.UnidadEntrega = new UnidadEntrega();

                            paquete.IdPaquete=datos.IdPaquete;
                            paquete.DireccionOrigen=datos.DireccionOrigen;
                            paquete.DireccionEntrega = datos.DireccionEntrega;
                            paquete.FechaEstimadaEntrega = DateTime.Parse(datos.FechaEstimadaEntrega.ToString());

                            //Repartidor
                            paquete.Repartidor.IdRepartidor=datos.IdRepartidor;

                            //EstatusEntrega
                            paquete.EstatusEntrega.Estatus = datos.Estatus;

                            //Usuario
                            paquete.Usuario.Nombre = datos.Nombre;
                            paquete.Usuario.ApellidoPaterno = datos.ApellidoPaterno;
                            paquete.Usuario.ApellidoMaterno = datos.ApellidoMaterno;
                            
                            //UnidadEntrega
                            paquete.Repartidor.UnidadEntrega.IdUnidad=datos.IdUnidad;
                            paquete.Repartidor.UnidadEntrega.NumeroPlaca = datos.NumeroPlaca;
                            listPaquetes.Add(paquete);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No se encontraron registros del paquete");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listPaquetes;
        }
    }
}
