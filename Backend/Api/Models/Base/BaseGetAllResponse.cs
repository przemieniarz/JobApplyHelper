using static Backend.Api.Models.OfferPlacement;

namespace Backend.Api.Models.Base;

public class BaseGetAllResponse<T>
{
    public List<T> Items { get; set; } = [];
    public int TotalRows { get; set; }
}
