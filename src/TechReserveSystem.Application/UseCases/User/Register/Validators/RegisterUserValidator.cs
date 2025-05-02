using FluentValidation;
using TechReserveSystem.Shared.Communication.Request.User;

namespace TechReserveSystem.Application.UseCases.User.Register.Validators
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage("");
            RuleFor(user => user.Email).NotEmpty().WithMessage("");
            RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage("");
        }
    }
}