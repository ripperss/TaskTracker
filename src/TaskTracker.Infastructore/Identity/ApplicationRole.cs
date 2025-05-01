using Microsoft.AspNetCore.Identity;

namespace TaskTracker.Infastructore.Identity;

public class ApplicationRole : IdentityRole<int>
{
    public ApplicationRole() : base()
    {
    }

    public ApplicationRole(string roleName) : base(roleName)
    {
    }
}
