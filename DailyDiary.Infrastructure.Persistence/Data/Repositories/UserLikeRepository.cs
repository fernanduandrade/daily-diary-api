using DailyDiary.Domain.UserLikes;
using Microsoft.EntityFrameworkCore;

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

    public async Task<UserLike> GetByUserAndDiaryId(Guid userId, Guid diaryId)
    {
        var entity = await _context.UserLikes
            .FirstOrDefaultAsync(x => x.UserId == userId && x.DiaryId == diaryId);
        return entity;
    }

    public void Delete(UserLike userLike)
    {
        _context.UserLikes.Remove(userLike);
    }
}