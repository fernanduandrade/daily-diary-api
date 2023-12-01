using AutoMapper;
using DailyDiary.Application.Common.Interfaces;
using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Diaries.Dto;
using DailyDiary.Domain.Common;
using DailyDiary.Domain.Diaries;
using OneOf;
using MediatR;

namespace DailyDiary.Application.Diaries.Commands;

public sealed record CreateDiaryCommand(
    Guid userId,
    string Title,
    string Text,
    string Mood,
    bool IsPublic) : IRequest<OneOf<ApiResponse<DiaryDto>, Error>>;

public class CreateDiaryCommandHandler : IRequestHandler<CreateDiaryCommand, OneOf<ApiResponse<DiaryDto>, Error>>
{
    private readonly IDiaryRepository _diaryRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateDiaryCommandHandler(IDiaryRepository diaryRepository, IMapper mapper, IUnitOfWork unitOfWork)
        => (_diaryRepository, _mapper, _unitOfWork) = (diaryRepository, mapper, unitOfWork);
    public async Task<OneOf<ApiResponse<DiaryDto>, Error>> Handle(
        CreateDiaryCommand request, CancellationToken cancellationToken)
    {
        var hasPublishedToday = await _diaryRepository.HasPublish(request.userId);
        if (hasPublishedToday)
            return DiaryErrors.InvalidDate;

        var diary = Diary.Create(request.Title, request.Text, request.Mood, request.userId ,request.IsPublic);
        await _diaryRepository.AddAsync(diary);
        await _unitOfWork.Commit(cancellationToken);
        var dto = _mapper.Map<DiaryDto>(diary);

        return new ApiResponse<DiaryDto>() { Data = dto, Message = "OK", Success = true };
    }
}