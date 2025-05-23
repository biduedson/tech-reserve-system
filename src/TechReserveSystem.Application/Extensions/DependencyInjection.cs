using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechReserveSystem.Application.BusinessRules.Implementations;
using TechReserveSystem.Application.BusinessRules.Interfaces;
using TechReserveSystem.Application.Interfaces.UseCases.Equipment;
using TechReserveSystem.Application.Interfaces.UseCases.EquipmentCategory;
using TechReserveSystem.Application.Interfaces.UseCases.EquipmentReservation;
using TechReserveSystem.Application.Interfaces.UseCases.Login;
using TechReserveSystem.Application.Services.AutoMapper;
using TechReserveSystem.Application.Services.Processing.Implementations;
using TechReserveSystem.Application.Services.Processing.Interfaces;
using TechReserveSystem.Application.Services.Reservation;
using TechReserveSystem.Application.Services.Response.Implementations;
using TechReserveSystem.Application.Services.Responses.Interfaces;
using TechReserveSystem.Application.UseCases.Equipment;
using TechReserveSystem.Application.UseCases.EquipmentCategory;
using TechReserveSystem.Application.UseCases.EquipmentReservation;
using TechReserveSystem.Application.UseCases.Login;
using TechReserveSystem.Application.UseCases.User.Register;
using TechReserveSystem.Application.Validations.Equipment.Implementations;
using TechReserveSystem.Application.Validations.Equipment.Interfaces;
using TechReserveSystem.Application.Validations.Reservation.Implementations;
using TechReserveSystem.Application.Validations.Reservation.interfaces;
using TechReserveSystem.Application.Validations.User.Implementations;
using TechReserveSystem.Application.Validations.User.Interface;
using TechReserveSystem.Domain.Interfaces.Services;

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
            AddValidations(services);
            AddServices(services);
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapping));
        }
        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IRegisterEquipmentUseCase, RegisterEquipmentUseCase>();
            services.AddScoped<IRegisterEquipmentCategoryUseCase, RegisterEquipmentCategoryUseCase>();
            services.AddScoped<IRegisterReservationUseCase, RegisterReservationUseCase>();
            services.AddScoped<ILoginUseCase, LoginUseCase>();
        }

        private static void AddProcessingServices(IServiceCollection services)
        {
            services.AddScoped<IEquipmentProcessingService, EquipmentProcessingService>();
            services.AddScoped<IReservationProcessingService, ReservationProcessingService>();
            services.AddScoped<IUserProcessingService, UserProcessingService>();
        }

        private static void AddBusinessRules(IServiceCollection services)
        {
            services.AddScoped<IEquipmentBusinessRules, EquipmentBusinessRules>();
            services.AddScoped<IReservationBusinessRules, ReservationBusinessRules>();
            services.AddScoped<IUserBusinessRules, UserBusinessRules>();
        }
        private static void AddValidations(IServiceCollection services)
        {
            services.AddScoped<IEquipmentValidation, EquipmentValidation>();
            services.AddScoped<IReservationValidation, ReservationValidation>();
            services.AddScoped<IUserValidation, UserValidation>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IResponseService<>), typeof(ResponseService<>));
            services.AddScoped<IReservationService, ReservationService>();
        }

    }
}