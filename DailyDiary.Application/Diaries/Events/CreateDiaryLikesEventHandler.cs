using DailyDiary.Application.Common.Interfaces;
using DailyDiary.Domain.DiaryLikes;
using DailyDiary.Domain.DiaryLikes.Events;
using DailyDiary.Domain.LikesCounter;
using MediatR;

namespace DailyDiary.Application.Diaries.Events;

public sealed class CreateDiaryLikesEventHandler : INotificationHandler<CreateDiaryLikesEvent>
{
    private readonly IDiaryLikeRepository _diaryLikeRepository;
    private readonly ILikeCounterRepository _likeCounterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDiaryLikesEventHandler(IDiaryLikeRepository diaryLikeRepository,
                                                IUnitOfWork unitOfWork,
                                                ILikeCounterRepository likeCounterRepository)
    {
        _diaryLikeRepository = diaryLikeRepository;
        _unitOfWork = unitOfWork;
        _likeCounterRepository = likeCounterRepository;
    }
    public async Task Handle(CreateDiaryLikesEvent notification, CancellationToken cancellationToken)
    {
        var like = DiaryLike.Create(notification.diaryId);
        await _diaryLikeRepository.Create(like);
        var likeCounter = LikeCounter.Create(like.Id);
        _likeCounterRepository.Create(likeCounter);
        await _unitOfWork.Commit(cancellationToken);
    }
}