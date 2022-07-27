namespace PMS.Backend.Features.Models;

public class ListQueryParams
{
    private const int MaxPageSize = 50;

    public int Page { get; set; } = 1;

    private int _pageSize = 0;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = Math.Min(value, MaxPageSize);
    }
}
