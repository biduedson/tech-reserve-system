using AutoMapper;
using TechReserveSystem.Application.Interfaces.UseCases.Equipment;
using TechReserveSystem.Application.Validators.Equipment;
using TechReserveSystem.Domain.Interfaces.Repositories;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentCategoryRepository;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentRepository;
using TechReserveSystem.Shared.Communication.Request.Equipment;
using TechReserveSystem.Shared.Communication.Response.Equipment;
using TechReserveSystem.Shared.Exceptions.Constants;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Validation;
using TechReserveSystem.Shared.Resources;

namespace TechReserveSystem.Application.UseCases.Equipment
{
    public class RegisterEquipmentUseCase : IRegisterEquipmentUseCase
    {
        private readonly IEquipmentRepository _repository;
        private readonly IEquipmentCategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterEquipmentUseCase(
            IEquipmentRepository repository,
            IEquipmentCategoryRepository categoryRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork
        )
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseRegisteredEquipmentJson> Execute(RequestRegisterEquipmentJson request)
        {
            await Validate(request);
            var equipment = _mapper.Map<Domain.Entities.Equipment>(request);
            await _repository.Add(equipment);
            await _unitOfWork.Commit();
            var category = await _categoryRepository.GetById(request.CategoryId);

            return new ResponseRegisteredEquipmentJson
            {
                Name = request.Name,
                Description = request.Description,
                Category = category!.Name,
            };
        }

        private async Task Validate(RequestRegisterEquipmentJson request)
        {
            var validator = new RegisterEquipmentValidator();
            var result = validator.Validate(request);

            await CheckEquipmentExists(request.Name, result);
            await CheckCategoryExists(request.CategoryId, result);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }

        private async Task CheckEquipmentExists(string name, FluentValidation.Results.ValidationResult result)
        {
            var ExistEquipmentWithName = await _repository.ExistEquipmentWithName(name);
            if (ExistEquipmentWithName)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceAppMessages.GetExceptionMessage(EquipmentMessagesExceptions.EQUIPMENT_ALREADY_REGISTERED)));
            }
        }

        private async Task CheckCategoryExists(Guid categoryId, FluentValidation.Results.ValidationResult result)
        {
            var categoryExists = await _categoryRepository.GetById(categoryId);
            if (categoryExists is null)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceAppMessages.GetExceptionMessage(NotFoundMessagesExceptions.CATEGORY_NOT_FOUND)));
            }
        }
    }
}