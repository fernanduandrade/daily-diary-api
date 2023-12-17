using DailyDiary.Domain.Common;
using DailyDiary.Domain.Diaries;
using DailyDiary.Domain.Users;

namespace DailyDiary.Domain.Likes;

public class Like : Entity ,IAggregateRoot
{
    public virtual User User { get; }
    public Guid UserId { get; private set; }
    public virtual Diary Diary { get; }
    public Guid DiaryId { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public static Like Create(Guid useId, Guid diaryId)
    {
        Like like = new()
        {
            Id = Guid.NewGuid(),
            UserId = useId,
            DiaryId = diaryId
        };

        return like;
    }
}