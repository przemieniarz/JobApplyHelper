using Backend.Enums;

namespace Backend.Data.Models;

public class OfferPlacement : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public Region Region { get; set; }
}
