using AutoMapper;
using ControleGastos.Api.Models.Requests;
using ControleGastos.Api.Models.Responses;
using ControleGastos.DataAccess.Entities;

namespace ControleGastos.Api.Profiles
{
    /// <summary>
    /// Profile responsável pelo mapeamento da entidade Transacao
    /// </summary>
    public class TransacaoProfile : Profile
    {
        public TransacaoProfile()
        {
            // Request -> Entity
            CreateMap<CriarTransacaoRequest, Transacao>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID será gerado no service
                .ForMember(dest => dest.Pessoa, opt => opt.Ignore()) // já vinculamos via repository
                .ForMember(dest => dest.Categoria, opt => opt.Ignore());

            // Entity -> Request
            CreateMap<Transacao, TransacaoResponse>()
                .ForMember(dest => dest.Pessoa, opt => opt.MapFrom(src => src.Pessoa.Nome))
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria.Descricao));
        }
    }
}