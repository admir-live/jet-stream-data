namespace JetStreamData.Kernel.Domain.Exceptions;

[Serializable]
public abstract class BadRequestException(string message) : DomainException(message);
