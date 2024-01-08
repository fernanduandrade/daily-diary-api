using DailyDiary.Domain.Common;

namespace DailyDiary.Domain.UserLikes;

public interface IUserLikeRepository : IRepository<UserLike>
{
    void Add(UserLike userLike);
    Task<UserLike> GetByUserAndDiaryId(Guid userId, Guid diaryId);
    void Delete(UserLike userLike);
}