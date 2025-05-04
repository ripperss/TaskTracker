using Microsoft.AspNetCore.Identity;

namespace TaskTracker.Infastructore.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }

} 