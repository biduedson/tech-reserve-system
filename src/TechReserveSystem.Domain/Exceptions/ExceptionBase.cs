namespace TechReserveSystem.Domain.Exceptions
{
    public abstract class ExceptionBase : Exception
    {
        protected ExceptionBase(string message) : base(message) { }
    }

}