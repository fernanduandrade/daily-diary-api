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

    public async Task<Diary> AddAsync(Diary diary)
    {
        _context.Diaries.Add(diary);
        await _context.SaveChangesAsync();
        return diary;
    }

    public async Task<List<Diary>> GetAllByUserId(Guid userId)
    {
        var diaries = await _context.Diaries
            .Where(x => x.UserId == userId)
            .AsNoTracking()
            .ToListAsync();

        return diaries;
    }

    public async Task<Diary> Update(Diary diary)
    {
        _context.Diaries.Update(diary);
        await _context.SaveChangesAsync();
        return diary;
    }

    public async Task<Diary> GetById(Guid id)
    {
        var diary = await _context.Diaries
            .AsNoTracking().
            SingleOrDefaultAsync(x => x.Id == id);
        return diary;
    }

    public void Delete(Guid id)
    {
        var diary = _context.Diaries.FirstOrDefault(x => x.Id == id);
        _context.Diaries.Remove(diary);
        _context.SaveChanges();
    }
}