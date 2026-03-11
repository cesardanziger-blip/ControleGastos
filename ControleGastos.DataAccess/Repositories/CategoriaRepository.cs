using ControleGastos.DataAccess.Context;
using ControleGastos.DataAccess.Entities;
using ControleGastos.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.DataAccess.Repositories
{
    public class CategoriaRepository(AppDbContext context) : ICategoriaRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Categoria>> Listar()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categoria?> Obter(Guid id)
        {
            return await _context.Categorias
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Criar(Categoria categoria)
        {
            _context.Categorias.Add(categoria);

            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Categoria categoria)
        {
            _context.Categorias.Update(categoria);

            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);

            await _context.SaveChangesAsync();
        }
    }
}