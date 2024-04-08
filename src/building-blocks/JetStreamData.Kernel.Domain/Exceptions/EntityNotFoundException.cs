namespace JetStreamData.Kernel.Domain.Exceptions;

[Serializable]
public class EntityNotFoundException(string message) : DomainException(message);
