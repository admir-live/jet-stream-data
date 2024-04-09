using MediatR;

namespace JetStreamData.Kernel.Application.Model;

public abstract class QueryCommand
{
    public abstract class Query<TResponse> : IRequest<TResponse>
    {
    }

    public abstract class PaginationQuery<TResponse> : BasePaginationQuery<TResponse>
    {
    }
}
