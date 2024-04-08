using MediatR;
using Microsoft.Extensions.Logging;

namespace JetStreamData.Kernel.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var name = typeof(TRequest).Name;
        logger.LogInformation("----- Handling command {CommandName})", name);
        var response = await next();
        logger.LogInformation("----- Command {CommandName} handled", name);

        return response;
    }
}
