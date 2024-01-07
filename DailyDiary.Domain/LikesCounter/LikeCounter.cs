using DailyDiary.Domain.Common;
using DailyDiary.Domain.DiaryLikes;
namespace DailyDiary.Domain.LikesCounter;

public class LikeCounter : Entity ,IAggregateRoot
{
    public virtual DiaryLike DiaryLike { get; }
    public Guid DiaryLikeId { get; private set; }
    public int Counter { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public static LikeCounter Create(Guid likeId)
    {
        LikeCounter likeCounter = new()
        {
            Id = Guid.NewGuid(),
            DiaryLikeId = likeId,
            Counter = 0,
        };

        return likeCounter;
    }

    public void Increment()
    {
        Counter += 1;
    }
    
    public void Decrement()
    {
        Counter -= 1;
    }
}