using ControleGastos.DataAccess.Entities;

namespace ControleGastos.DataAccess.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> Listar();

        Task<Categoria?> Obter(Guid id);

        Task Criar(Categoria categoria);

        Task Atualizar(Categoria categoria);

        Task Deletar(Categoria categoria);
    }
}