using PedidosNet.Models.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PedidosNet.Models.DAO
{
    public class ProdutoDAO : AbstractDAO
    {
        public ProdutoDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Alterar(ProdutoVO objVO)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"
                                         UPDATE DBPedidosNet.dbo.Produto                         
                                            SET                                                  
                                                  Descricao = @Descricao                          
                                                , Marca = @Marca  
                                                , ValorUnitario = @ValorUnitario
                                                , QuantidadeEstoque = @QuantidadeEstoque

                                            WHERE IdProduto = @IdProduto 
                ");    
                

                if (objVO != null)
                {
                    GetSqlCommand().CommandText = "";
                    GetSqlCommand().CommandText = objSbUpdate.ToString();
                    GetSqlCommand().Parameters.Clear();
                    GetSqlCommand().Parameters.Add("IdProduto", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                    GetSqlCommand().Parameters.Add("Marca", SqlDbType.VarChar).Value = objVO.Marca;
                    GetSqlCommand().Parameters.Add("ValorUnitario", SqlDbType.Decimal).Value = objVO.ValorUnitario;
                    GetSqlCommand().Parameters.Add("QuantidadeEstoque", SqlDbType.Int).Value = objVO.QuantidadeEstoque;
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

        public void Deletar(ProdutoVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(@" DELETE FROM DBPedidosNet.dbo.Produto WHERE IdProduto =  @IdProduto  ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdProduto", SqlDbType.Int).Value = objVO.Id;
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

        public void Inserir(ProdutoVO objVO)
        {
            try
            {

                objSbInsert = new StringBuilder();

                objSbInsert.AppendLine(@"
                                        INSERT INTO DBPedidosNet.dbo.Produto      
                                        (                                              
                                                     Descricao                          
                                                   , Marca                          
                                                   , ValorUnitario              
                                                   , QuantidadeEstoque                              
                                        )                                              
                                        VALUES                                    
                                        (                                              
                                                     @Descricao                         
                                                   , @Marca                         
                                                   , @ValorUnitario             
                                                   , @QuantidadeEstoque                             
                                        )                                              
                ");


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("Descricao", SqlDbType.VarChar).Value = objVO.Descricao;
                GetSqlCommand().Parameters.Add("Marca", SqlDbType.VarChar).Value = objVO.Marca;
                GetSqlCommand().Parameters.Add("ValorUnitario", SqlDbType.Decimal).Value = objVO.ValorUnitario;
                GetSqlCommand().Parameters.Add("QuantidadeEstoque", SqlDbType.Int).Value = objVO.QuantidadeEstoque;

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

        public List<ProdutoVO> Selecionar(ProdutoVO objVO = null)
        {
            ProdutoVO ProdutoVO = null;
            List<ProdutoVO> lstProdutoVO = null;

            try
            {
                lstProdutoVO = new List<ProdutoVO>();

                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@"
                                         SELECT                                                         
                                                Produto.IdProduto                                               
                                              , Produto.Descricao                                               
                                              , Produto.Marca                                   
                                              , Produto.ValorUnitario
                                              , Produto.QuantidadeEstoque
                                           FROM DBPedidosNet.dbo.Produto                                 
                                          WHERE 1 = 1                      
                ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND Produto.IdProduto = @IdProduto");
                        GetSqlCommand().Parameters.Add("IdProduto", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (!string.IsNullOrEmpty(objVO.Descricao))
                    {
                        objSbSelect.AppendLine(string.Format(" AND Produto.Descricao LIKE '%{0}%'", objVO.Descricao));
                    }

                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    ProdutoVO = new ProdutoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdProduto"))))
                        ProdutoVO.Id = Convert.ToInt64(GetSqlDataReader()["IdProduto"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Descricao"))))
                        ProdutoVO.Descricao = Convert.ToString(GetSqlDataReader()["Descricao"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Marca"))))
                        ProdutoVO.Marca = Convert.ToString(GetSqlDataReader()["Marca"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ValorUnitario"))))
                        ProdutoVO.ValorUnitario = Convert.ToDecimal(GetSqlDataReader()["ValorUnitario"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("QuantidadeEstoque"))))
                        ProdutoVO.QuantidadeEstoque = Convert.ToInt32(GetSqlDataReader()["QuantidadeEstoque"]);

                    lstProdutoVO.Add(ProdutoVO);
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

            return lstProdutoVO;
        }

        public ProdutoVO Consultar(ProdutoVO objVO)
        {
            try
            {
                List<ProdutoVO> lst = Selecionar(objVO);
                return lst.Count > 0 ? (ProdutoVO)lst.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ProdutoVO ConsultarMax()
        {
            ProdutoVO ProdutoVO = null;

            try
            {
                ProdutoVO = new ProdutoVO();

                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@" SELECT MAX(IdProduto) IdProduto FROM DBPedidosNet.dbo.Produto ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    ProdutoVO = new ProdutoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdProduto"))))
                        ProdutoVO.Id = Convert.ToInt64(GetSqlDataReader()["IdProduto"]);

                }

                return ProdutoVO;
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
