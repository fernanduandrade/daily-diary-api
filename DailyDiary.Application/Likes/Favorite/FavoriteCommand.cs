using MediatR;

namespace DailyDiary.Application.Likes.Favorite;

public sealed record FavoriteCommand(Guid UserId, Guid DiaryId) : IRequest<Unit>;