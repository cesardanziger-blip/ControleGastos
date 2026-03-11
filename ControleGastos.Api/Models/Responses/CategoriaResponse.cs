using ControleGastos.DataAccess.Enums;

namespace ControleGastos.Api.Models.Responses
{
    /// <summary>
    /// Dados de retorno de uma categoria
    /// </summary>
    public class CategoriaResponse
    {
        /// <summary>
        /// Identificador da categoria
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Descrição da categoria
        /// </summary>
        public string? Descricao { get; set; }

        /// <summary>
        /// Finalidade da categoria
        /// </summary>
        /// <remarks>
        /// 1 = Despesa
        /// 2 = Receita
        /// 3 = Ambas
        /// </remarks>
        public FinalidadeCategoria Finalidade { get; set; }
    }
}
