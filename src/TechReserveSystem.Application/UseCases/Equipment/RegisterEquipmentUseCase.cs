using AutoMapper;
using TechReserveSystem.Application.Interfaces.Services.Validations;
using TechReserveSystem.Application.Interfaces.UseCases.Equipment;
using TechReserveSystem.Application.Services.Processing.Interfaces;
using TechReserveSystem.Application.Validators.Equipment;
using TechReserveSystem.Domain.Interfaces.Repositories;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentCategoryRepository;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentRepository;
using TechReserveSystem.Shared.Communication.Request.Equipment;
using TechReserveSystem.Shared.Communication.Response.Equipment;
using TechReserveSystem.Shared.Exceptions.Constants;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Business;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Validation;
using TechReserveSystem.Shared.Resources;

namespace TechReserveSystem.Application.UseCases.Equipment
{
    public class RegisterEquipmentUseCase : IRegisterEquipmentUseCase
    {

        private readonly IEquipmentProcessingService _equipmentProcessingService;

        public RegisterEquipmentUseCase(IEquipmentProcessingService equipmentProcessingService)
        {
            _equipmentProcessingService = equipmentProcessingService;
        }

        public async Task<ResponseRegisteredEquipmentJson> Execute(RequestRegisterEquipmentJson request)
        {
            var result = await _equipmentProcessingService.Register(request);
            return result;
        }

    }
}