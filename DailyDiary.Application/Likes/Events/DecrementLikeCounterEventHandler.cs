using DailyDiary.Application.Common.Interfaces;
using DailyDiary.Domain.LikesCounter;
using DailyDiary.Domain.UserLikes.Events;
using MediatR;

namespace DailyDiary.Application.Likes.Events;

public sealed class DecrementLikeCounterEventHandler : INotificationHandler<DecrementLikeCounterEvent>
{
    private readonly ILikeCounterRepository _likeCounterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DecrementLikeCounterEventHandler(ILikeCounterRepository likeCounterRepository, IUnitOfWork unitOfWork)
    {
        _likeCounterRepository = likeCounterRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(DecrementLikeCounterEvent notification, CancellationToken cancellationToken)
    {
        _likeCounterRepository.Decrement(notification.diaryLikeId);
        await _unitOfWork.Commit(cancellationToken);
    }
}