namespace TechReserveSystem.Shared.Exceptions.ExceptionsBase.Authentication
{
    public class AuthenticationException : MyTechReserveSystemException
    {
        public string ErrorMessage { get; set; }
        public AuthenticationException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}