using DailyDiary.Domain.Common;
using DailyDiary.Domain.Diaries;

namespace DailyDiary.Domain.LikesCounter;

public class LikeCounter : Entity ,IAggregateRoot
{
    public int Counter { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public virtual Diary Diary { get; private set; }
    public Guid DiaryId { get; private set; }

    public static LikeCounter Create(Guid diaryId)
    {
        LikeCounter likeCounter = new()
        {
            Id = Guid.NewGuid(),
            DiaryId = diaryId,
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