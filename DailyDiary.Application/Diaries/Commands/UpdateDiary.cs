using AutoMapper;
using DailyDiary.Application.Common.Interfaces;
using DailyDiary.Application.Common.Mapping;
using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Diaries.Dto;
using DailyDiary.Domain.Common;
using DailyDiary.Domain.Diaries;
using OneOf;
using MediatR;

namespace DailyDiary.Application.Diaries.Commands;

public sealed record UpdateDiaryCommand : IMapFrom<Diary>, IRequest<OneOf<ApiResponse<DiaryDto>, Error>>
{
    public bool IsPublic { get; init; }
    public string? Text { get; init; }
    public string? Title { get; init; }
    public string? Mood { get; init; }
    public DateTime CreatedAt { get; init; }
    public Guid UserId { get; init; }
    public Guid Id { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateDiaryCommand, Diary>();
    }
}

public class UpdateDiaryCommandHandler : IRequestHandler<UpdateDiaryCommand, OneOf<ApiResponse<DiaryDto>, Error>>
{
    private readonly IDiaryRepository _diaryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public UpdateDiaryCommandHandler(IDiaryRepository diaryRepository, IMapper mapper, IUnitOfWork unitOfWork)
        => (_diaryRepository, _mapper, _unitOfWork) = (diaryRepository, mapper, unitOfWork);
    public async Task<OneOf<ApiResponse<DiaryDto>, Error>> Handle(
        UpdateDiaryCommand request, CancellationToken cancellationToken)
    {
        var newDiary = _mapper.Map<Diary>(request);
        _diaryRepository.Update(newDiary);
        await _unitOfWork.Commit(cancellationToken);
        var dto = _mapper.Map<DiaryDto>(newDiary);
        return new ApiResponse<DiaryDto>() { Data = dto, Message = "OK", Success = true };
    }
}