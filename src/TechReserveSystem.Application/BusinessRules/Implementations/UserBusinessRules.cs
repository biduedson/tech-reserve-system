using TechReserveSystem.Application.BusinessRules.Interfaces;
using TechReserveSystem.Domain.Interfaces.Repositories.UserRepository;

namespace TechReserveSystem.Application.BusinessRules.Implementations
{
    public class UserBusinessRules : IUserBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public UserBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> VerifyExistingEmail(string email)
        {
            var isEmailExists = await _userRepository.GetByEmail(email);
            return isEmailExists != null;
        }
    }
}