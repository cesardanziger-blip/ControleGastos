using ControleGastos.DataAccess.Enums;
using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Api.Models.Requests
{
    public class CriarTransacaoRequest
    {
        [Required]
        [MaxLength(400)]
        public string Descricao { get; set; }

        [Required]
        public decimal Valor { get; set; }

        [Required]
        public TipoTransacao Tipo { get; set; }

        [Required]
        public Guid CategoriaId { get; set; }

        [Required]
        public Guid PessoaId { get; set; }
    }
}
