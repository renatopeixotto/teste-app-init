using PedidosNet.Models.DAO;
using PedidosNet.Models.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PedidosNet.Models.BE
{
    public class ProdutoBE : AbstractBE
    {
        public ProdutoBE() : base() { }

        public ProdutoBE(SqlCommand sqlComm) : base(sqlComm) { }

        public long Alterar(ProdutoVO objVO)
        {
            ProdutoDAO dao = null;

            try
            {
                dao = new ProdutoDAO(GetSqlCommand());
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

        public void Deletar(ProdutoVO objVO)
        {
            ProdutoDAO dao = null;

            try
            {
                dao = new ProdutoDAO(GetSqlCommand());
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

        public long Inserir(ProdutoVO objVO)
        {
            ProdutoDAO dao = null;
            ProdutoVO produtoVO = null;

            try
            {
                dao = new ProdutoDAO(GetSqlCommand());
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

        public List<ProdutoVO> Selecionar(ProdutoVO objVO = null)
        {
            ProdutoDAO dao = null;
            try
            {
                dao = new ProdutoDAO(GetSqlCommand());
                return dao.Selecionar(objVO);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public ProdutoVO Consultar(ProdutoVO objVO)
        {
            ProdutoDAO dao = null;
            try
            {
                dao = new ProdutoDAO(GetSqlCommand());
                return dao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
