using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ContasBancarias.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : ControllerBase
    {
        [HttpGet]
        public IActionResult SelecionarContas()
        {
            string mensagemErro = "";
            var listaContas = new Conta().SelecionarContas(out mensagemErro);
            if (mensagemErro == "")
                return Ok(listaContas);
            else
                return BadRequest(mensagemErro);
        }

        [HttpGet]
        [Route("{numero}")]
        public IActionResult SelecionarContas(int numero)
        {
            string mensagemErro = "";
            var listaContas = new Conta().SelecionarContas(numero, out mensagemErro);
            if (mensagemErro == "")
                return Ok(listaContas);
            else
                return BadRequest(mensagemErro);
        }
    }

}