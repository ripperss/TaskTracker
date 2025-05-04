using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Users;

namespace TaskTracker.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
    Task<IEnumerable<User>> GetAllAsync();
    Task DeleteByIdAsync(Guid id);
    Task<User> GetByIdentityIdAsync(string id);
    Task<User> CreateUserAsync(User user);
}
