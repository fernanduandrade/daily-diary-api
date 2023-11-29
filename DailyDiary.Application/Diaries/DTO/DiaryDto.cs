using AutoMapper;
using DailyDiary.Application.Common.Mapping;
using DailyDiary.Domain.Diaries;

namespace DailyDiary.Application.Diaries.DTO;

public sealed record DiaryDto : IMapFrom<Diary>
{
    public bool IsPublic { get; init; }
    public string? Text { get; init; }
    public string? Title { get; init; }
    public string? Mood { get; init; }
    public DateTime CreatedAt { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Diary, DiaryDto>();
    }
}
