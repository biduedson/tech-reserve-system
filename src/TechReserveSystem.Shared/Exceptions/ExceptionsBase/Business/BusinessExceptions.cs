namespace TechReserveSystem.Shared.Exceptions.ExceptionsBase.Business
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }
    }
}