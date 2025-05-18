

using TechReserveSystem.Shared.Communication.Request.User;
using TechReserveSystem.Shared.Communication.Response;
using TechReserveSystem.Shared.Communication.Response.User;

namespace TechReserveSystem.Application.Services.Processing.Interfaces
{
    public interface IUserProcessingService
    {
        Task<Response<ResponseRegisteredUserJson>> Register(RequestRegisterUserJson request);
    }
}