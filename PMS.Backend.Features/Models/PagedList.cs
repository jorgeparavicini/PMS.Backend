using Microsoft.EntityFrameworkCore;

namespace PMS.Backend.Features.Models;

/// <summary>
/// A list that contains paging metadata.
/// </summary>
/// <typeparam name="T">The underlying type of the list.</typeparam>
public class PagedList<T> : List<T>
{
    /// <summary>
    /// The current page for the query. The first page is <c>1</c>.
    /// </summary>
    public int CurrentPage { get; init; }

    /// <summary>
    /// The total number of pages based on the <see cref="PageSize"/>.
    /// </summary>
    public int TotalPages { get; init; }

    /// <summary>
    /// The size of a single page.
    /// </summary>
    public int PageSize { get; init; }

    /// <summary>
    /// The total number of items of the queryable.
    /// </summary>
    public int TotalCount { get; init; }


    /// <summary>
    /// Is there a previous page.
    /// </summary>
    public bool HasPrevious => CurrentPage > 1;

    /// <summary>
    /// Is there a next page.
    /// </summary>
    public bool HasNext => CurrentPage < TotalPages;

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
    /// </summary>
    /// <param name="items">The items for the list.</param>
    /// <param name="count">The number of items in the list.</param>
    /// <param name="pageNumber">Current page number.</param>
    /// <param name="pageSize">The size of a page.</param>
    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);
    }
}

/// <summary>
/// Extension Class to facilitate the creation of <see cref="PagedList{T}"/> instances.
/// </summary>
public static class PagedListExtension
{
    /// <summary>
    /// Converts a <see cref="IQueryable"/> to a <see cref="PagedList{T}"/>.
    /// </summary>
    /// <param name="source">The queryable to convert.</param>
    /// <param name="pageNumber">The current page number to get. The first page is <c>1</c>.</param>
    /// <param name="pageSize">The number of items in a page.</param>
    /// <typeparam name="T">The type of the underlying list.</typeparam>
    /// <returns>A new instance of <see cref="PagedList{T}"/>.</returns>
    public static async Task<PagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> source,
        int pageNumber,
        int pageSize)
    {
        var count = source.Count();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}
