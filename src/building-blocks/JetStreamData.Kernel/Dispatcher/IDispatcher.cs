using MediatR;

namespace JetStreamData.Kernel.Dispatcher;

public interface IDispatcher
{
    Task<TResult> DispatchAsync<TResult>(
        IRequest<TResult> request,
        CancellationToken cancellationToken = default);
    Task DispatchAsync(
        IRequest request,
        CancellationToken cancellationToken = default);
    Task DispatchAsync(
        INotification @event,
        CancellationToken cancellationToken = default);
}
