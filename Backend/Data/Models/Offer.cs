using Backend.Enums;

namespace Backend.Data.Models;

public class Offer : BaseModel
{
    public string Position { get; set; }
    public string? Company { get; set; }
    public string Url { get; set; }
    public OfferStatus Status { get; set; }
    public string? Comment { get; set; }
    public string? OtherPlacement { get; set; }

    public Guid? PlacementId { get; set; }
    public OfferPlacement? Placement { get; set; }
    public Guid UserId { get; set; }
    public AppUser User { get; set; }
}
