using TechReserveSystem.Shared.Communication.Request.Authentication;
using TechReserveSystem.Shared.Communication.Response.Authentication;

namespace TechReserveSystem.Application.Interfaces.UseCases.Login
{
    public interface ILoginUseCase
    {
        Task<LoginResponse> Execute(UserLoginRequest request);
    }
}