using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechReserveSystem.Application.Interfaces.UseCases.Equipment;
using TechReserveSystem.Application.Interfaces.UseCases.EquipmentCategory;
using TechReserveSystem.Application.Interfaces.UseCases.EquipmentReservation;
using TechReserveSystem.Application.Interfaces.UseCases.Login;
using TechReserveSystem.Application.Services.AutoMapper;
using TechReserveSystem.Application.UseCases.Equipment;
using TechReserveSystem.Application.UseCases.EquipmentCategory;
using TechReserveSystem.Application.UseCases.EquipmentReservation;
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
            services.AddScoped<IRegisterEquipmentUseCase, RegisterEquipmentUseCase>();
            services.AddScoped<IRegisterEquipmentCategoryUseCase, RegisterEquipmentCategoryUseCase>();
            services.AddScoped<IRegisterReservationUseCase, RegisterReservationUseCase>();
            services.AddScoped<ILoginUseCase, LoginUseCase>();
        }

    }
}