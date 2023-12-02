using DailyDiary.Domain.Common;

namespace DailyDiary.Domain.Users;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByIdAsync(Guid id);
    Task AddAsync(User user);
    Task<User>? GetByEmail(string email);
}