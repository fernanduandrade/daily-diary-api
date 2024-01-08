using DailyDiary.Application.Common.Interfaces;
using DailyDiary.Domain.DiaryLikes;
using DailyDiary.Domain.UserLikes;
using MediatR;

namespace DailyDiary.Application.Likes.UnFavorite;

public class UnfavoriteCommandHandler : IRequestHandler<UnfavoriteCommand, Unit>
{
    private readonly IDiaryLikeRepository _diaryLikeRepository;
    private readonly IUserLikeRepository _userLikeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UnfavoriteCommandHandler(IDiaryLikeRepository diaryLikeRepository, IUnitOfWork unitOfWork, IUserLikeRepository userLikeRepository)
    {
        _diaryLikeRepository = diaryLikeRepository;
        _unitOfWork = unitOfWork;
        _userLikeRepository = userLikeRepository;
    }
    public async Task<Unit> Handle(UnfavoriteCommand request, CancellationToken cancellationToken)
    {
        var userLike = await _userLikeRepository.GetByUserAndDiaryId(request.UserId, request.DiaryId);
        if (userLike is null)
            return Unit.Value;
        
        var diaryLike = await _diaryLikeRepository.GetByDiaryId(request.DiaryId);
        userLike.DecrementLikeEvent(diaryLike.Id);
        _userLikeRepository.Delete(userLike);
        await _unitOfWork.Commit(cancellationToken);
        return Unit.Value;
    }
}