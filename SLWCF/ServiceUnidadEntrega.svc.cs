using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SLWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceUnidadEntrega" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceUnidadEntrega.svc or ServiceUnidadEntrega.svc.cs at the Solution Explorer and start debugging.
    public class ServiceUnidadEntrega : IServiceUnidadEntrega
    {
        public SLWCF.Result Add(BL.UnidadEntrega unidadEntrega)
        {
            bool result=BL.UnidadEntrega.Add(unidadEntrega);
            return new SLWCF.Result
            {
                Correct=result
            };
        }
        public SLWCF.Result Update(BL.UnidadEntrega unidadEntrega)
        {
            bool result = BL.UnidadEntrega.Update(unidadEntrega);
            return new SLWCF.Result 
            {
            Correct = result
            };
        }
        public SLWCF.Result Delete(int IdUnidad)
        {
            bool result=BL.UnidadEntrega.Delete(IdUnidad);
            return new Result
            {
                Correct=result
            };
        }
        public SLWCF.Result GetAll()
        {
            List<object> result=BL.UnidadEntrega.GetAll();
            return new SLWCF.Result
            {
                Objects = result,
            };
        }
        public SLWCF.Result GetById(int IdUnidad)
        {
            object result=BL.UnidadEntrega.GetById(IdUnidad);
            return new SLWCF.Result
            {
                Object = result
            };
        }
    }
}
