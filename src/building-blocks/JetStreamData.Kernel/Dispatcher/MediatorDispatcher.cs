using MediatR;

namespace JetStreamData.Kernel.Dispatcher;

public sealed class MediatorDispatcher(IMediator mediator) : IDispatcher
{
    public Task<TResult> DispatchAsync<TResult>(
        IRequest<TResult> request,
        CancellationToken cancellationToken = default)
    {
        return mediator.Send(request, cancellationToken);
    }

    public Task DispatchAsync(
        IRequest request,
        CancellationToken cancellationToken = default)
    {
        return mediator.Send(request, cancellationToken);
    }

    public Task DispatchAsync(
        INotification @event,
        CancellationToken cancellationToken = default)
    {
        return mediator.Publish(@event, cancellationToken);
    }
}
