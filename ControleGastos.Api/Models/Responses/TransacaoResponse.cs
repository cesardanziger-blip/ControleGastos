namespace ControleGastos.Api.Models.Responses
{
    /// <summary>
    /// Dados de retorno de uma transação financeira
    /// </summary>
    public class TransacaoResponse
    {
        /// <summary>
        /// Identificador da transação
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Descrição da transação
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Valor da transação
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Tipo da transação
        /// </summary>
        /// <remarks>
        /// 1 = Despesa  
        /// 2 = Receita
        /// </remarks>
        public int Tipo { get; set; }

        /// <summary>
        /// Nome da categoria da transação
        /// </summary>
        public string Categoria { get; set; }

        /// <summary>
        /// Nome da pessoa associada à transação
        /// </summary>
        public string Pessoa { get; set; }
    }
}