using TransPorter.Shared.Enums;

namespace TransPorter.Shared.DTOs.Request;

public abstract class PagedRequest
{
    public string? SearchString { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public string? SortLabel { get; set; }
    public SortDirection SortDirection { get; set; }
}
