using DailyDiary.Application.Common.Interfaces;
using DailyDiary.Application.Common.Models;
using DailyDiary.Domain.UserLikes;
using MediatR;

namespace DailyDiary.Application.Likes.Favorite;

public class CreateLikeCommandHandler : IRequestHandler<FavoriteCommand, ApiResponse<Unit>>
{
    private readonly IUserLikeRepository _userLikeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLikeCommandHandler(IUnitOfWork unitOfWork, IUserLikeRepository userLikeRepository)
    {
        _unitOfWork = unitOfWork;
        _userLikeRepository = userLikeRepository;
    }
    public async Task<ApiResponse<Unit>> Handle(FavoriteCommand request, CancellationToken cancellationToken)
    {
        var hasAlreadyLike = await _userLikeRepository.GetByUserAndDiaryId(request.UserId, request.DiaryId);
        if(hasAlreadyLike is not null)
            return ApiResponse<Unit>.Response(Unit.Value, "Already favorited", false);
        
        var userLike = UserLike.Create(request.UserId, request.DiaryId);
        userLike.IncrementLikeEvent(request.DiaryId);
        _userLikeRepository.Add(userLike);
        await _unitOfWork.Commit(cancellationToken);
        return ApiResponse<Unit>.Response(Unit.Value, "Done");
    }
}