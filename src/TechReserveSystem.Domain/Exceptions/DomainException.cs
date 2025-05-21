namespace TechReserveSystem.Domain.Exceptions
{
    public abstract class DomainException : ExceptionBase
    {
        protected DomainException(string message) : base(message) { }
    }
}