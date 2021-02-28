using Bibi.Application.ArquivoAggregate;
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

            services.RegisterRepository("Conexao");
            // services.RegisterRepository(configuration.GetConnectionString("connectionString"));
        }

    }
}