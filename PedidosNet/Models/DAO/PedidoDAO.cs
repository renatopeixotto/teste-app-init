using PedidosNet.Models.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PedidosNet.Models.DAO
{
    public class PedidoDAO : AbstractDAO
    {
        public PedidoDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Alterar(PedidoVO objVO)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"
                                         UPDATE DBPedidosNet.dbo.Pedido                       
                                            SET                                                  
                                                  IdCliente = @IdCliente                          
                                                , Efetuado = @Efetuado

                                            WHERE IdPedido = @IdPedido 
                ");    
                

                if (objVO != null)
                {
                    GetSqlCommand().CommandText = "";
                    GetSqlCommand().CommandText = objSbUpdate.ToString();
                    GetSqlCommand().Parameters.Clear();
                    GetSqlCommand().Parameters.Add("IdPedido", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("IdCliente", SqlDbType.VarChar).Value = objVO.Cliente.Id;
                    GetSqlCommand().Parameters.Add("Efetuado", SqlDbType.Bit).Value = objVO.Efetuado;

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

        public void Deletar(PedidoVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(@" DELETE FROM DBPedidosNet.dbo.Pedido WHERE IdPedido = @IdPedido ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdPedido", SqlDbType.Int).Value = objVO.Id;
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

        public void Inserir(PedidoVO objVO)
        {
            try
            {

                objSbInsert = new StringBuilder();

                objSbInsert.AppendLine(@"
                                        INSERT INTO DBPedidosNet.dbo.Pedido    
                                        (                                              
                                                     IdCliente                          
                                                   , DataPedido
                                                   , Efetuado
                                        )                                              
                                        VALUES                                    
                                        (                                              
                                                     @IdCliente                         
                                                   , GETDATE()
                                                   , @Efetuado
                                        )                                              
                ");


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdCliente", SqlDbType.VarChar).Value = objVO.Cliente.Id;
                GetSqlCommand().Parameters.Add("Efetuado", SqlDbType.Bit).Value = objVO.Efetuado;

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

        public long FinalizarPedido(PedidoVO objVO)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"
                                         UPDATE DBPedidosNet.dbo.Pedido                       
                                            SET                                                                      
                                                 Efetuado = @Efetuado

                                            WHERE IdPedido = @IdPedido 
                ");


                if (objVO != null)
                {
                    GetSqlCommand().CommandText = "";
                    GetSqlCommand().CommandText = objSbUpdate.ToString();
                    GetSqlCommand().Parameters.Clear();
                    GetSqlCommand().Parameters.Add("IdPedido", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("Efetuado", SqlDbType.Bit).Value = objVO.Efetuado;

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

        public List<PedidoVO> Selecionar(PedidoVO objVO = null)
        {
            PedidoVO PedidoVO = null;
            List<PedidoVO> lstPedidoVO = null;

            try
            {
                lstPedidoVO = new List<PedidoVO>();

                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@"
                                         SELECT                                                         
                                                Pedido.IdPedido                                               
                                              , Pedido.IdCliente                                               
                                              , Pedido.DataPedido
                                              , Pedido.Efetuado
                                              , Cliente.Cpf
                                              , Cliente.Nome
                                              , Cliente.Telefone
 											  , (SELECT SUM(PP.ValorUnitario) 
											       FROM DBPedidosNet.dbo.PedidoProduto PP 
												  WHERE PP.IdPedido = Pedido.IdPedido) AS ValorPedido
                                           FROM DBPedidosNet.dbo.Pedido 
                                     INNER JOIN DBPedidosNet.dbo.Cliente ON Cliente.IdCliente = Pedido.IdCliente
                                          WHERE 1 = 1                      
                ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND Pedido.IdPedido = @IdPedido");
                        GetSqlCommand().Parameters.Add("IdCliente", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (objVO.Efetuado != null)
                    {
                        objSbSelect.AppendLine(@" AND Pedido.Efetuado = @Efetuado");
                        GetSqlCommand().Parameters.Add("Efetuado", SqlDbType.VarChar).Value = objVO.Efetuado;
                    }
                    if (objVO.Cliente.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND Cliente.IdCliente = @IdCliente");
                        GetSqlCommand().Parameters.Add("IdCliente", SqlDbType.VarChar).Value = objVO.Cliente.Id;
                    }
                    if (!string.IsNullOrEmpty(objVO.Cliente.Nome))
                    {
                        objSbSelect.AppendLine(@" AND Cliente.Nome = @Nome");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Cliente.Nome;
                    }

                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    PedidoVO = new PedidoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdPedido"))))
                        PedidoVO.Id = Convert.ToInt64(GetSqlDataReader()["IdPedido"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCliente"))))
                        PedidoVO.Cliente.Id = Convert.ToInt64(GetSqlDataReader()["IdCliente"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("DataPedido"))))
                        PedidoVO.DataPedido = Convert.ToDateTime(GetSqlDataReader()["DataPedido"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Efetuado"))))
                        PedidoVO.Efetuado = Convert.ToBoolean(GetSqlDataReader()["Efetuado"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cpf"))))
                        PedidoVO.Cliente.Cpf = Convert.ToString(GetSqlDataReader()["Cpf"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        PedidoVO.Cliente.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Telefone"))))
                        PedidoVO.Cliente.Telefone = Convert.ToDecimal(GetSqlDataReader()["Telefone"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("ValorPedido"))))
                        PedidoVO.ValorPedido = Convert.ToDecimal(GetSqlDataReader()["ValorPedido"]);

                    lstPedidoVO.Add(PedidoVO);
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

            return lstPedidoVO;
        }

        public PedidoVO Consultar(PedidoVO objVO)
        {
            try
            {
                List<PedidoVO> lst = Selecionar(objVO);
                return lst.Count > 0 ? (PedidoVO)lst.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PedidoVO ConsultarMax()
        {
            PedidoVO PedidoVO = null;

            try
            {
                PedidoVO = new PedidoVO();

                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@" SELECT MAX(IdPedido) IdPedido FROM DBPedidosNet.dbo.Pedido");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    PedidoVO = new PedidoVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdPedido"))))
                        PedidoVO.Id = Convert.ToInt64(GetSqlDataReader()["IdPedido"]);

                }

                return PedidoVO;
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
