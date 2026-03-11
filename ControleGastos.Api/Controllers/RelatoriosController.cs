using ControleGastos.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioController(IRelatorioService relatorioService) : ControllerBase
    {
        private readonly IRelatorioService _relatorioService = relatorioService;

        /// <summary>
        /// Consulta totais de receitas, despesas e saldo por pessoa e total geral
        /// </summary>
        [HttpGet("SaldoTotal")]
        public async Task<IActionResult> TotaisCompletos()
        {
            var resultado = await _relatorioService.TotaisCompletos();
            return Ok(resultado);
        }
    }
}