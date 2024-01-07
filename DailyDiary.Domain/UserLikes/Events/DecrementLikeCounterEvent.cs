using DailyDiary.Domain.Common;

namespace DailyDiary.Domain.UserLikes.Events;

public sealed record DecrementLikeCounterEvent(Guid diaryLikeId) : IDomainEvent { }