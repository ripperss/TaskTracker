using TaskTracker.Domain.Users;

namespace TaskTracker.API.Models;

public class ManagerRegisterDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? TeamName { get; set; }
    public string? Password { get; set; }
    public string? TeamPassword { get; set; }
}
