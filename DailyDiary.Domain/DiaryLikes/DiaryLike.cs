using DailyDiary.Domain.Common;
using DailyDiary.Domain.Diaries;

namespace DailyDiary.Domain.DiaryLikes;

public class DiaryLike : Entity ,IAggregateRoot
{
    public virtual Diary Diary { get; }
    public Guid DiaryId { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public static DiaryLike Create(Guid diaryId)
    {
        DiaryLike like = new()
        {
            Id = Guid.NewGuid(),
            DiaryId = diaryId
        };

        return like;
    }
}