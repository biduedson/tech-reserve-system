using FluentValidation;
using TechReserveSystem.Shared.Communication.Request.User;
using TechReserveSystem.Shared.Exceptions.Constants;
using TechReserveSystem.Shared.Resources;

namespace TechReserveSystem.Application.Validators.User
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceAppMessages.GetExceptionMessage(UserMessagesExceptions.USER_NAME_EMPTY));
            RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceAppMessages.GetExceptionMessage(UserMessagesExceptions.EMAIL_EMPTY));
            RuleFor(user => user.Password).NotEmpty().WithMessage(ResourceAppMessages.GetExceptionMessage(UserMessagesExceptions.PASSWORD_EMPTY));
            RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceAppMessages.GetExceptionMessage(UserMessagesExceptions.PASSWORD_LENGTH_INVALID)); ;
        }
    }
}