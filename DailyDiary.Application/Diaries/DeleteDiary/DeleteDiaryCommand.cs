using MediatR;

namespace DailyDiary.Application.Diaries.DeleteDiary;

public record DeleteDiaryCommand(Guid Id) : IRequest<Unit>;