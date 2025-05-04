using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TechReserveSystem.Application.Interfaces.Services.Authentication;
using TechReserveSystem.Application.Interfaces.Services.Security;
using TechReserveSystem.Domain.Interfaces.Repositories;
using TechReserveSystem.Domain.Interfaces.Repositories.UserRepository;
using TechReserveSystem.Infrastructure.Configuration;
using TechReserveSystem.Infrastructure.Data.Context;
using TechReserveSystem.Infrastructure.Data.Repositories;
using TechReserveSystem.Infrastructure.Data.Repositories.UserRepository;
using TechReserveSystem.Infrastructure.Security.Authentication;
using TechReserveSystem.Infrastructure.Services.Authentication;
using TechReserveSystem.Infrastructure.Services.Security;

namespace TechReserveSystem.Infrastructure.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            AddDbContextConfig(services, configuration);
            AddTokenValidationService.ConfigAuthentication(services, configuration);
            AddTokenService(services, configuration);
            AddPasswordEncryptService(services, configuration);
            AddRepositories(services, configuration);
        }

        private static void AddPasswordEncryptService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IPasswordHashService>(provider =>
            {
                var additionalKey = configuration.GetValue<string>("Settings:Passwords:AdditionalKey");
                return new PasswordHashService(configuration, additionalKey!);
            });
        }


        private static void AddTokenService(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection("Jwt");
            services.Configure<JwtSettings>(jwtSection);
            services.AddSingleton<ITokenService, TokenService>();
        }

        private static void AddRepositories(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void AddDbContextConfig(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            var serverVersion = new MySqlServerVersion(new Version());
            services.AddDbContext<AppDbContext>(appDbContextOption =>
            {
                appDbContextOption.UseMySql(connectionString, serverVersion); ;
            });
        }
    }
}