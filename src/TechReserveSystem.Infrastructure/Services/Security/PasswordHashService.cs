using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using TechReserveSystem.Application.Services.Security;

namespace TechReserveSystem.Infrastructure.Services.Security
{
    public class PasswordHashService : IPasswordHashService
    {
        private readonly IConfiguration _configuration;
        private readonly string _additionalKey;

        public PasswordHashService(IConfiguration configuration, string additionalKey)
        {
            _configuration = configuration;
            _additionalKey = additionalKey;
        }

        public string GeneratePasswordEncrypt(string password)
        {
            return PasswordEncrypt(password);
        }
        private string PasswordEncrypt(string password)
        {
            var passwordHash = $"{password}{_additionalKey}";
            var bytes = Encoding.UTF8.GetBytes(passwordHash);
            var hashbytes = SHA512.HashData(bytes);
            return StringBytes(hashbytes);
        }

        private string StringBytes(byte[] bytes)
        {
            var sb = new StringBuilder();

            foreach (var b in bytes)
            {

                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
    }
}