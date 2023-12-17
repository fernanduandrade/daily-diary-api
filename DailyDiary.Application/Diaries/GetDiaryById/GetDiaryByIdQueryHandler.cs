using AutoMapper;
using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Diaries.Dto;
using DailyDiary.Domain.Common;
using DailyDiary.Domain.Diaries;
using MediatR;
using OneOf;

namespace DailyDiary.Application.Diaries.GetDiaryById;

public class GetDiaryByIdQueryHandler : IRequestHandler<GetDiaryByIdQuery, OneOf<ApiResponse<DiaryDto>, Error>>
{
    private readonly IDiaryRepository _diaryRepository;
    private readonly IMapper _mapper;

    public GetDiaryByIdQueryHandler(IDiaryRepository diaryRepository, IMapper mapper)
        => (_diaryRepository, _mapper) = (diaryRepository, mapper);
    public async Task<OneOf<ApiResponse<DiaryDto>, Error>> Handle(
        GetDiaryByIdQuery request, CancellationToken cancellationToken)
    {
        var diary = await _diaryRepository.GetById(request.Id);

        if (diary is null)
            return DiaryErrors.NotFound;

        var dto = _mapper.Map<DiaryDto>(diary);

        return new ApiResponse<DiaryDto>() { Data = dto, Message = "OK", Success = true };
    }
}