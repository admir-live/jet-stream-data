using AutoMapper;
using MediatR;

namespace JetStreamData.Kernel.Dispatcher;

public abstract class BaseEventHandler<TEvent>(
    IMapper mapper,
    IDispatcher dispatcher) : INotificationHandler<TEvent>
    where TEvent : INotification
{
    protected readonly IDispatcher Dispatcher = dispatcher;
    protected readonly IMapper Mapper = mapper;

    public abstract Task Handle(
        TEvent notification,
        CancellationToken cancellationToken);
}
