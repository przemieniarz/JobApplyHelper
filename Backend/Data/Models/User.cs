using Backend.Data.Models;

namespace Backend.Db.Models;

public class User : BaseModel
{
    public string Nickname = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
