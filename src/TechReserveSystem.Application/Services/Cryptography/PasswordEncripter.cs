using System.Security.Cryptography;
using System.Text;

namespace TechReserveSystem.Application.Services.Cryptography
{
    public class PasswordEncripter
    {
        private readonly string _additionalKey;

        public PasswordEncripter(string additionalKey) => _additionalKey = additionalKey;

        public string Encrypt(string password)
        {
            var newPassword = $"{password} {_additionalKey}";
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashbytes = SHA3_512.HashData(bytes);

            return StringBytes(hashbytes);
        }

        private static string StringBytes(byte[] bytes)
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