namespace DailyDiary.Domain.Diaries;

public interface IDiaryRepository
{
    Task<bool> HasPublish(Guid userId);
    Task<Diary> AddAsync(Diary diary);
}