using DailyDiary.Application.Common.Interfaces;
using DailyDiary.Domain.UserLikes;
using MediatR;

namespace DailyDiary.Application.Likes.UnFavorite;

public class UnfavoriteCommandHandler : IRequestHandler<UnfavoriteCommand, Unit>
{
    private readonly IUserLikeRepository _userLikeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UnfavoriteCommandHandler(IUnitOfWork unitOfWork, IUserLikeRepository userLikeRepository)
    {
        _unitOfWork = unitOfWork;
        _userLikeRepository = userLikeRepository;
    }
    public async Task<Unit> Handle(UnfavoriteCommand request, CancellationToken cancellationToken)
    {
        var userLike = await _userLikeRepository.GetByUserAndDiaryId(request.UserId, request.DiaryId);
        if (userLike is null)
            return Unit.Value;
        
        userLike.DecrementLikeEvent(request.DiaryId);
        _userLikeRepository.Delete(userLike);
        await _unitOfWork.Commit(cancellationToken);
        return Unit.Value;
    }
}