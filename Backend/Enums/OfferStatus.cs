using System.Runtime.Serialization;

namespace Backend.Enums;

public enum OfferStatus
{
    [EnumMember(Value = "Applied")]
    Applied = 0,
    [EnumMember(Value = "Rejected")]
    Rejected = 1,
    [EnumMember(Value = "On Hold")]
    OnHold = 2,
}
