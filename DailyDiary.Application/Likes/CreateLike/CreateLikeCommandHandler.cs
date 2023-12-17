using AutoMapper;
using DailyDiary.Application.Common.Interfaces;
using DailyDiary.Domain.Likes;
using MediatR;

namespace DailyDiary.Application.Likes.CreateLike;

public class CreateLikeCommandHandler : IRequestHandler<CreateLikeCommand, Unit>
{
    private readonly ILikeRepository _likeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLikeCommandHandler(ILikeRepository likeRepository, IUnitOfWork unitOfWork)
    {
        _likeRepository = likeRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
    {
        Like like = Like.Create(request.UserId, request.DiaryId);
        await _likeRepository.Create(like);
        await _unitOfWork.Commit(cancellationToken);
        return Unit.Value;
    }
}