namespace ControleGastos.Api.Models.Responses
{
    public class RelatorioCategoriaResponse
    {
        public string Categoria { get; set; } = string.Empty;
        public decimal Receitas { get; set; }
        public decimal Despesas { get; set; }
        public decimal Saldo { get; set; }
    }
}
