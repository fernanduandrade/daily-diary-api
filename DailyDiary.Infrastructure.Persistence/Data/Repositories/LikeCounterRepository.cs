using DailyDiary.Domain.LikesCounter;
using Microsoft.EntityFrameworkCore;

namespace DailyDiary.Infrastructure.Persistence.Data.Repositories;

public class LikeCounterRepository : ILikeCounterRepository
{
    private readonly AppDbContext _context;

    public LikeCounterRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public void Create(LikeCounter likeCounter)
    {
        _context.Add(likeCounter);
    }

    public void Increment(Guid diaryId)
    {
        var entity = _context.LikesCounter.FirstOrDefault(x => x.DiaryId == diaryId);
        entity?.Increment();
    }

    public void Decrement(Guid diaryId)
    {
        var entity = _context.LikesCounter.FirstOrDefault(x => x.DiaryId == diaryId);
        entity?.Decrement();
    }

    public async Task<int> GetDiaryLikes(Guid diaryId)
    {
        var likesCount = await _context.LikesCounter.FirstOrDefaultAsync(x => x.DiaryId == diaryId);

        return likesCount.Counter;
    }

    public Task<bool> HasUserFavorite(Guid userId)
    {
        throw new NotImplementedException();
    }
}