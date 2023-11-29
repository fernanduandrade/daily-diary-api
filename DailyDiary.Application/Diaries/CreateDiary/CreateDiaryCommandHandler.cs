using AutoMapper;
using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Diaries.DTO;
using DailyDiary.Domain.Common;
using DailyDiary.Domain.Diaries;
using OneOf;
using MediatR;

namespace DailyDiary.Application.Diaries.CreateDiary;

public class CreateDiaryCommandHandler : IRequestHandler<CreateDiaryCommand, OneOf<ApiResponse<DiaryDto>, Error>>
{
    private readonly IDiaryRepository _diaryRepository;
    private readonly IMapper _mapper;
    
    public CreateDiaryCommandHandler(IDiaryRepository diaryRepository, IMapper mapper)
        => (_diaryRepository, _mapper) = (diaryRepository, mapper);
    public async Task<OneOf<ApiResponse<DiaryDto>, Error>> Handle(CreateDiaryCommand request, CancellationToken cancellationToken)
    {
        var hasPublishedToday = await _diaryRepository.HasPublish(request.userId);
        if (hasPublishedToday)
            return DiaryErrors.InvalidDate;

        var diary = Diary.Create(request.Title, request.Text, request.Mood, request.userId ,request.IsPublic);
        Diary entity = await _diaryRepository.AddAsync(diary);
        var dto = _mapper.Map<DiaryDto>(entity);

        return new ApiResponse<DiaryDto>() { Data = dto, Message = "OK", Success = true };
    }
}