using MediatR;

namespace DailyDiary.Application.Likes.CreateFavorite;

public sealed record CreateFavoriteCommand(Guid UserId, Guid DiaryId) : IRequest<Unit>;