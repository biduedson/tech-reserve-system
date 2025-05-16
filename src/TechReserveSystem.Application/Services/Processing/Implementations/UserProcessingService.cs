using AutoMapper;
using TechReserveSystem.Application.BusinessRules.Interfaces;
using TechReserveSystem.Application.Services.Processing.Interfaces;
using TechReserveSystem.Application.Services.Security;
using TechReserveSystem.Application.Validations.User.Interface;
using TechReserveSystem.Domain.Entities;
using TechReserveSystem.Domain.Interfaces.Repositories;
using TechReserveSystem.Domain.Interfaces.Repositories.UserRepository;
using TechReserveSystem.Shared.Communication.Request.User;
using TechReserveSystem.Shared.Communication.Response.User;
using TechReserveSystem.Shared.Exceptions.Constants;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Business;
using TechReserveSystem.Shared.Resources;

namespace TechReserveSystem.Application.Services.Processing.Implementations
{
    public class UserProcessingService : IUserProcessingService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserBusinessRules _userBusinessRules;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserValidation _userValidation;
        private readonly IPasswordHashService _passwordHashService;
        public UserProcessingService
        (
            IUserRepository userRepository,
            IMapper mapper,
            IUserBusinessRules userBusinessRules,
            IUnitOfWork unitOfWork,
            IUserValidation userValidation,
            IPasswordHashService passwordHashService
        )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
            _unitOfWork = unitOfWork;
            _userValidation = userValidation;
            _passwordHashService = passwordHashService;
        }

        public async Task<User> Register(RequestRegisterUserJson request)
        {
            EnsureValidationRules(request);
            await EnsureBusinessRules(request);
            var user = MapUserRequestToEntity(request);
            await PersistUser(user);
            return user;
        }

        private void EnsureValidationRules(RequestRegisterUserJson request)
        {
            _userValidation.Validation(request);
        }

        private async Task EnsureBusinessRules(RequestRegisterUserJson request)
        {
            bool isEmailExists = await _userBusinessRules.VerifyExistingEmail(request.Email);
            if (isEmailExists)
                throw new BusinessException(ResourceAppMessages.GetExceptionMessage(UserMessagesExceptions.EMAIL_ALREADY_REGISTERED));
        }

        private User MapUserRequestToEntity(RequestRegisterUserJson request)
        {
            var user = _mapper.Map<User>(request);
            user.Password = _passwordHashService.GeneratePasswordEncrypt(request.Password);
            return user;
        }

        private async Task PersistUser(User user)
        {
            await _userRepository.Add(user);
            await _unitOfWork.Commit();
        }

    }
}