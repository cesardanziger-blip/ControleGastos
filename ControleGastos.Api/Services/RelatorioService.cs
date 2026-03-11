using ControleGastos.Api.Interfaces;
using ControleGastos.Api.Models.Responses;
using ControleGastos.DataAccess.Enums;
using ControleGastos.DataAccess.Interfaces;

namespace ControleGastos.Api.Services
{
    public class RelatorioService(IPessoaRepository pessoaRepository, 
        ICategoriaRepository categoriaRepository) : IRelatorioService
    {
        private readonly IPessoaRepository _pessoaRepository = pessoaRepository;
        private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;

        public async Task<RelatorioGeralResponse> TotaisCompletos()
        {
            // Listar pessoas
            var pessoas = await _pessoaRepository.Listar();
            var relatorioPessoas = pessoas.Select(p =>
            {
                var receitas = p.Transacoes.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor);
                var despesas = p.Transacoes.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor);

                return new RelatorioPessoaResponse
                {
                    Pessoa = p.Nome,
                    Receitas = receitas,
                    Despesas = despesas,
                    Saldo = receitas - despesas
                };
            }).ToList();

            // Listar categorias
            var categorias = await _categoriaRepository.Listar(); // incluir transacoes com Include
            var relatorioCategorias = categorias.Select(c =>
            {
                var receitas = c.Transacoes.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor);
                var despesas = c.Transacoes.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor);

                return new RelatorioCategoriaResponse
                {
                    Categoria = c.Descricao,
                    Receitas = receitas,
                    Despesas = despesas,
                    Saldo = receitas - despesas
                };
            }).ToList();

            // Totais gerais (somando pessoas e categorias)
            var totalReceitas = relatorioPessoas.Sum(r => r.Receitas); // ou pode somar também categorias se quiser
            var totalDespesas = relatorioPessoas.Sum(r => r.Despesas);

            return new RelatorioGeralResponse
            {
                Pessoas = relatorioPessoas,
                Categorias = relatorioCategorias,
                TotalReceitas = totalReceitas,
                TotalDespesas = totalDespesas,
                SaldoLiquido = totalReceitas - totalDespesas
            };
        }
    }
}