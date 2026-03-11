using System.ComponentModel.DataAnnotations;

namespace ControleGastos.DataAccess.Entities
{
    public class Pessoa
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nome { get; set; } = String.Empty;

        [Required]
        public int Idade { get; set; }

        public List<Transacao> Transacoes { get; set; } = [];
    }
}
