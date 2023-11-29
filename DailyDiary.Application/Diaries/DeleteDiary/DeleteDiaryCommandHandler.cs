using DailyDiary.Domain.Diaries;
using MediatR;

namespace DailyDiary.Application.Diaries.DeleteDiary;

public class DeleteDiaryCommandHandler : IRequestHandler<DeleteDiaryCommand, Unit>
{
    private readonly IDiaryRepository _diaryRepository;

    public DeleteDiaryCommandHandler(IDiaryRepository diaryRepository)
        => (_diaryRepository) = (diaryRepository);
    public async Task<Unit> Handle(DeleteDiaryCommand request, CancellationToken cancellationToken)
    {
        //get by id and allow change the tracks of the entity since is going to be deleted any way
        var diary = await _diaryRepository.GetById(request.Id);
        _diaryRepository.Delete(diary);
        return Unit.Value;
    }
}