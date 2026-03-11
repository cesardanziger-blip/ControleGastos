using AutoMapper;
using ControleGastos.Api.Interfaces;
using ControleGastos.Api.Models.Requests;
using ControleGastos.Api.Models.Responses;
using ControleGastos.DataAccess.Entities;
using ControleGastos.DataAccess.Interfaces;

namespace ControleGastos.Api.Services
{
    public class PessoaService(IPessoaRepository pessoaRepository, IMapper mapper) : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository = pessoaRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<PessoaResponse>> Listar()
        {
            var pessoas = await _pessoaRepository.Listar();

            return _mapper.Map<IEnumerable<PessoaResponse>>(pessoas);
        }

        public async Task<PessoaResponse> Criar(CriarPessoaRequest request)
        {
            if (request.Idade < 0)
                throw new Exception("Idade inválida.");

            var pessoa = _mapper.Map<Pessoa>(request);

            pessoa.Id = Guid.NewGuid();

            await _pessoaRepository.Criar(pessoa);

            return _mapper.Map<PessoaResponse>(pessoa);
        }

        public async Task Atualizar(Guid id, CriarPessoaRequest request)
        {
            var pessoa = await _pessoaRepository.Obter(id) ?? throw new Exception("Pessoa não encontrada");

            pessoa.Nome = request.Nome;
            pessoa.Idade = request.Idade;

            await _pessoaRepository.Atualizar(pessoa);
        }

        public async Task Deletar(Guid id)
        {
            var pessoa = await _pessoaRepository.Obter(id);

            if (pessoa == null)
                throw new Exception("Pessoa não encontrada");

            await _pessoaRepository.Deletar(pessoa);
        }
    }
}