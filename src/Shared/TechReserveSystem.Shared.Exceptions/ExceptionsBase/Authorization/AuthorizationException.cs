namespace TechReserveSystem.Shared.Exceptions.ExceptionsBase.Authorization
{
    public class AuthorizationException : MyTechReserveSystemException
    {
        public string ErrorMessage { get; set; }
        public AuthorizationException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}