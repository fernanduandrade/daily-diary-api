using DailyDiary.Domain.Diaries;
using MediatR;

namespace DailyDiary.Application.Diaries.Commands;

public sealed record DeleteDiaryCommand(Guid Id) : IRequest<Unit>;

public class DeleteDiaryCommandHandler : IRequestHandler<DeleteDiaryCommand, Unit>
{
    private readonly IDiaryRepository _diaryRepository;
    public DeleteDiaryCommandHandler(IDiaryRepository diaryRepository)
        => (_diaryRepository) = (diaryRepository);
    public Task<Unit> Handle(DeleteDiaryCommand request, CancellationToken cancellationToken)
    {
        _diaryRepository.Delete(request.Id);
        return Task.FromResult(Unit.Value);
    }
}