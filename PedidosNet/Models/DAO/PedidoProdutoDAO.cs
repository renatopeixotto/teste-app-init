using PedidosNet.Models.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PedidosNet.Models.DAO
{
    public class PedidoProdutoDAO : AbstractDAO
    {
        public PedidoProdutoDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Alterar(PedidoProdutoVO objVO)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"
                                         UPDATE DBPedidosNet.dbo.PedidoProduto                       
                                            SET                                                  
                                                  IdPedido = @IdPedido                          
                                                , IdProduto = @IdProduto  
                                                , Quantidade = @Quantidade  
                                                , ValorUnitario = @ValorUnitario  
                                            WHERE IdPedidoProduto = @IdPedidoProduto 
                ");    
                

                if (objVO != null)
                {
                    GetSqlCommand().CommandText = "";
                    GetSqlCommand().CommandText = objSbUpdate.ToString();
                    GetSqlCommand().Parameters.Clear();
                    GetSqlCommand().Parameters.Add("IdPedidoProduto", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("IdPedido", SqlDbType.VarChar).Value = objVO.Pedido.Id;
                    GetSqlCommand().Parameters.Add("IdProduto", SqlDbType.VarChar).Value = objVO.Produto.Id;
                    GetSqlCommand().Parameters.Add("Quantidade", SqlDbType.VarChar).Value = objVO.Quantidade;
                    GetSqlCommand().Parameters.Add("ValorUnitario", SqlDbType.VarChar).Value = objVO.ValorUnitario;

                    GetSqlCommand().ExecuteNonQuery();
                }
                return objVO.Id;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbUpdate != null)
                {
                    objSbUpdate = null;
                }
            }
        }

        public void Deletar(PedidoProdutoVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(@" DELETE FROM DBPedidosNet.dbo.PedidoProduto WHERE IdPedidoProduto = @IdPedidoProduto ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdPedidoProduto", SqlDbType.Int).Value = objVO.Id;
                GetSqlCommand().ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                if (objSbDelete != null)
                {
                    objSbDelete = null;
                }
            }
        }

        public void Inserir(PedidoProdutoVO objVO)
        {
            try
            {

                objSbInsert = new StringBuilder();

                objSbInsert.AppendLine(@"
                                        INSERT INTO DBPedidosNet.dbo.PedidoProduto    
                                        (                                              
                                                     IdPedido                          
                                                   , IdProduto
                                                   , Quantidade 
                                                   , ValorUnitario 
                                        )                                              
                                        VALUES                                    
                                        (                                              
                                                     @IdPedido                         
                                                   , @IdProduto 
                                                   , @Quantidade 
                                                   , @ValorUnitario 
                                        )                                              
                ");


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdPedido", SqlDbType.VarChar).Value = objVO.Pedido.Id;
                GetSqlCommand().Parameters.Add("IdProduto", SqlDbType.VarChar).Value = objVO.Produto.Id;
                GetSqlCommand().Parameters.Add("Quantidade", SqlDbType.VarChar).Value = objVO.Quantidade;
                GetSqlCommand().Parameters.Add("ValorUnitario", SqlDbType.VarChar).Value = objVO.ValorUnitario;

                GetSqlCommand().ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbInsert != null)
                {
                    objSbInsert = null;
                }
            }
        }

        public List<PedidoProdutoVO> Selecionar(PedidoProdutoVO objVO = null)
        {
            PedidoProdutoVO PedidoProdutoVO = null;
            List<PedidoProdutoVO> lstPedidoProdutoVO = null;

            try
            {
                lstPedidoProdutoVO = new List<PedidoProdutoVO>();

                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@"
                                         SELECT                                                         
                                                PedidoProduto.IdPedidoProduto 
                                              , PedidoProduto.IdPedido
                                              , PedidoProduto.IdProduto
                                              , PedidoProduto.Quantidade
                                              , PedidoProduto.ValorUnitario
                                              , Pedido.IdCliente
                                              , Pedido.DataPedido
                                              , Pedido.Efetuado
                                              , Produto.Descricao
                                              , Produto.Marca
                                              , Cliente.Cpf
                                              , Cliente.Nome
                                           FROM DBPedidosNet.dbo.PedidoProduto 
                                     INNER JOIN DBPedidosNet.dbo.Pedido  ON Pedido.IdPedido = PedidoProduto.IdPedido
                                     INNER JOIN DBPedidosNet.dbo.Produto ON Produto.IdProduto = PedidoProduto.IdProduto
                                     INNER JOIN DBPedidosNet.dbo.Cliente ON Cliente.IdCliente = Pedido.IdCliente
                                          WHERE 1 = 1                  
                ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND Pedido.IdPedidoProduto = @IdPedidoProduto");
                        GetSqlCommand().Parameters.Add("IdPedidoProduto", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (objVO.Pedido.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND Pedido.IdPedido = @IdPedido");
                        GetSqlCommand().Parameters.Add("IdPedido", SqlDbType.VarChar).Value = objVO.Pedido.Id;
                    }
                    if (objVO.Pedido.Cliente.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND Cliente.IdCliente = @IdCliente");
                        GetSqlCommand().Parameters.Add("IdCliente", SqlDbType.VarChar).Value = objVO.Pedido.Cliente.Id;
                    }
                    if (objVO.Pedido.Efetuado != null)
                    {
                        objSbSelect.AppendLine(@" AND Pedido.Efetuado = @Efetuado");
                        GetSqlCommand().Parameters.Add("Efetuado", SqlDbType.Bit).Value = objVO.Pedido.Efetuado;
                    }
                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    PedidoProdutoVO = new PedidoProdutoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdPedidoProduto"))))
                        PedidoProdutoVO.Id = Convert.ToInt64(GetSqlDataReader()["IdPedidoProduto"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdPedido"))))
                        PedidoProdutoVO.Pedido.Id = Convert.ToInt64(GetSqlDataReader()["IdPedido"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdProduto"))))
                        PedidoProdutoVO.Produto.Id = Convert.ToInt64(GetSqlDataReader()["IdProduto"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Quantidade"))))
                        PedidoProdutoVO.Quantidade = Convert.ToInt32(GetSqlDataReader()["Quantidade"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ValorUnitario"))))
                        PedidoProdutoVO.ValorUnitario = Convert.ToDecimal(GetSqlDataReader()["ValorUnitario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCliente"))))
                        PedidoProdutoVO.Pedido.Cliente.Id = Convert.ToInt64(GetSqlDataReader()["IdCliente"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataPedido"))))
                        PedidoProdutoVO.Pedido.DataPedido = Convert.ToDateTime(GetSqlDataReader()["DataPedido"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Efetuado"))))
                        PedidoProdutoVO.Pedido.Efetuado = Convert.ToBoolean(GetSqlDataReader()["Efetuado"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        PedidoProdutoVO.Produto.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Marca"))))
                        PedidoProdutoVO.Produto.Marca = Convert.ToString(GetSqlDataReader()["Marca"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cpf"))))
                        PedidoProdutoVO.Pedido.Cliente.Cpf = Convert.ToString(GetSqlDataReader()["Cpf"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        PedidoProdutoVO.Pedido.Cliente.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    lstPedidoProdutoVO.Add(PedidoProdutoVO);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                {
                    objSbSelect = null;
                }
                Close();

            }

            return lstPedidoProdutoVO;
        }

        public PedidoProdutoVO Consultar(PedidoProdutoVO objVO)
        {
            try
            {
                List<PedidoProdutoVO> lst = Selecionar(objVO);
                return lst.Count > 0 ? (PedidoProdutoVO)lst.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PedidoProdutoVO ConsultarMax()
        {
            PedidoProdutoVO PedidoProdutoVO = null;

            try
            {
                PedidoProdutoVO = new PedidoProdutoVO();

                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@" SELECT MAX(IdPedidoProduto) IdPedidoProduto FROM DBPedidosNet.dbo.PedidoProduto");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    PedidoProdutoVO = new PedidoProdutoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdPedidoProduto"))))
                        PedidoProdutoVO.Id = Convert.ToInt64(GetSqlDataReader()["IdPedidoProduto"]);

                }

                return PedidoProdutoVO;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbSelect != null)
                {
                    objSbSelect = null;
                }
                Close();
            }
        }
    }
}
