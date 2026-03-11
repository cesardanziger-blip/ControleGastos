using AutoMapper;
using ControleGastos.Api.Models.Requests;
using ControleGastos.Api.Models.Responses;
using ControleGastos.DataAccess.Entities;

namespace ControleGastos.Api.Profiles
{
    /// <summary>
    /// Profile responsável pelo mapeamento da entidade Categoria
    /// </summary>
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            // Request -> Entity
            CreateMap<CriarCategoriaRequest, Categoria>();

            // Entity -> Request
            CreateMap<Categoria, CategoriaResponse>();
        }
    }
}