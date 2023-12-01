using DailyDiary.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace DailyDiary.Infrastructure.Persistence.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
        => (_context) = (context);
    public async Task<User> GetByIdAsync(Guid id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        return user;
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<User>? GetByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.Value == email);
        return user;
    }
}