using FluentValidation;
using TechReserveSystem.Application.Validations.User.Interface;
using TechReserveSystem.Shared.Communication.Request.User;
using TechReserveSystem.Shared.Exceptions.Constants;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Validation;
using TechReserveSystem.Shared.Resources;

namespace TechReserveSystem.Application.Validations.User.Implementations
{
    public class UserValidation : AbstractValidator<RequestRegisterUserJson>, IUserValidation
    {
        public UserValidation()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceAppMessages.GetExceptionMessage(UserMessagesExceptions.USER_NAME_EMPTY));
            RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceAppMessages.GetExceptionMessage(UserMessagesExceptions.EMAIL_EMPTY));
            RuleFor(user => user.Password).NotEmpty().WithMessage(ResourceAppMessages.GetExceptionMessage(UserMessagesExceptions.PASSWORD_EMPTY));
            RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceAppMessages.GetExceptionMessage(UserMessagesExceptions.PASSWORD_LENGTH_INVALID)); ;
        }

        public void Validation(RequestRegisterUserJson request)
        {
            var result = Validate(request);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}