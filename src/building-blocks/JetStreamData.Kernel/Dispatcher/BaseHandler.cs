using AutoMapper;
using MediatR;

namespace JetStreamData.Kernel.Dispatcher;

public abstract class BaseHandler<TRequest, TResponse>(
    IMapper mapper,
    IDispatcher dispatcher) : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected readonly IDispatcher Dispatcher = dispatcher;
    protected readonly IMapper Mapper = mapper;

    public abstract Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken);
}
