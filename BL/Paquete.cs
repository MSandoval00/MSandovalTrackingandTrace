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
        public DateTime FechaEstimada { get; set; }
        public string CodigoRastreo { get; set; }
        public static object GetByCodigoRastreo(string CodigoRastreo)
        {
            object objCodigoRastreo = new object();
            try
            {
                using (DL.MSandovalTrackingandTraceEntities context=new DL.MSandovalTrackingandTraceEntities())
                {

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return objCodigoRastreo;
        }
    }
}
