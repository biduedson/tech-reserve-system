using FluentValidation;
using TechReserveSystem.Shared.Communication.Request.EquipmentCategory;
using TechReserveSystem.Shared.Exceptions.Constants;
using TechReserveSystem.Shared.Resources;

namespace TechReserveSystem.Application.Validators.EquipmentCategory
{
    public class RegisterCategoryValidator : AbstractValidator<EquipmentCategoryRegisterRequest>
    {
        public RegisterCategoryValidator()
        {
            RuleFor(category => category.Name)
            .NotEmpty().WithMessage(ResourceAppMessages.GetExceptionMessage(CategoryMessagesExceptions.CATEGORY_NAME_EMPTY))
            .NotNull().WithMessage(ResourceAppMessages.GetExceptionMessage(CategoryMessagesExceptions.CATEGORY_NAME_INVALID));

            RuleFor(category => category.Description)
            .NotEmpty().WithMessage(ResourceAppMessages.GetExceptionMessage(CategoryMessagesExceptions.CATEGORY_DESCRIPTION_EMPTY))
            .NotNull().WithMessage(ResourceAppMessages.GetExceptionMessage(CategoryMessagesExceptions.CATEGORY_DESCRIPTION_INVALID));
        }
    }
}
