using DailyDiary.Domain.Common;

namespace DailyDiary.Domain.LikesCounter.Events;

public sealed record CreateDiaryLikesEvent(Guid diaryId) : IDomainEvent { }