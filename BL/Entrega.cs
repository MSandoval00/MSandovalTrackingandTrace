using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Entrega
    {
        public int IdEntrega { get; set; }
        public BL.Paquete Paquete { get; set; }
        public BL.Repartidor Repartidor { get; set; }
        public DateTime FechaEntrega { get; set; }
        public BL.EstatusEntrega EstatusEntrega { get; set; }
        
    }
}
