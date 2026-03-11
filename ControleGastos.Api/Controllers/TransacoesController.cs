using ControleGastos.Api.Interfaces;
using ControleGastos.Api.Models.Requests;
using ControleGastos.Api.Models.Responses;
using ControleGastos.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de transações financeiras
    /// </summary>
    [ApiController]
    [Route("api/transacoes")]
    public class TransacoesController(ITransacaoService transacaoService) : ControllerBase
    {
        private readonly ITransacaoService _transacaoService = transacaoService;

        /// <summary>
        /// Lista todas as transações cadastradas.
        /// </summary>
        /// <returns>Uma lista de <see cref="TransacaoResponse"/> representando todas as transações.</returns>
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var transacoes = await _transacaoService.Listar();
            return Ok(transacoes);
        }

        /// <summary>
        /// Cria uma nova transação financeira.
        /// </summary>
        /// <param name="request">Dados da transação a ser criada.</param>
        /// <returns>A transação criada como <see cref="TransacaoResponse"/>.</returns>
        /// <response code="201">Transação criada com sucesso.</response>
        /// <response code="400">Dados inválidos ou violação de regras de negócio.</response>
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarTransacaoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var resultado = await _transacaoService.Criar(request);
                return CreatedAtAction(nameof(Listar), new { id = resultado.Id }, resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Remove transação.
        /// </summary>
        /// <param name="id">Identificador único da transação a ser removida.</param>
        /// <response code="204">Transação removida com sucesso.</response>
        /// <response code="400">Erro ao tentar remover a transação.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            try
            {
                await _transacaoService.Deletar(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}