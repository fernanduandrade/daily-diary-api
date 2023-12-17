using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Diaries.Dto;
using DailyDiary.Domain.Common;
using MediatR;
using OneOf;

namespace DailyDiary.Application.Diaries.GetDiaryById;

public sealed record GetDiaryByIdQuery(Guid Id) : IRequest<OneOf<ApiResponse<DiaryDto>, Error>>;