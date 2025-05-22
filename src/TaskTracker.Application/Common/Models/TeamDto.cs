
namespace TaskTracker.Application.Common.Models;

public class TeamDto
{
    public Guid TeamID { get; set; }    
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid AdminId { get; set; }
    public List<UserDto> Members { get; set; }
}
