using ControleGastos.DataAccess.Context;
using ControleGastos.DataAccess.Entities;
using ControleGastos.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.DataAccess.Repositories
{
    public class TransacaoRepository(AppDbContext context) : ITransacaoRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Transacao>> Listar()
        {
            return await _context.Transacoes
                .Include(t => t.Pessoa)
                .Include(t => t.Categoria)
                .ToListAsync();
        }

        public async Task<Transacao?> Obter(Guid id)
        {
            return await _context.Transacoes
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Criar(Transacao transacao)
        {
            _context.Transacoes.Add(transacao);

            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Transacao transacao)
        {
            _context.Transacoes.Remove(transacao);

            await _context.SaveChangesAsync();
        }
    }
}