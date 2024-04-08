using MediatR;
using Microsoft.Extensions.Logging;

namespace JetStreamData.Kernel.Behaviors;

public class TransactionBehaviour<TRequest, TResponse>(ILogger<TransactionBehaviour<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger = logger ?? throw new ArgumentException(nameof(ILogger));

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        return await next();
    }
}
