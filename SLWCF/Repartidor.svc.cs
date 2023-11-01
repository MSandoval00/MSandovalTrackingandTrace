using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SLWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Repartidor" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Repartidor.svc or Repartidor.svc.cs at the Solution Explorer and start debugging.
    public class Repartidor : IRepartidor
    {
        public SLWCF.Result GetAll()
        {
            List<object> result=BL.Repartidor.GetAll();
            return new SLWCF.Result
            {
                Objects = result
            };
        }
        public SLWCF.Result UnidadEntregaGetAll()
        {
            List<object> result=BL.UnidadEntrega.GetAll();
            return new SLWCF.Result
            {
                Objects = result
            };
        }
        public SLWCF.Result GetById(int IdRepartidor)
        {
            object result=BL.Repartidor.GetById(IdRepartidor);
            return new SLWCF.Result
            {
                Object = result
            };
        }
        public SLWCF.Result Add(BL.Repartidor repartidor)
        {
            bool result=BL.Repartidor.Add(repartidor);
            return new SLWCF.Result { 
                Correct = result 
            };
        }
        public SLWCF.Result Update(BL.Repartidor repartidor)
        {
            bool result=BL.Repartidor.Update(repartidor);
            return new SLWCF.Result
            {
                Correct = result
            };
        }
        public SLWCF.Result Delete(int IdRepartidor)
        {
            bool result = BL.Repartidor.Delete(IdRepartidor);
            return new SLWCF.Result
            {
                Correct=result
            };
        }
    }
}
