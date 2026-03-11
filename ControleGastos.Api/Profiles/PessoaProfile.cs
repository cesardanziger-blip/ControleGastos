using AutoMapper;
using ControleGastos.Api.Models.Requests;
using ControleGastos.Api.Models.Responses;
using ControleGastos.DataAccess.Entities;

namespace ControleGastos.Api.Profiles
{
    /// <summary>
    /// Profile responsável pelo mapeamento da entidade Pessoa
    /// </summary>
    public class PessoaProfile : Profile
    {
        public PessoaProfile()
        {
            // Request -> Entity
            CreateMap<CriarPessoaRequest, Pessoa>();

            // Entity -> Response
            CreateMap<Pessoa, PessoaResponse>();
        }
    }
}