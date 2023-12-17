using DailyDiary.Application.Common.Interfaces;
using DailyDiary.Domain.Diaries;
using MediatR;

namespace DailyDiary.Application.Diaries.DeleteDiary;

public class DeleteDiaryCommandHandler : IRequestHandler<DeleteDiaryCommand, Unit>
{
    private readonly IDiaryRepository _diaryRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteDiaryCommandHandler(IDiaryRepository diaryRepository, IUnitOfWork unitOfWork)
        => (_diaryRepository, _unitOfWork) = (diaryRepository, unitOfWork);
    public async Task<Unit> Handle(DeleteDiaryCommand request, CancellationToken cancellationToken)
    {
        _diaryRepository.Delete(request.Id);
        await _unitOfWork.Commit(cancellationToken);
        return Unit.Value;
    }
}