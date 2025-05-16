namespace TechReserveSystem.Application.Services.Security
{
    public interface IPasswordHashService
    {
        string GeneratePasswordEncrypt(string password);
    }
}