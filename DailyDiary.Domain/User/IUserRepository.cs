namespace DailyDiary.Domain.User;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
    Task<User> AddAsync(User user);
    Task<bool> VerifyEmail(string email);
    Task<User>? GetByEmail(string email);
}