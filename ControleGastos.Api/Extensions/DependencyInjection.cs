using ControleGastos.Api.Interfaces;
using ControleGastos.Api.Services;
using ControleGastos.DataAccess.Interfaces;
using ControleGastos.DataAccess.Repositories;

namespace ControleGastos.Api.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IPessoaService, PessoaService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<ITransacaoService, TransacaoService>();
            services.AddScoped<IRelatorioService, RelatorioService>();

            // Repositories
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<ITransacaoRepository, TransacaoRepository>();

            return services;
        }
    }
}