using DailyDiary.Domain.Common;

namespace DailyDiary.Domain.DiaryLikes;

public interface IDiaryLikeRepository : IRepository<DiaryLike>
{
    Task Create(DiaryLike like);
    Task<DiaryLike> GetByDiaryId(Guid diaryId);
    Task<DiaryLike> GetByUserAndDiaryId(Guid userId, Guid diaryId);
}