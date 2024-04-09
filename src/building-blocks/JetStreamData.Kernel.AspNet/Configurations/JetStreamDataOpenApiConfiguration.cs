namespace JetStreamData.Kernel.AspNet.Configurations;

public sealed class JetStreamDataOpenApiConfiguration
{
    public string ApplicationName { get; set; }
    public bool AddSwagger { get; set; }

    public JetStreamDataOpenApiConfiguration UseSwagger(string applicationName)
    {
        ApplicationName = applicationName;
        AddSwagger = true;
        return this;
    }
}
