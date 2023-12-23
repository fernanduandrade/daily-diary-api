using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Diaries.Dto;
using DailyDiary.Domain.Common;
using MediatR;
using OneOf;

namespace DailyDiary.Application.Diaries.SearchPublicDiaries;

public sealed record SearchPublicDiariesQuery(Guid userId, string Search) : IRequest<OneOf<ApiResponse<List<DiaryDto>>, Error>>;