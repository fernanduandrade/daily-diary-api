using DailyDiary.Domain.Common;

namespace DailyDiary.Domain.UserLikes;

public interface IUserLikeRepository : IRepository<UserLike>
{
    void Add(UserLike userLike);
}