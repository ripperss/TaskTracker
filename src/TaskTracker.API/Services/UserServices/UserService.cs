using System.Security.Claims;

namespace TaskTracker.API.Services.UserServices;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public UserService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string GetUserIdentityId()
    {
        var identityId = _contextAccessor
            .HttpContext
            ?.User
            .FindFirstValue(ClaimTypes.NameIdentifier);

        return identityId;
    }
}
