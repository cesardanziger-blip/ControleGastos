namespace ControleGastos.Api.Models.Responses
{
    public class RelatorioPessoaResponse
    {
        public string Pessoa { get; set; } = string.Empty;

        public decimal Receitas { get; set; }

        public decimal Despesas { get; set; }

        public decimal Saldo { get; set; }
    }
}