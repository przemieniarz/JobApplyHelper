using System.Runtime.Serialization;

namespace Backend.Enums;

public enum Region
{
    [EnumMember(Value = "Other")]
    Other = 0,
    [EnumMember(Value = "Poland")]
    Poland = 1,
}
