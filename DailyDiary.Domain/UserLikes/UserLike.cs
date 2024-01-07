using DailyDiary.Domain.Common;
using DailyDiary.Domain.Diaries;
using DailyDiary.Domain.UserLikes.Events;
using DailyDiary.Domain.Users;

namespace DailyDiary.Domain.UserLikes;

public class UserLike : Entity, IAggregateRoot
{
    public virtual Diary Diary { get; private set; }
    public Guid DiaryId { get; private set; }
    public virtual User User { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public static UserLike Create(Guid userId, Guid diaryId)
    {
        UserLike userLike = new()
        {
            UserId = userId,
            DiaryId = diaryId
        };

        return userLike;
    }

    public void IncrementLikeEvent(Guid diaryLikeId)
    {
        Raise(new IncrementLikeCounterEvent(diaryLikeId));
    }
    
    public void DecrementLikeEvent(Guid diaryLikeId)
    {
        Raise(new DecrementLikeCounterEvent(diaryLikeId));
    }
}