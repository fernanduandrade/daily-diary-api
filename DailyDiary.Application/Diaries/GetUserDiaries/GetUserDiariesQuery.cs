using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Diaries.Dto;
using MediatR;

namespace DailyDiary.Application.Diaries.GetUserDiaries;

public sealed record GetUserDiariesQuery(Guid userId) : IRequest<ApiResponse<List<DiaryDto>>>;