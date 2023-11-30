using AutoMapper;
using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Diaries.Dto;
using DailyDiary.Domain.Diaries;
using MediatR;

namespace DailyDiary.Application.Diaries.Queries;

public sealed record GetUserDiariesQuery(Guid userId) : IRequest<ApiResponse<List<DiaryDto>>>;

public class GetUserDiariesQueryHandler : IRequestHandler<GetUserDiariesQuery, ApiResponse<List<DiaryDto>>>
{
    private readonly IDiaryRepository _diaryRepository;
    private readonly IMapper _mapper;

    public GetUserDiariesQueryHandler(IDiaryRepository diaryRepository, IMapper mapper)
        => (_diaryRepository, _mapper) = (diaryRepository, mapper);
    public async Task<ApiResponse<List<DiaryDto>>> Handle(
        GetUserDiariesQuery request, CancellationToken cancellationToken)
    {
        var diaries = await _diaryRepository.GetAllByUserId(request.userId);
        
        var dto = _mapper.Map<List<DiaryDto>>(diaries);
        return new ApiResponse<List<DiaryDto>>() { Data = dto, Message = "OK", Success = true };
    }
}