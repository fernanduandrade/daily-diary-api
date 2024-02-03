using DailyDiary.Application.Common.Interfaces;
using DailyDiary.Application.Common.Models;
using DailyDiary.Domain.Diaries;
using DailyDiary.Domain.UserLikes;
using MediatR;

namespace DailyDiary.Application.Likes.UnFavorite;

public class UnfavoriteCommandHandler : IRequestHandler<UnfavoriteCommand, ApiResponse<Unit>>
{
    private readonly IUserLikeRepository _userLikeRepository;
    private readonly IDiaryRepository _diaryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UnfavoriteCommandHandler(IUnitOfWork unitOfWork, IUserLikeRepository userLikeRepository, IDiaryRepository diaryRepository)
    {
        _unitOfWork = unitOfWork;
        _userLikeRepository = userLikeRepository;
        _diaryRepository = diaryRepository;
    }
    public async Task<ApiResponse<Unit>> Handle(UnfavoriteCommand request, CancellationToken cancellationToken)
    {
        var diaryExist = await _diaryRepository.GetById(request.DiaryId);
        
        if(diaryExist is null)
            return ApiResponse<Unit>.Response(Unit.Value, "Error, can't unfavorite a diary that doesn't exist.", false); 
        var userLike = await _userLikeRepository.GetByUserAndDiaryId(request.UserId, request.DiaryId);
        if (userLike is null)
            return ApiResponse<Unit>.Response(Unit.Value, "Error, Diary hasn't been liked", false);
        
        userLike.DecrementLikeEvent(request.DiaryId);
        _userLikeRepository.Delete(userLike);
        await _unitOfWork.Commit(cancellationToken);
        return ApiResponse<Unit>.Response(Unit.Value, "Done");
    }
}