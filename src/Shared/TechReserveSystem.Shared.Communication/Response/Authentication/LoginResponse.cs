namespace TechReserveSystem.Shared.Communication.Response.Authentication
{
    public class LoginResponse
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresOn { get; set; }


    }

}