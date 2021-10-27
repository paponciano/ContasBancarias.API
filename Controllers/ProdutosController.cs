using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProjetoVendas;

namespace ContasBancarias.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        [HttpGet]
        [Route("selecionardados")]
        public List<dynamic> SelecionarDados()
        {
            List<dynamic> minhaLista = new();
            minhaLista.Add(new { Codigo = 1, Nome = "Paulo" });
            minhaLista.Add(new { Codigo = 2, Nome = "Maria" });
            return minhaLista;
        }

        [HttpGet]
        public IActionResult SelecionarProdutos()
        {
            string mensagemErro = "";
            var listaProdutos = new Produto().SelecionarProdutos(out mensagemErro);
            if (mensagemErro == "")
                return Ok(listaProdutos);
            else
                return BadRequest(mensagemErro);
        }

        [HttpGet]
        [Route("{codigo}")]
        public IActionResult SelecionarProdutos(int codigo)
        {
            string mensagemErro = "";
            var listaProdutos = new Produto().SelecionarProdutos(codigo, out mensagemErro);
            if (mensagemErro == "")
                return Ok(listaProdutos);
            else
                return BadRequest(mensagemErro);
        }
    }
}