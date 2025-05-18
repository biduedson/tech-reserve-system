
using TechReserveSystem.Application.Services.Processing.Interfaces;
using TechReserveSystem.Application.Services.Responses.Interfaces;
using TechReserveSystem.Shared.Communication.Request.User;
using TechReserveSystem.Shared.Communication.Response;
using TechReserveSystem.Shared.Communication.Response.User;


namespace TechReserveSystem.Application.UseCases.User.Register
{

    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserProcessingService _userProcessingService;

        public RegisterUserUseCase(IUserProcessingService userProcessingService)
        {

            _userProcessingService = userProcessingService;
        }

        public async Task<Response<ResponseRegisteredUserJson>> Execute(RequestRegisterUserJson request)
        {
            var response = await _userProcessingService.Register(request);
            return response;
        }
    }
}