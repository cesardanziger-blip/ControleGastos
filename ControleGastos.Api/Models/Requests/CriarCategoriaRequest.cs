using ControleGastos.DataAccess.Enums;
using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Api.Models.Requests
{
    public class CriarCategoriaRequest
    {
        [Required]
        [MaxLength(400)]
        public string Descricao { get; set; }

        [Required]
        public FinalidadeCategoria Finalidade { get; set; }
    }
}
