using System;
using Microsoft.AspNetCore.Mvc;
using PedidosNet.Models.VO;
using PedidosNet.Models.BE;

namespace PedidosNet.Controllers
{
    [Route("api/cliente")]
    public class ClienteController : Controller
    {
        //Como estava com erro no web service "http://pessoa-hom.tjmt.jus.br/api/soapservice.svc?wsdl"
        //fiz este método simples para retornar o cliente (para não perder tempo)
        [HttpGet]
        [Route("GetCliente")]
        public ActionResult GetCliente(string cpf, string senha)
        {
            ClienteBE clienteBe = null;

            try
            {
                clienteBe = new ClienteBE();

                var cliente = clienteBe.Consultar(new ClienteVO() { Cpf = cpf, Senha = senha });

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                clienteBe?.FecharConexao();
            }
        }
    }

}