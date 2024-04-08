using MediatR;

namespace JetStreamData.Kernel.Dispatcher;

public class NullDispatcher : IDispatcher
{
    public static NullDispatcher New => new();

    public Task<TResult> DispatchAsync<TResult>(
        IRequest<TResult> request,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(default(TResult));
    }

    public Task DispatchAsync(
        IRequest request,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task DispatchAsync(
        INotification @event,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
