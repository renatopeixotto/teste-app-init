using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using PedidosNet.Models.VO;
using PedidosNet.Models.BE;

namespace PedidosNet.Controllers
{
    [Route("api/pedido")]
    public class PedidoController : Controller
    {
        [HttpGet]
        [Route("GetAll")]
        public ActionResult GetAll()
        {
            PedidoBE pedidoBE = null;

            try
            {
                pedidoBE = new PedidoBE();

                var lst = pedidoBE.Selecionar();

                return Ok(lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (pedidoBE != null)
                    pedidoBE.FecharConexao();
            }
        }

        [HttpGet]
        [Route("GetMeusPedidos")]
        public ActionResult GetMeusPedidos(long idCliente)
        {
            PedidoBE pedidoBE = null;

            try
            {
                pedidoBE = new PedidoBE();

                var lst = pedidoBE.Selecionar(new PedidoVO() {  Cliente = { Id = idCliente }, Efetuado = true });

                return Ok(lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (pedidoBE != null)
                    pedidoBE.FecharConexao();
            }
        }

        [HttpGet]
        [Route("GetPedidoProduto")]
        public ActionResult GetPedidoProduto(long idCliente)
        {
            PedidoProdutoBE pedidoProdutoBE = null;

            try
            {
                pedidoProdutoBE = new PedidoProdutoBE();

                var lst = pedidoProdutoBE.Selecionar(new PedidoProdutoVO() { Pedido = { Cliente = { Id = idCliente }, Efetuado = false } });

                return Ok(lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (pedidoProdutoBE != null)
                    pedidoProdutoBE.FecharConexao();
            }
        }

        [HttpGet("{id}", Name = "GetPedido")]
        public ActionResult GetById(long id)
        {
            PedidoBE pedidoBE = null;

            try
            {
                pedidoBE = new PedidoBE();

                var item = pedidoBE.Consultar(new PedidoVO { Id = id });

                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (pedidoBE != null)
                    pedidoBE.FecharConexao();
            }
        }

        [HttpPost]
        [Route("Insert")]
        public ActionResult Insert([FromBody]PedidoVO pedido)
        {
            PedidoBE pedidoBE = null;

            try
            {
                pedidoBE = new PedidoBE();

                var id = pedidoBE.Inserir(pedido);

                return CreatedAtRoute("GetPedido", new { id = id }, pedido);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (pedidoBE != null)
                    pedidoBE.FecharConexao();
            }
        }

        [HttpPost]
        [Route("InsertPedidoProduto")]
        public ActionResult InsertPedidoProduto([FromBody]PedidoProdutoVO pedidoProduto)
        {
            PedidoProdutoBE pedidoProdutoBE = null;

            try
            {
                pedidoProdutoBE = new PedidoProdutoBE();

                var id = pedidoProdutoBE.InserirPedidoProduto(pedidoProduto);

                return CreatedAtRoute("GetPedido", new { id = id }, pedidoProduto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (pedidoProdutoBE != null)
                    pedidoProdutoBE.FecharConexao();
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public ActionResult Delete(long id)
        {
            PedidoBE pedidoBE = null;
            
            try
            {
                pedidoBE = new PedidoBE();

                pedidoBE.Deletar(new PedidoVO { Id = id });

                return NoContent();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (pedidoBE != null)
                    pedidoBE.FecharConexao();
            }
        }

        [HttpDelete]
        [Route("DeletePedidoProduto/{id}")]
        public ActionResult DeletePedidoProduto(long id)
        {
            PedidoProdutoBE pedidoProdutoBE = null;

            try
            {
                pedidoProdutoBE = new PedidoProdutoBE();

                pedidoProdutoBE.Deletar(new PedidoProdutoVO { Id = id });

                return NoContent();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (pedidoProdutoBE != null)
                    pedidoProdutoBE.FecharConexao();
            }
        }

        [HttpPut]
        [Route("FinalizarPedido")]
        public ActionResult FinalizarPedido([FromBody]PedidoVO pedido)
        {
            PedidoBE pedidoBE = null;

            try
            {
                pedidoBE = new PedidoBE();

                pedidoBE.FinalizarPedido(new PedidoVO { Id = pedido.Id, Efetuado = true });

                return NoContent();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (pedidoBE != null)
                    pedidoBE.FecharConexao();
            }
        }

    }

}