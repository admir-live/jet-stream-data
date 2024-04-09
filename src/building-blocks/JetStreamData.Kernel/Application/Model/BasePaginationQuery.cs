using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace JetStreamData.Kernel.Application.Model;

public abstract class BasePaginationQuery<TEntity> : IRequest<TEntity>
{
    private string _direction;
    private int _pageSize;

    [SwaggerParameter("The page size of response. Default value id 10.", Required = false)]
    public int PageSize
    {
        get => _pageSize == default ? 10 : _pageSize;
        set => _pageSize = value;
    }

    [SwaggerParameter("The page number of response. Started from index 1.", Required = true)]
    public virtual int Page { get; set; }

    [SwaggerParameter("The sort direction of response.", Required = false)]
    public string OrderBy { get; set; }

    [SwaggerParameter("The sort direction of response like asc or desc", Required = false)]
    public string Direction
    {
        get => _direction != "desc" && _direction != "asc" ? "asc" : _direction;
        set => _direction = value;
    }
}
