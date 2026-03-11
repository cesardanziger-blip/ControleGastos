using ControleGastos.DataAccess.Context;
using ControleGastos.DataAccess.Entities;
using ControleGastos.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.DataAccess.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly AppDbContext _context;

        public PessoaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pessoa>> Listar()
        {
            return await _context.Pessoas
                .Include(p => p.Transacoes)
                .ToListAsync();
        }

        public async Task<Pessoa?> Obter(Guid id)
        {
            return await _context.Pessoas
                .Include(p => p.Transacoes)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Criar(Pessoa pessoa)
        {
            _context.Pessoas.Add(pessoa);

            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Pessoa pessoa)
        {
            _context.Pessoas.Update(pessoa);

            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Pessoa pessoa)
        {
            _context.Pessoas.Remove(pessoa);

            await _context.SaveChangesAsync();
        }
    }
}