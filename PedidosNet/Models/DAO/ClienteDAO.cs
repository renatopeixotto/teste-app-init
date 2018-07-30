using PedidosNet.Models.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PedidosNet.Models.DAO
{
    public class ClienteDAO : AbstractDAO 
    {
        public ClienteDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {

        }

        public long Alterar(ClienteVO objVO)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"
                                         UPDATE DBPedidosNet.dbo.Cliente                        
                                            SET                                                  
                                                  Nome = @Nome                          
                                                , Cpf = @Cpf  
                                                , Telefone = @Telefone
                                                , Senha = @Senha

                                            WHERE IdCliente = @IdCliente 
                ");    
                

                if (objVO != null)
                {
                    GetSqlCommand().CommandText = "";
                    GetSqlCommand().CommandText = objSbUpdate.ToString();
                    GetSqlCommand().Parameters.Clear();
                    GetSqlCommand().Parameters.Add("IdCliente", SqlDbType.Int).Value = objVO.Id;
                    GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                    GetSqlCommand().Parameters.Add("Cpf", SqlDbType.VarChar).Value = objVO.Cpf;
                    GetSqlCommand().Parameters.Add("Telefone", SqlDbType.Decimal).Value = objVO.Telefone;
                    GetSqlCommand().Parameters.Add("Senha", SqlDbType.Int).Value = objVO.Senha;
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

        public void Deletar(ClienteVO objVO)
        {
            try
            {
                objSbDelete = new StringBuilder();

                objSbDelete.AppendLine(@" DELETE FROM DBPedidosNet.dbo.Cliente WHERE IdCliente = @IdCliente  ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbDelete.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdCliente", SqlDbType.Int).Value = objVO.Id;
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

        public void Inserir(ClienteVO objVO)
        {
            try
            {

                objSbInsert = new StringBuilder();

                objSbInsert.AppendLine(@"
                                        INSERT INTO DBPedidosNet.dbo.Cliente     
                                        (                                              
                                                     Nome                          
                                                   , Cpf                          
                                                   , Telefone              
                                                   , Senha                                
                                        )                                              
                                        VALUES                                    
                                        (                                              
                                                     @Nome                         
                                                   , @Cpf                         
                                                   , @Telefone             
                                                   , @Senha                             
                                        )                                              
                ");


                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbInsert.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                GetSqlCommand().Parameters.Add("Cpf", SqlDbType.VarChar).Value = objVO.Cpf;
                GetSqlCommand().Parameters.Add("Telefone", SqlDbType.Decimal).Value = objVO.Telefone;
                GetSqlCommand().Parameters.Add("Senha", SqlDbType.Int).Value = objVO.Senha;

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

        public List<ClienteVO> Selecionar(ClienteVO objVO = null)
        {
            ClienteVO ClienteVO = null;
            List<ClienteVO> lstClienteVO = null;

            try
            {
                lstClienteVO = new List<ClienteVO>();

                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@"
                                         SELECT                                                         
                                                Cliente.IdCliente                                               
                                              , Cliente.Nome                                               
                                              , Cliente.Cpf                                   
                                              , Cliente.Telefone
                                              , Cliente.Senha
                                           FROM DBPedidosNet.dbo.Cliente                                
                                          WHERE 1 = 1                      
                ");

                if (objVO != null)
                {
                    GetSqlCommand().Parameters.Clear();

                    if (objVO.Id > 0)
                    {
                        objSbSelect.AppendLine(@" AND Cliente.IdCliente = @IdCliente");
                        GetSqlCommand().Parameters.Add("IdCliente", SqlDbType.Int).Value = objVO.Id;
                    }
                    if (!string.IsNullOrEmpty(objVO.Nome))
                    {
                        objSbSelect.AppendLine(@" AND Cliente.Nome = @Nome");
                        GetSqlCommand().Parameters.Add("Nome", SqlDbType.VarChar).Value = objVO.Nome;
                    }

                }

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    ClienteVO = new ClienteVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCliente"))))
                        ClienteVO.Id = Convert.ToInt64(GetSqlDataReader()["IdCliente"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Nome"))))
                        ClienteVO.Nome = Convert.ToString(GetSqlDataReader()["Nome"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Cpf"))))
                        ClienteVO.Cpf = Convert.ToString(GetSqlDataReader()["Cpf"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Telefone"))))
                        ClienteVO.Telefone = Convert.ToDecimal(GetSqlDataReader()["Telefone"]);

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("Senha"))))
                        ClienteVO.Senha = Convert.ToInt32(GetSqlDataReader()["Senha"]);

                    lstClienteVO.Add(ClienteVO);
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

            return lstClienteVO;
        }

        public ClienteVO Consultar(ClienteVO objVO)
        {
            try
            {
                List<ClienteVO> lst = Selecionar(objVO);
                return lst.Count > 0 ? (ClienteVO)lst.ToArray().GetValue(0) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ClienteVO ConsultarMax()
        {
            ClienteVO ClienteVO = null;

            try
            {
                ClienteVO = new ClienteVO();

                objSbSelect = new StringBuilder();

                objSbSelect.AppendLine(@" SELECT MAX(IdCliente) IdCliente FROM DBPedidosNet.dbo.Cliente ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbSelect.ToString();

                while (GetSqlDataReader().Read())
                {
                    ClienteVO = new ClienteVO();

                    if (!(GetSqlDataReader().IsDBNull(GetSqlDataReader().GetOrdinal("IdCliente"))))
                        ClienteVO.Id = Convert.ToInt64(GetSqlDataReader()["IdCliente"]);

                }

                return ClienteVO;
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
