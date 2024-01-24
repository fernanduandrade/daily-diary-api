using DailyDiary.Application.Common.Interfaces;
using DailyDiary.Domain.LikesCounter;
using DailyDiary.Domain.LikesCounter.Events;
using MediatR;

namespace DailyDiary.Application.Diaries.Events;

public sealed class CreateDiaryLikesEventHandler : INotificationHandler<CreateDiaryLikesEvent>
{
    private readonly ILikeCounterRepository _likeCounterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDiaryLikesEventHandler(IUnitOfWork unitOfWork,
                                                ILikeCounterRepository likeCounterRepository)
    {
        _unitOfWork = unitOfWork;
        _likeCounterRepository = likeCounterRepository;
    }
    public async Task Handle(CreateDiaryLikesEvent notification, CancellationToken cancellationToken)
    {
        var diaryLikeCounter = LikeCounter.Create(notification.diaryId);
        _likeCounterRepository.Create(diaryLikeCounter);
        await _unitOfWork.Commit(cancellationToken);
    }
}