namespace TechReserveSystem.Shared.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : MyTechReserveSystemException
    {
        public IList<string> ErroMessages { get; set; }
        public ErrorOnValidationException(IList<string> erroMessages)
        {
            ErroMessages = erroMessages;
        }
    }
}