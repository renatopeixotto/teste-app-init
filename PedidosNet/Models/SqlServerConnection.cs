using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace PedidosNet.Models
{
    public abstract class SqlServerConnection
    {
        private SqlConnection SqlConn;
        private SqlTransaction SqlTrans;
        private SqlCommand SqlComm;
        private Random TransactionRandom = new Random();
        //private SqlCommand sqlComm;

        private readonly IConfiguration configuration;

        public SqlServerConnection()
        {
            AbrirConexao();
        }

        public SqlServerConnection(IConfiguration config)
        {
            this.configuration = config;
        }

        private void SetSqlCommand(SqlCommand sqlComm)
        {
            this.SqlComm = sqlComm;
        }

        public SqlCommand GetSqlCommand()
        {
            return SqlComm;
        }

        private void SetSqlConnection(SqlConnection sqlConn)
        {
            this.SqlConn = sqlConn;
        }

        private SqlConnection GetSqlConnection()
        {
            return SqlConn;
        }

        private SqlTransaction GetSqlTransaction()
        {
            return SqlTrans;
        }

        private void SetSqlTransaction(SqlTransaction sqlTrans)
        {
            SqlTrans = sqlTrans;
        }

        protected SqlServerConnection(SqlCommand sqlComm)
        {
            try
            {
                if (!sqlComm.Connection.State.Equals(ConnectionState.Open))
                {
                    throw new Exception("A conexão com o banco de dados não esta aberta!");
                }
            }
            catch (Exception e)
            {
                throw new Exception("A conexão com o banco de dados não esta aberta!", e);
            }

            if (sqlComm.Transaction != null)
                SetSqlTransaction(sqlComm.Transaction);

            SetSqlConnection(sqlComm.Connection);
            SetSqlCommand(sqlComm);
        }

        private void AbrirConexao()
        {
            SqlConnection sqlConn = null;
            SqlCommand sqlComm = null;
            try
            {
                if (sqlConn == null)
                {
                    sqlConn = new SqlConnection(GetStringConexaoBanco());
                    sqlConn.Open();
                    SetSqlConnection(sqlConn);
                    sqlComm = sqlConn.CreateCommand();
                    sqlComm.Connection = sqlConn;
                    sqlComm.CommandType = CommandType.Text;
                    sqlComm.CommandTimeout = 500;
                    sqlComm.Parameters.Clear();
                    SetSqlCommand(sqlComm);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possivel abrir conexão com o banco de dados.", e);
            }
        }

        private string GetStringConexaoBanco()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

                var config = builder.Build();

                var connectionString = config.GetConnectionString("DefaultConnectionString");

                return connectionString;              
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void FecharConexao()
        {
            try
            {
                if (GetSqlConnection() != null && GetSqlCommand() != null)
                {
                    GetSqlConnection().Close();
                    GetSqlCommand().Dispose();
                    SetSqlCommand(null);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possivel fechar conexão com o banco de dados!", e);
            }
        }

        protected void BeginTransaction()
        {
            SqlTransaction sqlTransaction = null;
            try
            {
                if (GetSqlTransaction() == null)
                {
                    sqlTransaction = GetSqlConnection().BeginTransaction("AbreTransacao" + TransactionRandom.Next(0, 100000));

                    SetSqlTransaction(sqlTransaction);
                    GetSqlCommand().Transaction = GetSqlTransaction();
                }

            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível abrir a transação com o banco de dados!", e);
            }
        }

        protected void Commit()
        {
            try
            {
                GetSqlTransaction().Commit();
                SetSqlTransaction(null);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        protected void Rollback()
        {
            try
            {
                GetSqlTransaction().Rollback();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
