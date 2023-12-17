using AutoMapper;
using DailyDiary.Application.Common.Mapping;
using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Diaries.Dto;
using DailyDiary.Domain.Common;
using DailyDiary.Domain.Diaries;
using MediatR;
using OneOf;

namespace DailyDiary.Application.Diaries.UpdateDiary;

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