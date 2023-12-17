using AutoMapper;
using DailyDiary.Application.Common.Interfaces;
using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Diaries.Dto;
using DailyDiary.Domain.Common;
using DailyDiary.Domain.Diaries;
using MediatR;
using OneOf;

namespace DailyDiary.Application.Diaries.UpdateDiary;

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