namespace TechReserveSystem.Application.Interfaces.Services.Security
{
    public interface IPasswordHashService
    {
        string GeneratePasswordEncrypt(string password);
    }
}