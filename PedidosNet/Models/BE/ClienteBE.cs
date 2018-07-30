using PedidosNet.Models.DAO;
using PedidosNet.Models.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PedidosNet.Models.BE
{
    public class ClienteBE : AbstractBE
    {
        public ClienteBE() : base() { }

        public ClienteBE(SqlCommand sqlComm) : base(sqlComm) { } 

        public long Alterar(ClienteVO objVO)
        {
            ClienteDAO dao = null;

            try
            {
                dao = new ClienteDAO(GetSqlCommand());
                BeginTransaction();
                var id = dao.Alterar(objVO);
                Commit();
                return id;
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        public void Deletar(ClienteVO objVO)
        {
            ClienteDAO dao = null;

            try
            {
                dao = new ClienteDAO(GetSqlCommand());
                BeginTransaction();
                dao.Deletar(objVO);
                Commit();
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        public long Inserir(ClienteVO objVO)
        {
            ClienteDAO dao = null;
            ClienteVO produtoVO = null;

            try
            {
                dao = new ClienteDAO(GetSqlCommand());
                BeginTransaction();
                dao.Inserir(objVO);
                Commit();
                produtoVO = dao.ConsultarMax();
                return produtoVO.Id;               
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        public List<ClienteVO> Selecionar(ClienteVO objVO = null)
        {
            ClienteDAO dao = null;
            try
            {
                dao = new ClienteDAO(GetSqlCommand());
                return dao.Selecionar(objVO);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public ClienteVO Consultar(ClienteVO objVO)
        {
            ClienteDAO dao = null;
            try
            {
                dao = new ClienteDAO(GetSqlCommand());
                return dao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
