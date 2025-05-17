
using TechReserveSystem.Application.Services.Processing.Interfaces;
using TechReserveSystem.Application.Services.Responses.Interfaces;
using TechReserveSystem.Shared.Communication.Request.User;
using TechReserveSystem.Shared.Communication.Response;
using TechReserveSystem.Shared.Communication.Response.User;


namespace TechReserveSystem.Application.UseCases.User.Register
{

    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IResponseService<ResponseRegisteredUserJson> _responseService;
        private readonly IUserProcessingService _userProcessingService;

        public RegisterUserUseCase(IResponseService<ResponseRegisteredUserJson> responseService, IUserProcessingService userProcessingService)
        {
            _responseService = responseService;
            _userProcessingService = userProcessingService;
        }

        public async Task<Response<ResponseRegisteredUserJson>> Execute(RequestRegisterUserJson request)
        {
            var newUser = await _userProcessingService.Register(request);

            var data = new ResponseRegisteredUserJson
            {
                Name = newUser.Name,
            };

            return _responseService.Success(data);
        }
    }
}