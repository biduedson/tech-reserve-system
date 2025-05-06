using FluentValidation;
using TechReserveSystem.Shared.Communication.Request.EquipmentCategory;
using TechReserveSystem.Shared.Exceptions.Resources;

namespace TechReserveSystem.Application.Validators.EquipmentCategory
{
    public class RegisterCategoryValidator : AbstractValidator<EquipmentCategoryRegisterRequest>
    {
        public RegisterCategoryValidator()
        {
            RuleFor(category => category.Name)
            .NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY)
            .NotNull().WithMessage(ResourceMessagesException.NAME_EMPTY);

            RuleFor(category => category.Description)
            .NotEmpty().WithMessage(ResourceMessagesException.DESCRIPTION_EMPTY)
            .NotNull().WithMessage(ResourceMessagesException.DESCRIPTION_EMPTY);
        }
    }
}
