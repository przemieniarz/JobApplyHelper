using System.ComponentModel.DataAnnotations;

namespace Backend.Data.Models;

public class BaseModel
{
    [Key]
    public Guid Id { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset ModifiedDate { get; set; }
}
