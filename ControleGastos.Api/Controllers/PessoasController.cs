using ControleGastos.Api.Interfaces;
using ControleGastos.Api.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de pessoas
    /// </summary>
    [ApiController]
    [Route("api/pessoas")]
    public class PessoasController(IPessoaService pessoaService) : ControllerBase
    {
        private readonly IPessoaService _pessoaService = pessoaService;

        /// <summary>
        /// Retorna todas as pessoas cadastradas.
        /// </summary>
        /// <returns>Uma lista de <see cref="PessoaResponse"/> representando todas as pessoas.</returns>
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var pessoas = await _pessoaService.Listar();
            return Ok(pessoas);
        }

        /// <summary>
        /// Cria uma nova pessoa.
        /// </summary>
        /// <param name="request">Dados da pessoa a ser criada.</param>
        /// <returns>A pessoa criada como <see cref="PessoaResponse"/>.</returns>
        /// <response code="201">Pessoa criada com sucesso.</response>
        /// <response code="400">Dados inválidos ou violação de regras de negócio.</response>
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarPessoaRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var resultado = await _pessoaService.Criar(request);
                return CreatedAtAction(nameof(Listar), new { id = resultado.Id }, resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza os dados de uma pessoa existente.
        /// </summary>
        /// <param name="id">Identificador único da pessoa a ser atualizada.</param>
        /// <param name="request">Novos dados da pessoa.</param>
        /// <response code="204">Atualização realizada com sucesso.</response>
        /// <response code="400">Dados inválidos ou violação de regras de negócio.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] CriarPessoaRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _pessoaService.Atualizar(id, request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Remove uma pessoa e todas as suas transações associadas.
        /// </summary>
        /// <param name="id">Identificador único da pessoa a ser removida.</param>
        /// <response code="204">Pessoa removida com sucesso.</response>
        /// <response code="400">Erro ao tentar remover a pessoa.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            try
            {
                await _pessoaService.Deletar(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}