using AutoMapper;
using TechReserveSystem.Application.Interfaces.Services.Security;
using TechReserveSystem.Application.Validators.User;
using TechReserveSystem.Domain.Interfaces.Repositories;
using TechReserveSystem.Domain.Interfaces.Repositories.UserRepository;
using TechReserveSystem.Shared.Communication.Request.User;
using TechReserveSystem.Shared.Communication.Response.User;
using TechReserveSystem.Shared.Exceptions.Constants;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Validation;
using TechReserveSystem.Shared.Resources;

namespace TechReserveSystem.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordHashService _passwordHashService;

        public RegisterUserUseCase(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IPasswordHashService passwordHashService
        )
        {
            _repository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordHashService = passwordHashService;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {
            await Validate(request);

            var user = _mapper.Map<Domain.Entities.User>(request);

            user.Password = _passwordHashService.GeneratePasswordEncrypt(request.Password);

            await _repository.Add(user);
            await _unitOfWork.Commit();

            return new ResponseRegisteredUserJson
            {
                Name = request.Name,
            };
        }

        private async Task Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(request);

            var emailExists = await _repository.ExistActiveUserWithEmail(request.Email);

            if (emailExists)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceAppMessages.GetExceptionMessage(UserMessagesExceptions.EMAIL_ALREADY_REGISTERED)));

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }

    }
}