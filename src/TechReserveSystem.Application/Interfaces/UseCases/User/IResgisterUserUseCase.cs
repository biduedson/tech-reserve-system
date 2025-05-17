using TechReserveSystem.Shared.Communication.Request.User;
using TechReserveSystem.Shared.Communication.Response;
using TechReserveSystem.Shared.Communication.Response.User;

namespace TechReserveSystem.Application.UseCases.User.Register
{
    public interface IRegisterUserUseCase
    {
        public Task<Response<ResponseRegisteredUserJson>> Execute(RequestRegisterUserJson request);
    }
}