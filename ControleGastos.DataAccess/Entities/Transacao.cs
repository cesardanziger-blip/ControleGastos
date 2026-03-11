using ControleGastos.DataAccess.Enums;
using System.ComponentModel.DataAnnotations;

namespace ControleGastos.DataAccess.Entities
{
    public class Transacao
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(400)]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Valor { get; set; }

        [Required]
        public TipoTransacao Tipo { get; set; }

        [Required]
        public Guid CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }

        [Required]
        public Guid PessoaId { get; set; }

        public Pessoa? Pessoa { get; set; }
    }
}
