using TechReserveSystem.Shared.Communication.Request.User;

namespace TechReserveSystem.Application.Validations.User.Interface
{
    public interface IUserValidation
    {
        void Validation(RequestRegisterUserJson request);
    }
}