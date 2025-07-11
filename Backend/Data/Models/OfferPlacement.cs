using Backend.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Data.Models;

public class OfferPlacement : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public Region Region { get; set; }

    [NotMapped]
    public string NormalizedName => Name.ToLowerInvariant();

}
