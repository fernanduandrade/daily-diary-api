using AutoMapper;
using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Diaries.Dto;
using DailyDiary.Domain.Common;
using DailyDiary.Domain.Diaries;
using MediatR;
using OneOf;

namespace DailyDiary.Application.Diaries.SearchPublicDiaries;

public class SearchPublicDiariesQueryHandler : IRequestHandler<SearchPublicDiariesQuery, OneOf<ApiResponse<DiaryDto[]>, Error>
{
    private readonly IDiaryRepository _diaryRepository;
    private readonly IMapper _mapper;

    public SearchPublicDiariesQueryHandler(IDiaryRepository diaryRepository, IMapper mapper)
    {
        _diaryRepository = diaryRepository;
        _mapper = mapper;
    }

    public async Task<OneOf<ApiResponse<DiaryDto[]>, Error>> Handle(SearchPublicDiariesQuery request, CancellationToken cancellationToken)
    {
        var diaries = await _diaryRepository.GetPublics();
        throw new NotImplementedException();
    }
}