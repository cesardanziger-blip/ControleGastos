using ControleGastos.Api.Models.Responses;

namespace ControleGastos.Api.Interfaces
{
    public interface IRelatorioService
    {
        public Task<RelatorioGeralResponse> TotaisCompletos();
    }
}