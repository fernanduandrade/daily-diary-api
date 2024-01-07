using DailyDiary.Domain.Common;

namespace DailyDiary.Domain.DiaryLikes.Events;

public sealed record CreateDiaryLikesEvent(Guid diaryId) : IDomainEvent { }