using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Api.Models.Requests
{
    public class CriarPessoaRequest
    {
        [Required]
        [MaxLength(200)]
        public string Nome { get; set; }

        [Required]
        public int Idade { get; set; }
    }
}
