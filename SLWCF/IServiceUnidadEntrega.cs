using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SLWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceUnidadEntrega" in both code and config file together.
    [ServiceContract]
    public interface IServiceUnidadEntrega
    {
        [OperationContract]
        [ServiceKnownType(typeof(BL.UnidadEntrega))]
        SLWCF.Result GetAll();
        //[OperationContract]
        //[ServiceKnownType(typeof(BL.UnidadEntrega))]
        //SLWCF.Result GetAllEstatusUnidad();
        [OperationContract]
        [ServiceKnownType(typeof(BL.UnidadEntrega))]
        SLWCF.Result GetById(int IdUnidad);
        [OperationContract]
        SLWCF.Result Add(BL.UnidadEntrega unidadEntrega);
        [OperationContract]
        SLWCF.Result Update(BL.UnidadEntrega unidadEntrega);
        [OperationContract]
        SLWCF.Result Delete(int IdUnidad);
    }
}
