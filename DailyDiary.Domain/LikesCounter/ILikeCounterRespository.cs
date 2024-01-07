using DailyDiary.Domain.Common;

namespace DailyDiary.Domain.LikesCounter;

public interface ILikeCounterRepository : IRepository<LikeCounter>
{
    void Create(LikeCounter likeCounter);
    void Increment(Guid diaryLikeId);
    void Decrement(Guid diaryLikeId);
}