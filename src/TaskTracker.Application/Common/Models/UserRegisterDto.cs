
using TaskTracker.Domain.Users;

namespace TaskTracker.Application.Common.Models;

public class UserRegisterDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string ImagePath { get; set; }
    public string Password { get; set; }
}
