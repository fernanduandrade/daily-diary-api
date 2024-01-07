using DailyDiary.Domain.Common;

namespace DailyDiary.Domain.UserLikes.Events;

public sealed record IncrementLikeCounterEvent(Guid diaryLikeEvent) : IDomainEvent {};