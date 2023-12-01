namespace DailyDiary.Domain.Users;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
    Task AddAsync(User user);
    Task<User>? GetByEmail(string email);
}