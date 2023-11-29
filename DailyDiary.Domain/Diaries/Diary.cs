using DailyDiary.Domain.Common;
using DailyDiary.Domain.Users;

namespace DailyDiary.Domain.Diaries;

public class Diary : Entity
{
    public string? Title { get; private set; }
    public string? Text { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public bool IsPublic { get; private set; }
    public string? Mood { get; private set; }
    public virtual User User { get; private set; }
    public Guid UserId { get; private set ;}
    public static Diary Create(string title, string text, string mood, Guid userId, bool isPublic = false)
    {
        var diary = new Diary()
        {
            Title = title,
            Text = text,
            Mood = mood,
            IsPublic = isPublic,
            UserId = userId
        };

        return diary;
    }
}

public class DiaryErrors
{
    public static readonly Error InvalidDate = new("Invalid Date", "Can not create two diaries in the same day");
}