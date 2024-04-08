namespace JetStreamData.Kernel.Domain.Exceptions;

public sealed class ConfigurationMissingException(string key) : Exception($"Configuration missing for key: {key}")
{
    public string Key { get; } = key;
}
