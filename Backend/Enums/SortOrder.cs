using System.Runtime.Serialization;

namespace Backend.Enums;

public enum SortOrder
{
    [EnumMember(Value = "Asc")]
    Asc = 0,
    [EnumMember(Value = "Desc")]
    Desc = 1,
}
