using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace JetStreamData.Kernel.AspNet.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, string serviceName)
    {
        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
            options.CustomSchemaIds(type => type.FullName!.Replace("+", "."));
            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            options.SwaggerGeneratorOptions.SwaggerDocs.Add(
                "v1",
                new OpenApiInfo
                {
                    Version = "v1.0.0",
                    Title = serviceName,
                    Description = "This is the REST API documentation for the Jet Stream Data service.",
                    License = new OpenApiLicense { Name = "MIT License", Url = new Uri("https://github.com/admir-live/jet-stream-data/blob/develop/LICENSE") }
                });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerWithUi(
        this IApplicationBuilder app,
        IApiVersionDescriptionProvider versionDescriptionProvider)
    {
        app.UseSwagger().UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Jet Stream Data Rest API v1.0.0");
        });

        return app;
    }
}
