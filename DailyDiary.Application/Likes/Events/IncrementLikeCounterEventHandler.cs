using DailyDiary.Application.Common.Interfaces;
using DailyDiary.Domain.LikesCounter;
using DailyDiary.Domain.UserLikes.Events;
using MediatR;

namespace DailyDiary.Application.Likes.Events;

public sealed class IncrementLikeCounterEventHandler : INotificationHandler<IncrementLikeCounterEvent>
{
    private readonly ILikeCounterRepository _likeCounterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public IncrementLikeCounterEventHandler(ILikeCounterRepository likeCounterRepository, IUnitOfWork unitOfWork)
    {
        _likeCounterRepository = likeCounterRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(IncrementLikeCounterEvent notification, CancellationToken cancellationToken)
    {
        _likeCounterRepository.Increment(notification.diaryLikeEvent);
        await _unitOfWork.Commit(cancellationToken);
    }
}