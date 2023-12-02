using DailyDiary.Domain.Common;

namespace DailyDiary.Domain.Diaries;

public interface IDiaryRepository : IRepository<Diary>
{
    Task<bool> HasPublish(Guid userId);
    Task AddAsync(Diary diary);
    Task<List<Diary>> GetAllByUserId(Guid userId);
    void Update(Diary diary);
    Task<Diary> GetById(Guid id);
    Task Delete(Guid diaryId);
}