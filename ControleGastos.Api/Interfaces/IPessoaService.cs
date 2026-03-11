using ControleGastos.Api.Models.Requests;
using ControleGastos.Api.Models.Responses;

namespace ControleGastos.Api.Interfaces
{
    public interface IPessoaService
    {
        public Task<IEnumerable<PessoaResponse>> Listar();

        public Task<PessoaResponse> Criar(CriarPessoaRequest request);

        public Task Atualizar(Guid id, CriarPessoaRequest request);

        public Task Deletar(Guid id);
    }
}