namespace TaskTracker.API.Services.UserServices;

public interface IJwtValidatorService
{
    public string GetUserIdentityId();
    public string GetTeamId();
}
