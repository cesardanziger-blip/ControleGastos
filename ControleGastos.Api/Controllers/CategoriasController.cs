using ControleGastos.Api.Interfaces;
using ControleGastos.Api.Models.Requests;
using ControleGastos.Api.Models.Responses;
using ControleGastos.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de categorias
    /// </summary>
    [ApiController]
    [Route("api/categorias")]
    public class CategoriasController(ICategoriaService categoriaService) : ControllerBase
    {
        private readonly ICategoriaService _categoriaService = categoriaService;

        /// <summary>
        /// Retorna todas as categorias cadastradas.
        /// </summary>
        /// <returns>Uma lista de <see cref="CategoriaResponse"/> representando todas as categorias.</returns>
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var categorias = await _categoriaService.Listar();
            return Ok(categorias);
        }

        /// <summary>
        /// Cria uma nova categoria financeira.
        /// </summary>
        /// <param name="request">Dados da categoria a ser criada.</param>
        /// <returns>A categoria criada como <see cref="CategoriaResponse"/>.</returns>
        /// <response code="201">Categoria criada com sucesso.</response>
        /// <response code="400">Dados inválidos ou violação de regras de negócio.</response>
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarCategoriaRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var resultado = await _categoriaService.Criar(request);
                return CreatedAtAction(nameof(Listar), new { id = resultado.Id }, resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza os dados de uma categoria existente.
        /// </summary>
        /// <param name="id">Identificador único da categoria a ser atualizada.</param>
        /// <param name="request">Novos dados da categoria.</param>
        /// <response code="204">Atualização realizada com sucesso.</response>
        /// <response code="400">Dados inválidos ou violação de regras de negócio.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] CriarCategoriaRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _categoriaService.Atualizar(id, request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Remove categoria e todas as transações ligadas a ela.
        /// </summary>
        /// <param name="id">Identificador único da categoria a ser removida.</param>
        /// <response code="204">Categoria removida com sucesso.</response>
        /// <response code="400">Erro ao tentar remover a categoria.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            try
            {
                await _categoriaService.Deletar(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}