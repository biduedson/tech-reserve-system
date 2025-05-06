using AutoMapper;
using TechReserveSystem.Application.Interfaces.UseCases.EquipmentCategory;
using TechReserveSystem.Application.UseCases.EquipmentCategory.Validators;
using TechReserveSystem.Domain.Interfaces.Repositories;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentCategoryRepository;
using TechReserveSystem.Shared.Communication.Request.EquipmentCategory;
using TechReserveSystem.Shared.Communication.Response.EquipmentCategory;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Validation;
using TechReserveSystem.Shared.Exceptions.Resources;

namespace TechReserveSystem.Application.UseCases.EquipmentCategory
{
    public class RegisterEquipmentCategoryUseCase : IRegisterEquipmentCategoryUseCase
    {
        private readonly IEquipmentCategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterEquipmentCategoryUseCase(IEquipmentCategoryRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RegisterEquipmentCategoryResponse> Execute(EquipmentCategoryRegisterRequest request)
        {
            await Validate(request);
            var category = _mapper.Map<Domain.Entities.EquipmentCategory>(request);
            await _repository.Add(category);
            await _unitOfWork.Commit();
            return new RegisterEquipmentCategoryResponse
            {
                Name = request.Name
            };

        }

        private async Task Validate(EquipmentCategoryRegisterRequest request)
        {
            var validator = new RegisterCategoryValidator();
            var result = validator.Validate(request);
            await CheckCategoryExists(request.Name, result);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }

        private async Task CheckCategoryExists(string name, FluentValidation.Results.ValidationResult result)
        {
            var categoryExists = await _repository.ExistEquipmentCategoryWithName(name);
            if (categoryExists)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.CATEGORY_ALREADY_REGISTERED));
        }
    }
}