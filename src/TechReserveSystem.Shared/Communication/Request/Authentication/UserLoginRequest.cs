namespace TechReserveSystem.Shared.Communication.Request.Authentication
{
    public class UserLoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}