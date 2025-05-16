using TechReserveSystem.Domain.Entities;
using TechReserveSystem.Shared.Communication.Request.User;

namespace TechReserveSystem.Application.Services.Processing.Interfaces
{
    public interface IUserProcessingService
    {
        Task<User> Register(RequestRegisterUserJson request);
    }
}