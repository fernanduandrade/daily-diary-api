using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Diaries.DTO;
using MediatR;

namespace DailyDiary.Application.Diaries.GetUserDairies;

public sealed record GetUserDiariesQuery(Guid userId) : IRequest<ApiResponse<List<DiaryDto>>>;