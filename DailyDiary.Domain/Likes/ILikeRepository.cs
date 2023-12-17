using DailyDiary.Domain.Common;

namespace DailyDiary.Domain.Likes;

public interface ILikeRepository : IRepository<Like>
{
    Task Create(Like like);
}