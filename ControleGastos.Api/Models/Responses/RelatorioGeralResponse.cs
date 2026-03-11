namespace ControleGastos.Api.Models.Responses
{
    public class RelatorioGeralResponse
    {
        /// <summary>
        /// Lista de relatórios individuais por pessoa
        /// </summary>
        public List<RelatorioPessoaResponse> Pessoas { get; set; } = [];

        public List<RelatorioCategoriaResponse>? Categorias { get; set; }

        /// <summary>
        /// Total de receitas de todas as pessoas
        /// </summary>
        public decimal TotalReceitas { get; set; }

        /// <summary>
        /// Total de despesas de todas as pessoas
        /// </summary>
        public decimal TotalDespesas { get; set; }

        /// <summary>
        /// Saldo líquido geral
        /// </summary>
        public decimal SaldoLiquido { get; set; }
    }
}
