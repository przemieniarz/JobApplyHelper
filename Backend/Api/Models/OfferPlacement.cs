using Backend.Api.Models.Base;
using Backend.Enums;

namespace Backend.Api.Models;

public class OfferPlacement
{
    public class Base
    {
        public string Name { get; set; } = string.Empty;
        public Region Region { get; set; }
    }

    public class BaseExtended : Base
    {
        public Guid Id { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
    }

    public class CreateRequest : Base
    {
    }

    public class UpdateRequest : BaseExtended
    {
    }

    public class GetByIdRequest : BaseGetByIdRequest
    {
    }

    public class GetAllRequest : BaseGetAllRequest
    {
        public string? NameInclude { get; set; }
        public Region? RegionInclude { get; set; }
    }

    public class CreateResponse : BaseExtended
    {
    }

    public class UpdateResponse : BaseExtended
    {
    }

    public class GetByIdResponse : BaseExtended
    {
    }

    public class GetAllResponse : BaseGetAllResponse<BaseExtended>
    {
    }
}
