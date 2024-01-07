using DailyDiary.Domain.UserLikes;

namespace DailyDiary.Infrastructure.Persistence.Data.Repositories;

public class UserLikeRepository : IUserLikeRepository
{
    private readonly AppDbContext _context;

    public UserLikeRepository(AppDbContext context)
    {
        _context = context;
    }
    public void Add(UserLike userLike)
    {
        _context.UserLikes.Add(userLike);
    }
}