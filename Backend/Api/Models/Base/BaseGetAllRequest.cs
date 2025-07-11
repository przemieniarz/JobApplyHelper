using Backend.Enums;

namespace Backend.Api.Models.Base;

public class BaseGetAllRequest
{
    public int? PageOffset { get; set; }
    public int? PageLimit { get; set; }
    public SortOrder? SortOrder { get; set; }
    public string? SortBy { get; set; }
}
