using Microsoft.AspNetCore.Identity;

namespace TaskTracker.Infastructore.Identity;

public class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole() : base()
    {
    }

    public ApplicationRole(string roleName) : base(roleName)
    {
    }
}
