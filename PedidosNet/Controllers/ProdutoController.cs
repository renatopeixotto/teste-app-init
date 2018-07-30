using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using PedidosNet.Models.VO;
using PedidosNet.Models.BE;

namespace PedidosNet.Controllers
{
    [Route("api/produto")]
    public class ProdutoController : Controller
    {
        [HttpGet]
        [Route("GetAll")]
        public ActionResult GetAll()
        {
            ProdutoBE produtoBE = null;

            try
            {
                produtoBE = new ProdutoBE();

                var lst = produtoBE.Selecionar();

                return Ok(lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (produtoBE != null)
                    produtoBE.FecharConexao();
            }
        }

        [Route("GetByDescricao")]
        public ActionResult GetByDescricao(string descricao)
        {
            ProdutoBE produtoBE = null;

            try
            {
                produtoBE = new ProdutoBE();

                var lst = produtoBE.Selecionar(new ProdutoVO { Descricao = descricao });

                if (lst == null)
                    return NotFound();              

                return Ok(lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (produtoBE != null)
                    produtoBE.FecharConexao();
            }
        }

        [HttpGet("{id}", Name = "GetProduto")]
        public ActionResult GetById(long id)
        {
            ProdutoBE produtoBE = null;

            try
            {
                produtoBE = new ProdutoBE();

                var item = produtoBE.Consultar(new ProdutoVO { Id = id });

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
                if (produtoBE != null)
                    produtoBE.FecharConexao();
            }
        }

        [HttpPost]
        [Route("Insert")]
        public ActionResult Insert([FromBody]ProdutoVO produto)
        {
            ProdutoBE produtoBE = null;

            try
            {
                produtoBE = new ProdutoBE();

                var id = produtoBE.Inserir(produto);

                return CreatedAtRoute("GetProduto", new { id = id }, produto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (produtoBE != null)
                    produtoBE.FecharConexao();
            }
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update([FromBody]ProdutoVO produto)
        {
            ProdutoBE produtoBE = null;

            try
            {
                produtoBE = new ProdutoBE();

                produtoBE.Alterar(produto);

                return NoContent();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (produtoBE != null)
                    produtoBE.FecharConexao();
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public ActionResult Delete(long id)
        {
            ProdutoBE produtoBE = null;
            
            try
            {
                produtoBE = new ProdutoBE();

                produtoBE.Deletar(new ProdutoVO { Id = id });

                return NoContent();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (produtoBE != null)
                    produtoBE.FecharConexao();
            }
        }
    }

}