using DailyDiary.Application.Common.Interfaces;
using DailyDiary.Application.Likes.CreateFavorite;
using DailyDiary.Domain.DiaryLikes;
using DailyDiary.Domain.UserLikes;
using MediatR;

namespace DailyDiary.Application.Likes.UnFavorite;

public class UnfavoriteCommandHandler : IRequestHandler<UnfavoriteCommand, Unit>
{
    private readonly IDiaryLikeRepository _diaryLikeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UnfavoriteCommandHandler(IDiaryLikeRepository diaryLikeRepository, IUnitOfWork unitOfWork)
    {
        _diaryLikeRepository = diaryLikeRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(UnfavoriteCommand request, CancellationToken cancellationToken)
    {
        var userLike = UserLike.Create(request.UserId, request.DiaryId);
        var diaryLike = await _diaryLikeRepository.GetByDiaryId(request.DiaryId);
        userLike.IncrementLikeEvent(diaryLike.Id);
        await _unitOfWork.Commit(cancellationToken);
        return Unit.Value;
    }
}