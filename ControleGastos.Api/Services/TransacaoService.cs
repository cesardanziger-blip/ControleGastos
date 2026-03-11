using AutoMapper;
using ControleGastos.Api.Interfaces;
using ControleGastos.Api.Models.Requests;
using ControleGastos.Api.Models.Responses;
using ControleGastos.DataAccess.Entities;
using ControleGastos.DataAccess.Enums;
using ControleGastos.DataAccess.Interfaces;

namespace ControleGastos.Api.Services
{
    public class TransacaoService(
        ITransacaoRepository transacaoRepository,
        IPessoaRepository pessoaRepository,
        ICategoriaRepository categoriaRepository,
        IMapper mapper) : ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository = transacaoRepository;
        private readonly IPessoaRepository _pessoaRepository = pessoaRepository;
        private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<TransacaoResponse>> Listar()
        {
            var transacoes = await _transacaoRepository.Listar();

            return _mapper.Map<IEnumerable<TransacaoResponse>>(transacoes);
        }

        public async Task<TransacaoResponse> Criar(CriarTransacaoRequest request)
        {
            if (request.Valor <= 0)
                throw new Exception("O valor da transação deve ser positivo");

            var pessoa = await _pessoaRepository.Obter(request.PessoaId) ?? throw new Exception("Pessoa não encontrada");
            var categoria = await _categoriaRepository.Obter(request.CategoriaId) ?? throw new Exception("Categoria não encontrada");
            
            // regra menor de idade
            if (pessoa.Idade < 18 && request.Tipo == TipoTransacao.Receita)
                throw new Exception("Menores de idade só podem registrar despesas");

            // valida finalidade categoria
            if (categoria.Finalidade == FinalidadeCategoria.Receita &&
                request.Tipo == TipoTransacao.Despesa)
                throw new Exception("Categoria não permite despesas");

            if (categoria.Finalidade == FinalidadeCategoria.Despesa &&
                request.Tipo == TipoTransacao.Receita)
                throw new Exception("Categoria não permite receitas");

            var transacao = _mapper.Map<Transacao>(request) ?? throw new Exception("Transação não encontrada.");
            transacao.Id = Guid.NewGuid();

            await _transacaoRepository.Criar(transacao);

            return _mapper.Map<TransacaoResponse>(transacao);
        }

        public async Task Deletar(Guid id)
        {
            var transacao = await _transacaoRepository.Obter(id);

            if (transacao == null)
                throw new Exception("Transacao não encontrada");

            await _transacaoRepository.Deletar(transacao);
        }
    }
}