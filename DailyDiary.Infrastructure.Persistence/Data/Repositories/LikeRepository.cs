using DailyDiary.Domain.Likes;

namespace DailyDiary.Infrastructure.Persistence.Data.Repositories;

public class LikeRepository : ILikeRepository
{
    private readonly AppDbContext _context;

    public LikeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Create(Like like)
    {
        await _context.Likes.AddAsync(like);
    }
}