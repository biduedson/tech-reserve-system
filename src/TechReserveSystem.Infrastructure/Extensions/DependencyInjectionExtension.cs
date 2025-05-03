using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechReserveSystem.Application.Interfaces.Services.Authentication;
using TechReserveSystem.Application.Interfaces.Services.Security;
using TechReserveSystem.Domain.Interfaces.Repositories;
using TechReserveSystem.Domain.Interfaces.Repositories.UserRepository;
using TechReserveSystem.Infrastructure.Configuration;
using TechReserveSystem.Infrastructure.Data.Context;
using TechReserveSystem.Infrastructure.Data.Repositories;
using TechReserveSystem.Infrastructure.Data.Repositories.UserRepository;
using TechReserveSystem.Infrastructure.Services.Authentication;
using TechReserveSystem.Infrastructure.Services.Security;

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
            var jwtSection = configuration.GetSection("Jwt");
            services.Configure<JwtSettings>(jwtSection);
            services.AddSingleton<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            AddPasswordEncryptService(services, configuration);
        }

        public static void AddPasswordEncryptService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPasswordHashService>(provider =>
            {
                var additionalKey = configuration.GetValue<string>("Settings:Passwords:AdditionalKey");
                return new PasswordHashService(configuration, additionalKey!);
            });
        }
    }
}