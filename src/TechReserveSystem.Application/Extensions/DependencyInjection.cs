using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechReserveSystem.Application.BusinessRules.Implementations;
using TechReserveSystem.Application.BusinessRules.Interfaces;
using TechReserveSystem.Application.Interfaces.Services.Validations;
using TechReserveSystem.Application.Interfaces.UseCases.Equipment;
using TechReserveSystem.Application.Interfaces.UseCases.EquipmentCategory;
using TechReserveSystem.Application.Interfaces.UseCases.EquipmentReservation;
using TechReserveSystem.Application.Interfaces.UseCases.Login;
using TechReserveSystem.Application.Services.AutoMapper;
using TechReserveSystem.Application.Services.Processing.Implementations;
using TechReserveSystem.Application.Services.Processing.Interfaces;
using TechReserveSystem.Application.Services.Validations;
using TechReserveSystem.Application.UseCases.Equipment;
using TechReserveSystem.Application.UseCases.EquipmentCategory;
using TechReserveSystem.Application.UseCases.EquipmentReservation;
using TechReserveSystem.Application.UseCases.Login;
using TechReserveSystem.Application.UseCases.User.Register;
using TechReserveSystem.Application.Validations.Equipment;

namespace TechReserveSystem.Application.Extensions
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddAutoMapper(services);
            AddUseCases(services);
            AddProcessingServices(services);
            AddBusinessRules(services);
            AddValiddations(services);
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
            services.AddScoped<IReservationValidationService, ReservationValidationService>();
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IRegisterEquipmentUseCase, RegisterEquipmentUseCase>();
            services.AddScoped<IRegisterEquipmentCategoryUseCase, RegisterEquipmentCategoryUseCase>();
            services.AddScoped<IRegisterReservationUseCase, RegisterReservationUseCase>();
            services.AddScoped<ILoginUseCase, LoginUseCase>();
        }

        private static void AddProcessingServices(IServiceCollection services)
        {
            services.AddScoped<IEquipmentProcessingService, EquipmentProcessingService>();
        }

        private static void AddBusinessRules(IServiceCollection services)
        {
            services.AddScoped<IEquipmentBusinessRules, EquipmentBusinessRules>();
        }
        private static void AddValiddations(IServiceCollection services)
        {
            services.AddScoped<EquipmentValidation>();
        }

    }
}