using AutoMapper;
using TechReserveSystem.Application.BusinessRules.Interfaces;
using TechReserveSystem.Application.Services.Processing.Interfaces;
using TechReserveSystem.Application.Services.Responses.Interfaces;
using TechReserveSystem.Application.Validations.Equipment;
using TechReserveSystem.Application.Validations.Equipment.Interfaces;
using TechReserveSystem.Domain.Entities;
using TechReserveSystem.Domain.Interfaces.Repositories;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentRepository;
using TechReserveSystem.Shared.Communication.Request.Equipment;
using TechReserveSystem.Shared.Communication.Response;
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
        private readonly IResponseService<ResponseRegisteredEquipmentJson> _responseService;
        public EquipmentProcessingService(
            IEquipmentRepository equipmentRepository,
            IEquipmentValidation equipmentValidation,
            IEquipmentBusinessRules equipmentBusinessRules,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IResponseService<ResponseRegisteredEquipmentJson> responseService
            )
        {
            _equipmentRepository = equipmentRepository;
            _equipmentValidation = equipmentValidation;
            _equipmentBusinessRules = equipmentBusinessRules;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _responseService = responseService;
        }

        public async Task<Response<ResponseRegisteredEquipmentJson>> Register(RequestRegisterEquipmentJson request)
        {
            EnsureValidationRules(request);
            var category = await EnsureBusinessRules(request);
            var newEquipment = MapEquipmentRequestToEntity(request);
            await PersistNewEquipment(newEquipment);
            return Response(newEquipment, category);

        }

        private void EnsureValidationRules(RequestRegisterEquipmentJson request)
        {
            _equipmentValidation.Validation(request);
        }

        private async Task<EquipmentCategory> EnsureBusinessRules(RequestRegisterEquipmentJson request)
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
            return category;
        }

        private Equipment MapEquipmentRequestToEntity(RequestRegisterEquipmentJson request)
        {
            return _mapper.Map<Equipment>(request);
        }

        private async Task PersistNewEquipment(Equipment equipment)
        {
            await _equipmentRepository.Add(equipment);
            await _unitOfWork.Commit();
        }
        private Response<ResponseRegisteredEquipmentJson> Response(Equipment equipment, EquipmentCategory category)
        {
            var response = new ResponseRegisteredEquipmentJson
            {
                Name = equipment.Name,
                Description = equipment.Description,
                Category = category.Name,
            };
            return _responseService.Success(response);
        }
    }
}