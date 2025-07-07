using Backend.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Db.Models;

public class AppUser : IdentityUser
{
    public string Nickname { get; set; } = string.Empty;
}
