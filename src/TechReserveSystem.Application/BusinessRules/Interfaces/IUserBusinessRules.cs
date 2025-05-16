
namespace TechReserveSystem.Application.BusinessRules.Interfaces
{
    public interface IUserBusinessRules
    {
        Task<bool> VerifyExistingEmail(string email);
    }
}