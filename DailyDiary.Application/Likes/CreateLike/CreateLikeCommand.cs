using MediatR;

namespace DailyDiary.Application.Likes.CreateLike;

public sealed record CreateLikeCommand(Guid UserId, Guid DiaryId) : IRequest<Unit>;