using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechReserveSystem.Application.Interfaces.UseCases.Login;
using TechReserveSystem.Application.Services.AutoMapper;
using TechReserveSystem.Application.UseCases.Login;
using TechReserveSystem.Application.UseCases.User.Register;

namespace TechReserveSystem.Application.Extensions
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddAutoMapper(services);
            AddUseCases(services);
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddScoped(option =>
            new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper());
        }
        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<ILoginUseCase, LoginUseCase>();
        }

    }
}