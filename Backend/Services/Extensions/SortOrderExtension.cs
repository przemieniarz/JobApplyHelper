using Backend.Enums;

namespace Backend.Services.Extensions;

public static class SortOrderExtensions
{
    public static string ToDirectionString(this SortOrder? sortOrder)
    {
        return sortOrder switch
        {
            SortOrder.Desc => "desc",
            _ => "asc"
        };
    }
}
