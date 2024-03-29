using System.ComponentModel.DataAnnotations.Schema;
using DailyDiary.Domain.Common;
using DailyDiary.Domain.LikesCounter;
using DailyDiary.Domain.LikesCounter.Events;
using DailyDiary.Domain.UserLikes;
using DailyDiary.Domain.Users;

namespace DailyDiary.Domain.Diaries;

public class Diary : Entity, IAggregateRoot
{
    public string? Title { get; private set; }
    public string? Text { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public bool IsPublic { get; private set; }
    public string? Mood { get; private set; }
    public User User { get; }
    public Guid UserId { get; private set ;}
    
    public LikeCounter LikeCounter { get; }
    public UserLike UserLike { get; }

    [NotMapped] public bool IsFavorited => UserLike != null;
    
    public static Diary Create(string title, string text, string mood, Guid userId, bool isPublic = false)
    {
        var diary = new Diary()
        {
            Id = new Guid(),
            Title = title,
            Text = text,
            Mood = mood,
            IsPublic = isPublic,
            UserId = userId
        };

        return diary;
    }
    
    public void CreateDiaryLikeCounter(Guid diaryId)
    {
        Raise(new CreateDiaryLikesEvent(diaryId));
    }
}

public class DiaryErrors
{
    public static readonly Error InvalidDate = new("Invalid Date", "Can not create two diaries in the same day");
    public static readonly Error NotFound = new("Not Found", "Diary not found");
}