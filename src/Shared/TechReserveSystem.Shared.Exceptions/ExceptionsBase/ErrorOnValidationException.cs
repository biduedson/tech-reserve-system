namespace TechReserveSystem.Shared.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : MyTechReserveSystemException
    {
        public IList<string> ErrorMessages { get; set; }
        public ErrorOnValidationException(IList<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}