using System.Collections.Immutable;
using DailyDiary.Domain.Diaries;
using Microsoft.EntityFrameworkCore;

namespace DailyDiary.Infrastructure.Persistence.Data.Repositories;

public class DiaryRepository : IDiaryRepository
{
    private readonly AppDbContext _context;

    public DiaryRepository(AppDbContext context)
        => (_context) = (context);
    public async Task<bool> HasPublish(Guid userId)
    {
        var compareDate = DateTime.UtcNow.Date;
        var hasPublish = await _context.Diaries
            .FirstOrDefaultAsync(x => x.CreatedAt.Date == compareDate && x.UserId == userId);
        return hasPublish is not null;
    }

    public async Task AddAsync(Diary diary)
     => await _context.Diaries.AddAsync(diary);

    public async Task<List<Diary>> GetAllByUserId(Guid userId)
    {
        var diaries = await _context.Diaries
            .Where(x => x.UserId == userId)
            .AsNoTracking()
            .ToListAsync();

        return diaries;
    }

    public void Update(Diary diary) => _context.Diaries.Update(diary);

    public async Task<Diary> GetById(Guid id)
    {
        var diary = await _context.Diaries
            .AsNoTracking().
            SingleOrDefaultAsync(x => x.Id == id);
        return diary;
    }

    public async Task Delete(Guid id)
    {
        var diary = await _context.Diaries.FirstOrDefaultAsync(x => x.Id == id);
        _context.Diaries.Remove(diary);
    }

    public async Task<List<Diary>> GetPublics()
    {
        var publicDiaries = await _context.Diaries.Where(x => x.IsPublic)
            .AsNoTracking()
            .ToListAsync();

        return publicDiaries;
    }
}