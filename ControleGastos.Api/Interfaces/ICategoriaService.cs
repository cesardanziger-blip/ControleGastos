using ControleGastos.Api.Models.Requests;
using ControleGastos.Api.Models.Responses;

namespace ControleGastos.Api.Interfaces
{
    public interface ICategoriaService
    {
        public Task<IEnumerable<CategoriaResponse>> Listar();

        public Task<CategoriaResponse> Criar(CriarCategoriaRequest request);

        public Task Atualizar(Guid id, CriarCategoriaRequest request);

        public Task Deletar(Guid id);
    }
}