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

        [HttpPost]
        [Route("{numero}")]
        public IActionResult Depositar([FromBody] Deposito deposito, int numero)
        {
            string mensagemErro = "";
            var efetuouDeposito = new Conta().Depositar(deposito, numero, out mensagemErro);
            if (mensagemErro == "")
            {
                if (efetuouDeposito)
                    return Ok("depósito efetuado com sucesso");
                else
                    return BadRequest("depósito não efetuado. Verifique as informações e tente novamente");
            }
            else
                return BadRequest(mensagemErro);
        }
    }

}