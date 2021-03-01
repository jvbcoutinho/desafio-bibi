using Bibi.Application.ArquivoAggregate;
using Bibi.Application.VerificadorAntiCorruption;
using Bibi.Application.VirusTotal;
using Bibi.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bibi.Application
{
    public static class ConfigurationModule
    {
        public static void RegisterBibiService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IArquivoService, ArquivoService>();
            services.AddSingleton<IVerificadorFacade, VirusTotalService>();

            services.RegisterRepository("Conexao");
        }

    }
}