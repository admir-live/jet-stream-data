using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;

namespace JetStreamData.Kernel.Dispatcher;

public abstract class BaseHandlerWithNoResponse<TRequest>(
    IMapper mapper,
    IDispatcher dispatcher) : IRequestHandler<TRequest, Result>
    where TRequest : IRequest<Result>
{
    protected readonly IDispatcher Dispatcher = dispatcher;
    protected readonly IMapper Mapper = mapper;

    public abstract Task<Result> Handle(
        TRequest request,
        CancellationToken cancellationToken);
}
