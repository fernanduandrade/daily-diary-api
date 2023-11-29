using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Diaries.DTO;
using DailyDiary.Domain.Common;
using MediatR;
using OneOf;

namespace DailyDiary.Application.Diaries.GetDiaryById;

public record GetDiaryByIdQuery(Guid Id) : IRequest<OneOf<ApiResponse<DiaryDto>, Error>>;