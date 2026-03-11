using ControleGastos.DataAccess.Entities;

namespace ControleGastos.DataAccess.Interfaces
{
    public interface ITransacaoRepository
    {
        Task<List<Transacao>> Listar();

        Task<Transacao?> Obter(Guid id);

        Task Criar(Transacao transacao);

        Task Deletar(Transacao transacao);
    }
}