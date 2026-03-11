using ControleGastos.DataAccess.Enums;
using System.ComponentModel.DataAnnotations;

namespace ControleGastos.DataAccess.Entities
{
    public class Categoria
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(400)]
        public string Descricao { get; set; } = String.Empty;

        [Required]
        public FinalidadeCategoria Finalidade { get; set; }

        public virtual List<Transacao> Transacoes { get; set; } = [];
    }
}
