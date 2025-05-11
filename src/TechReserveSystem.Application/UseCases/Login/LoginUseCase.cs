using AutoMapper;
using TechReserveSystem.Application.Interfaces.Services.Authentication;
using TechReserveSystem.Application.Interfaces.Services.Security;
using TechReserveSystem.Application.Interfaces.UseCases.Login;
using TechReserveSystem.Domain.Interfaces.Repositories.UserRepository;
using TechReserveSystem.Shared.Communication.Request.Authentication;
using TechReserveSystem.Shared.Communication.Response.Authentication;
using TechReserveSystem.Shared.Exceptions.Constants;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Validation;
using TechReserveSystem.Shared.Resources;

namespace TechReserveSystem.Application.UseCases.Login
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHashService _passwordHashService;

        public LoginUseCase(
           IUserRepository repository,
           IMapper mapper,
           ITokenService tokenService,
           IPasswordHashService passwordHashService
        )
        {
            _repository = repository;
            _mapper = mapper;
            _tokenService = tokenService;
            _passwordHashService = passwordHashService;
        }

        public async Task<LoginResponse> Execute(UserLoginRequest request)
        {
            var loggedInUser = await Authenticate(request);
            var response = new LoginResponse();
            return loggedInUser;
        }

        private async Task<LoginResponse> Authenticate(UserLoginRequest request)
        {
            var passwordHash = _passwordHashService.GeneratePasswordEncrypt(request.Password);

            var user = await _repository.GetByEmail(request.Email);

            if (user == null || user.Password != passwordHash)
            {
                var errors = new List<string>();
                errors.Add(ResourceAppMessages.GetExceptionMessage(AuthMessagesExceptions.INVALID_CREDENTIALS));
                throw new ErrorOnValidationException(errors);
            }

            var token = _tokenService.GenerateToken(user.Id, user.Email, user.Role);
            var expiresOn = _tokenService.ExperiOn(token);
            var loggedInUser = new LoginResponse
            {
                Name = user.Name,
                Email = user.Email,
                Token = token,
                ExpiresOn = expiresOn
            };

            return loggedInUser;
        }
    }
}