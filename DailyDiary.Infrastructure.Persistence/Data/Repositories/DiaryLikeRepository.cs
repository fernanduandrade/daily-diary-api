using DailyDiary.Domain.DiaryLikes;
using Microsoft.EntityFrameworkCore;

namespace DailyDiary.Infrastructure.Persistence.Data.Repositories;

public class DiaryLikeRepository : IDiaryLikeRepository
{
    private readonly AppDbContext _context;

    public DiaryLikeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Create(DiaryLike like)
    {
        await _context.DiaryLikes.AddAsync(like);
    }

    public async Task<DiaryLike> GetByDiaryId(Guid diaryId)
    {
        var entity = await _context.DiaryLikes.FirstOrDefaultAsync(x => x.DiaryId == diaryId);
        return entity;
    }

    public async Task<DiaryLike> GetByUserAndDiaryId(Guid userId, Guid diaryId)
    {
        var entity = await _context.DiaryLikes.FirstOrDefaultAsync(x => x.DiaryId == diaryId);
        return entity;
    }
}