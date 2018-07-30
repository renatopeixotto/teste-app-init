using System;
using System.Data.SqlClient;

namespace PedidosNet.Models
{
    public abstract class AbstractBE : SqlServerConnection
    {
        protected AbstractBE() : base()
        {
            //"SSPI"
        }

        protected AbstractBE(SqlCommand sqlComm)  
            : base(sqlComm)
        {

        }

    }
}