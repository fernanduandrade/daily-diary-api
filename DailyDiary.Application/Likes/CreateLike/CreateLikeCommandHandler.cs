using AutoMapper;
using DailyDiary.Application.Common.Interfaces;
using DailyDiary.Domain.Diaries;
using DailyDiary.Domain.Likes;
using DailyDiary.Domain.Users;
using MediatR;

namespace DailyDiary.Application.Likes.CreateLike;

public class CreateLikeCommandHandler : IRequestHandler<CreateLikeCommand, Unit>
{
    private readonly ILikeRepository _likeRepository;
    private readonly IDiaryRepository _diaryRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLikeCommandHandler(ILikeRepository likeRepository, IUnitOfWork unitOfWork, IDiaryRepository diaryRepository, IUserRepository userRepository)
    {
        _likeRepository = likeRepository;
        _unitOfWork = unitOfWork;
        _diaryRepository = diaryRepository;
        _userRepository = userRepository;
    }
    public async Task<Unit> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
    {
        Like like = Like.Create(request.UserId, request.DiaryId);
        await _likeRepository.Create(like);
        await _unitOfWork.Commit(cancellationToken);
        return Unit.Value;
    }
}