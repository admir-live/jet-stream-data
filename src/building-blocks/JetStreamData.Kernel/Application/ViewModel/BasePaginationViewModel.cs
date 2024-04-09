namespace JetStreamData.Kernel.Application.ViewModel;

public class BasePaginationViewModel<TViewModel>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public IReadOnlyList<TViewModel> Items { get; set; }
}
