using TechReserveSystem.Application.Interfaces.Services.Validations;
using TechReserveSystem.Application.Validators.Equipment;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentCategoryRepository;
using TechReserveSystem.Shared.Communication.Request.Equipment;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Validation;

namespace TechReserveSystem.Application.Services.Validations
{
    public class EquipmentValidationService : IEquipmentValidationService
    {

        private readonly IEquipmentCategoryRepository _equipmentCategoryRepository;
        public EquipmentValidationService(IEquipmentCategoryRepository equipmentCategoryRepository)
        {

            _equipmentCategoryRepository = equipmentCategoryRepository;
        }
        public void ValidateEquipmentRegisterRequest(RequestRegisterEquipmentJson request)
        {
            var validator = new RegisterEquipmentValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }


    }
}