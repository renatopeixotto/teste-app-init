using PedidosNet.Models.DAO;
using PedidosNet.Models.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PedidosNet.Models.BE
{
    public class PedidoBE : AbstractBE
    {
        public PedidoBE() : base() { }

        public PedidoBE(SqlCommand sqlComm) : base(sqlComm) { } 

        public long Alterar(PedidoVO objVO)
        {
            PedidoDAO dao = null;

            try
            {
                dao = new PedidoDAO(GetSqlCommand());
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

        public void Deletar(PedidoVO objVO)
        {
            PedidoDAO dao = null;

            try
            {
                dao = new PedidoDAO(GetSqlCommand());
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

        public long Inserir(PedidoVO objVO)
        {
            PedidoDAO dao = null;
            PedidoVO produtoVO = null;

            try
            {
                dao = new PedidoDAO(GetSqlCommand());
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

        public long FinalizarPedido(PedidoVO objVO)
        {
            PedidoDAO dao = null;

            try
            {
                dao = new PedidoDAO(GetSqlCommand());
                BeginTransaction();
                var id = dao.FinalizarPedido(objVO);
                Commit();
                return id;
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        public List<PedidoVO> Selecionar(PedidoVO objVO = null)
        {
            PedidoDAO dao = null;
            try
            {
                dao = new PedidoDAO(GetSqlCommand());
                return dao.Selecionar(objVO);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public PedidoVO Consultar(PedidoVO objVO)
        {
            PedidoDAO dao = null;
            try
            {
                dao = new PedidoDAO(GetSqlCommand());
                return dao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
