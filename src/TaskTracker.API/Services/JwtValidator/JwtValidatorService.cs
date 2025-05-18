using System.Security.Claims;

namespace TaskTracker.API.Services.UserServices;

public class JwtValidatorService : IJwtValidatorService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public JwtValidatorService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string GetTeamId()
    {
        var teamId = _contextAccessor.HttpContext?.User.FindFirstValue("TeamId");
        if(teamId == "")
        {
            throw new Exception("Вы не состоите в команде");
        }

        return teamId!;
    }

    public string GetUserIdentityId()
    {
        var identityId = _contextAccessor
            .HttpContext
            ?.User
            .FindFirstValue(ClaimTypes.NameIdentifier);

        return identityId!;
    }
}
