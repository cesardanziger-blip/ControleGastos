using ControleGastos.Api.Models.Requests;
using ControleGastos.Api.Models.Responses;

namespace ControleGastos.Api.Interfaces
{
    public interface ITransacaoService
    {
        public Task<IEnumerable<TransacaoResponse>> Listar();

        public Task<TransacaoResponse> Criar(CriarTransacaoRequest request);

        public Task Deletar(Guid id);
    }
}