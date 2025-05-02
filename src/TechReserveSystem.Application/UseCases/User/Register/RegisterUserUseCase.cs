using AutoMapper;
using TechReserveSystem.Application.Services.Cryptography;
using TechReserveSystem.Application.UseCases.User.Register.Validators;
using TechReserveSystem.Domain.Entities;
using TechReserveSystem.Domain.Interfaces.Repositories;
using TechReserveSystem.Domain.Interfaces.Repositories.UserRepository;
using TechReserveSystem.Shared.Communication.Request.User;
using TechReserveSystem.Shared.Communication.Response.User;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase;
using TechReserveSystem.Shared.Exceptions.Resources;

namespace TechReserveSystem.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        public readonly IUserRepository _repository;
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public readonly PasswordEncripter _passwordEncripter;

        public RegisterUserUseCase(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            PasswordEncripter passwordEncripter
        )
        {
            _repository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordEncripter = passwordEncripter;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {
            await Validate(request);

            var user = _mapper.Map<Domain.Entities.User>(request);

            user.Password = _passwordEncripter.Encrypt(request.Password);

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
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}