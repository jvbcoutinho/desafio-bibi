
using Bibi.Domain;
using Bibi.Repositoy.Repository;
using BlueModas.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bibi.Repository
{
    public static class ConfigurationModule
    {
        public static void RegisterRepository(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BibiContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });

            services.AddTransient<IArquivoRepository, ArquivoRepository>();
        }
    }
}