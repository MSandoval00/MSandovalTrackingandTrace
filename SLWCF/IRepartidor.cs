using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SLWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRepartidor" in both code and config file together.
    [ServiceContract]
    public interface IRepartidor
    {
        [OperationContract]
        [ServiceKnownType(typeof(BL.Repartidor))]
        SLWCF.Result GetAll();
        [OperationContract]
        [ServiceKnownType(typeof(BL.UnidadEntrega))]
        SLWCF.Result UnidadEntregaGetAll();
        [OperationContract]
        [ServiceKnownType(typeof(BL.Usuario))]
        SLWCF.Result UsuarioGetAll();
        [OperationContract]
        [ServiceKnownType(typeof(BL.Repartidor))]
        SLWCF.Result GetById(int IdRepartidor);
        [OperationContract]
        SLWCF.Result Add(BL.Repartidor repartidor);
        [OperationContract]
        SLWCF.Result Update(BL.Repartidor repartidor);
        [OperationContract]
        SLWCF.Result Delete(int IdRepartidor);

    }
}
