using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechReserveSystem.Domain.Interfaces.Repositories;
using TechReserveSystem.Domain.Interfaces.Repositories.UserRepository;
using TechReserveSystem.Infrastructure.Data.Context;
using TechReserveSystem.Infrastructure.Data.Repositories;
using TechReserveSystem.Infrastructure.Data.Repositories.UserRepository;

namespace TechReserveSystem.Infrastructure.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            var serverVersion = new MySqlServerVersion(new Version());
            services.AddDbContext<AppDbContext>(appDbContextOption =>
            {
                appDbContextOption.UseMySql(connectionString, serverVersion); ;
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}