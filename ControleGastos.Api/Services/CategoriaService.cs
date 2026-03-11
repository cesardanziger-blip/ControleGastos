using AutoMapper;
using ControleGastos.Api.Interfaces;
using ControleGastos.Api.Models.Requests;
using ControleGastos.Api.Models.Responses;
using ControleGastos.DataAccess.Entities;
using ControleGastos.DataAccess.Interfaces;

namespace ControleGastos.Api.Services
{
    public class CategoriaService(
        ICategoriaRepository categoriaRepository,
        IMapper mapper) : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<CategoriaResponse>> Listar()
        {
            var categorias = await _categoriaRepository.Listar();

            return _mapper.Map<IEnumerable<CategoriaResponse>>(categorias);
        }

        public async Task<CategoriaResponse> Criar(CriarCategoriaRequest request)
        {
            var categoria = _mapper.Map<Categoria>(request);

            categoria.Id = Guid.NewGuid();

            await _categoriaRepository.Criar(categoria);

            return _mapper.Map<CategoriaResponse>(categoria);
        }

        public async Task Atualizar(Guid id, CriarCategoriaRequest request)
        {
            var categoria = await _categoriaRepository.Obter(id) ?? throw new Exception("Categoria não encontrada");

            categoria.Descricao = request.Descricao;
            categoria.Finalidade = request.Finalidade;

            await _categoriaRepository.Atualizar(categoria);
        }

        public async Task Deletar(Guid id)
        {
            var categoria = await _categoriaRepository.Obter(id);

            if (categoria == null)
                throw new Exception("Categoria não encontrada");

            await _categoriaRepository.Deletar(categoria);
        }
    }
}