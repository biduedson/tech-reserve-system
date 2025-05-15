using AutoMapper;
using TechReserveSystem.Application.BusinessRules.Interfaces;
using TechReserveSystem.Application.Services.Processing.Interfaces;
using TechReserveSystem.Application.Validations.Equipment;
using TechReserveSystem.Application.Validations.Equipment.Interfaces;
using TechReserveSystem.Domain.Entities;
using TechReserveSystem.Domain.Interfaces.Repositories;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentRepository;
using TechReserveSystem.Shared.Communication.Request.Equipment;
using TechReserveSystem.Shared.Communication.Response.Equipment;
using TechReserveSystem.Shared.Exceptions.Constants;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Business;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Validation;
using TechReserveSystem.Shared.Resources;

namespace TechReserveSystem.Application.Services.Processing.Implementations
{
    public class EquipmentProcessingService : IEquipmentProcessingService
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IEquipmentValidation _equipmentValidation;
        private readonly IEquipmentBusinessRules _equipmentBusinessRules;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public EquipmentProcessingService(
            IEquipmentRepository equipmentRepository,
            IEquipmentValidation equipmentValidation,
            IEquipmentBusinessRules equipmentBusinessRules,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _equipmentRepository = equipmentRepository;
            _equipmentValidation = equipmentValidation;
            _equipmentBusinessRules = equipmentBusinessRules;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseRegisteredEquipmentJson> Register(RequestRegisterEquipmentJson request)
        {
            var result = new ResponseRegisteredEquipmentJson();
            EnsureValidationRules(request);
            await EnsureBusinessRules(request, result);
            return result;

        }

        private void EnsureValidationRules(RequestRegisterEquipmentJson request)
        {
            _equipmentValidation.Validation(request);
        }

        private async Task EnsureBusinessRules(RequestRegisterEquipmentJson request, ResponseRegisteredEquipmentJson result)
        {
            var equipmentNameAvailable = await _equipmentBusinessRules.IsEquipmentNameAvailable(request.Name);
            var category = await _equipmentBusinessRules.EnsureCategoryExists(request.CategoryId);
            var isAvailableQuantityValid = _equipmentBusinessRules.IsAvailableQuantityValid(request.AvailableQuantity);

            if (!equipmentNameAvailable)
                throw new BusinessException(ResourceAppMessages.GetExceptionMessage(EquipmentMessagesExceptions.EQUIPMENT_ALREADY_REGISTERED));

            if (category is null)
                throw new BusinessException(ResourceAppMessages.GetExceptionMessage(NotFoundMessagesExceptions.CATEGORY_NOT_FOUND));

            if (!isAvailableQuantityValid)
                throw new BusinessException(ResourceAppMessages.GetExceptionMessage(EquipmentMessagesExceptions.AVAILABLE_QUANTITY_EQUIPMENT_INVALID));

            var equipment = _mapper.Map<Equipment>(request);
            await _equipmentRepository.Add(equipment);
            await _unitOfWork.Commit();

            result.Name = equipment.Name;
            result.Description = equipment.Description;
            result.Category = category.Name;
        }
    }
}