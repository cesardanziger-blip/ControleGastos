using ControleGastos.DataAccess.Entities;

namespace ControleGastos.DataAccess.Interfaces
{
    public interface IPessoaRepository
    {
        Task<List<Pessoa>> Listar();

        Task<Pessoa> Obter(Guid id);

        Task Criar(Pessoa pessoa);

        Task Atualizar(Pessoa pessoa);

        Task Deletar(Pessoa pessoa);
    }
}
