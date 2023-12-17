using MediatR;

namespace DailyDiary.Application.Diaries.DeleteDiary;

public sealed record DeleteDiaryCommand(Guid Id) : IRequest<Unit>;