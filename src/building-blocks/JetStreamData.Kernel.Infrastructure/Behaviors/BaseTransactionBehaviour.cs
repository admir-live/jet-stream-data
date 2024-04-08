using JetStreamData.Kernel.Dispatcher;
using JetStreamData.Kernel.Domain.Interfaces;
using JetStreamData.Kernel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace JetStreamData.Kernel.Infrastructure.Behaviors;

public abstract class BaseTransactionBehaviour<TRequest, TResponse>(
    IServiceProvider serviceProvider,
    JetStreamDataBaseDbContext dbContext,
    ILogger<BaseTransactionBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly JetStreamDataBaseDbContext _dbContext = dbContext ?? throw new ArgumentException(nameof(JetStreamDataBaseDbContext));
    private readonly ILogger<BaseTransactionBehaviour<TRequest, TResponse>> _logger = logger ?? throw new ArgumentException(nameof(ILogger));

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var response = default(TResponse);
        var typeName = typeof(TRequest).FullName;

        try
        {
            if (_dbContext.HasActiveTransaction)
            {
                return await next();
            }

            var strategy = _dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
                response = await ExecuteTransactionAsync(request, next, typeName));
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);
            _logger.LogInformation($"Rollback transaction executed {typeName}");
            throw;
        }
    }

    private async Task<TResponse> ExecuteTransactionAsync(TRequest request, RequestHandlerDelegate<TResponse> next,
        string typeName)
    {
        await using var transaction = await _dbContext.StartNewTransactionAsync();

        _logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName} ({@Command})",
            transaction.TransactionId, typeName, request);
        var response = await next();
        _logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId,
            typeName);

        var domainEvents = _dbContext.ChangeTracker
            .Entries<IHasGlobalEvents>()
            .Where(x => x.Entity.GlobalEvents != null && x.Entity.GlobalEvents.Any())
            .SelectMany(x => x.Entity.GlobalEvents)
            .ToList();

        try
        {
            await _dbContext.CommitCurrentTransactionAsync(transaction);
            await RaiseGlobalHandlerAsync(domainEvents);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "ERROR Handling transaction");
        }

        return response;
    }

    private async Task RaiseGlobalHandlerAsync(IEnumerable<IGlobalEvent> events)
    {
        var globalEvents = events as IGlobalEvent[] ?? events.ToArray();
        if (globalEvents.Length != 0)
        {
            using var scope = serviceProvider.CreateScope();
            var logger = scope.ServiceProvider.GetService<ILogger<IEnumerable<IGlobalEvent>>>();
            logger!.LogInformation($"----- Global Events. Total: {globalEvents.Length} events for processing.");
            var dispatcher = scope.ServiceProvider.GetService<IDispatcher>();
            foreach (var globalEvent in globalEvents)
            {
                try
                {
                    await dispatcher!.DispatchAsync(globalEvent);
                    logger!.LogInformation(
                        $"----- Global Events: Event {globalEvent.GetType().FullName} has been successfully done.");
                }
                catch (Exception e)
                {
                    logger!.LogError(e,
                        $"----- Global Events: Error occured during processing global event: {globalEvent.GetType().FullName}.");
                }
            }
        }
    }
}
