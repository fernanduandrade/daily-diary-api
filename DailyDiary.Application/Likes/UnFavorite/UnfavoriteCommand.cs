using DailyDiary.Application.Common.Models;
using MediatR;

namespace DailyDiary.Application.Likes.UnFavorite;

public sealed record UnfavoriteCommand(Guid UserId, Guid DiaryId) : IRequest<ApiResponse<Unit>>;