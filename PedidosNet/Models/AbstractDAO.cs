using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PedidosNet.Models
{
    public abstract class AbstractDAO
    {
        protected StringBuilder objSbSelect;
        protected StringBuilder objSbUpdate;
        protected StringBuilder objSbDelete;
        protected StringBuilder objSbInsert;
        private SqlCommand SqlComm;
        private SqlDataReader SqlDReader; 

        protected SqlCommand GetSqlCommand()
        {
            return SqlComm;
        }

        protected void SetSqlCommand(SqlCommand sqlComm)
        {
            this.SqlComm = sqlComm;
        }

        private void SetSqlDataReader(SqlDataReader sqlDataReader)
        {
            this.SqlDReader = sqlDataReader;
        }

        protected SqlDataReader GetSqlDataReader()
        {
            if (SqlDReader == null)
            {
                SqlDReader = GetSqlCommand().ExecuteReader();
            }

            return SqlDReader;
        }

        protected AbstractDAO(SqlCommand sqlComm)
        {
            SetSqlCommand(sqlComm);
        }

        protected void Close()
        {
            if (GetSqlDataReader() != null)
            {
                try
                {
                    GetSqlDataReader().Close();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    SetSqlDataReader(null);
                }
            }
        }

    }
}