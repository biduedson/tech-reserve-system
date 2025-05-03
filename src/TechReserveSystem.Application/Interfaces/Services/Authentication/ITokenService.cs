namespace TechReserveSystem.Application.Interfaces.Services.Authentication
{
    public interface ITokenService
    {
        string GenerateToken(Guid userId, string email, string role);
        DateTime ExperiOn(string token);
    }
}