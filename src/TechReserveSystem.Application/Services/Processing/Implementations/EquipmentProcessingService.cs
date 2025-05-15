using AutoMapper;
using TechReserveSystem.Application.BusinessRules.Interfaces;
using TechReserveSystem.Application.Services.Processing.Interfaces;
using TechReserveSystem.Application.Validations.Equipment;
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
        private readonly EquipmentValidation _equipmentValidation;
        private readonly IEquipmentBusinessRules _equipmentBusinessRules;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public EquipmentProcessingService(
            IEquipmentRepository equipmentRepository,
            EquipmentValidation equipmentValidation,
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

        public async Task<ResponseRegisteredEquipmentJson> RegisterEquipment(RequestRegisterEquipmentJson request)
        {
            var validationResult = _equipmentValidation.Validate(request);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
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

            return new ResponseRegisteredEquipmentJson
            {
                Name = equipment.Name,
                Description = equipment.Description,
                Category = category.Name
            };
        }
    }
}