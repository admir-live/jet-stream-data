namespace JetStreamData.Kernel.Domain.Exceptions;

public class DomainException : Exception
{
    protected DomainException(string message)
        : base(message)
    {
    }

    public DomainException(
        string message,
        Exception innerException)
        : base(message, innerException)
    {
    }
}
