using PedidosNet.Models.DAO;
using PedidosNet.Models.VO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PedidosNet.Models.BE
{
    public class PedidoProdutoBE : AbstractBE
    {
        public PedidoProdutoBE() : base() { }

        public PedidoProdutoBE(SqlCommand sqlComm) : base(sqlComm) { } 

        public long Alterar(PedidoProdutoVO objVO)
        {
            PedidoProdutoDAO dao = null;

            try
            {
                dao = new PedidoProdutoDAO(GetSqlCommand());
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

        public void Deletar(PedidoProdutoVO objVO)
        {
            PedidoProdutoDAO dao = null;

            try
            {
                dao = new PedidoProdutoDAO(GetSqlCommand());
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

        public long Inserir(PedidoProdutoVO objVO)
        {
            PedidoProdutoDAO dao = null;
            PedidoProdutoVO pedidoProdutoVO = null;

            try
            {
                dao = new PedidoProdutoDAO(GetSqlCommand());
                BeginTransaction();
                dao.Inserir(objVO);
                Commit();
                pedidoProdutoVO = dao.ConsultarMax();
                return pedidoProdutoVO.Id;               
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        public long InserirPedidoProduto(PedidoProdutoVO objVO)
        {
            PedidoDAO pDao = null;
            PedidoProdutoDAO ppDao = null;
            PedidoVO pedidoVo = null;
            PedidoProdutoVO pedidoProdutoVO = null;

            try
            {
                pDao = new PedidoDAO(GetSqlCommand());
                ppDao = new PedidoProdutoDAO(GetSqlCommand());

                objVO.ValorUnitario = objVO.Produto.ValorUnitario;

                BeginTransaction();

                //Se houver pedido em aberto insere apenas o produto no pedido
                if (objVO.Pedido.Id > 0)
                {
                    ppDao.Inserir(objVO);
                }
                //Senao insere um novo pedido e o primeiro produto
                else
                {               
                    pDao.Inserir(objVO.Pedido);
                    pedidoVo = pDao.ConsultarMax();

                    if (pedidoVo != null)
                    {
                        objVO.Pedido.Id = pedidoVo.Id;                     
                        ppDao.Inserir(objVO);
                    }
                }

                Commit();
                pedidoProdutoVO = ppDao.ConsultarMax();
                return pedidoProdutoVO.Id;
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        public List<PedidoProdutoVO> Selecionar(PedidoProdutoVO objVO = null)
        {
            PedidoProdutoDAO dao = null;
            try
            {
                dao = new PedidoProdutoDAO(GetSqlCommand());
                return dao.Selecionar(objVO);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public PedidoProdutoVO Consultar(PedidoProdutoVO objVO)
        {
            PedidoProdutoDAO dao = null;
            try
            {
                dao = new PedidoProdutoDAO(GetSqlCommand());
                return dao.Consultar(objVO);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
