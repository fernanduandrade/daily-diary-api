using DailyDiary.Application.Common.Interfaces;
using DailyDiary.Domain.DiaryLikes;
using DailyDiary.Domain.UserLikes;
using MediatR;

namespace DailyDiary.Application.Likes.Favorite;

public class CreateLikeCommandHandler : IRequestHandler<FavoriteCommand, Unit>
{
    private readonly IDiaryLikeRepository _diaryLikeRepository;
    private readonly IUserLikeRepository _userLikeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLikeCommandHandler(IDiaryLikeRepository diaryLikeRepository, IUnitOfWork unitOfWork, IUserLikeRepository userLikeRepository)
    {
        _diaryLikeRepository = diaryLikeRepository;
        _unitOfWork = unitOfWork;
        _userLikeRepository = userLikeRepository;
    }
    public async Task<Unit> Handle(FavoriteCommand request, CancellationToken cancellationToken)
    {
        var hasAlreadyLike = await _userLikeRepository.GetByUserAndDiaryId(request.UserId, request.DiaryId);
        if(hasAlreadyLike is not null)
            return Unit.Value;
        
        var userLike = UserLike.Create(request.UserId, request.DiaryId);
        var diaryLike = await _diaryLikeRepository.GetByDiaryId(request.DiaryId);
        userLike.IncrementLikeEvent(diaryLike.Id);
        _userLikeRepository.Add(userLike);
        await _unitOfWork.Commit(cancellationToken);
        return Unit.Value;
    }
}