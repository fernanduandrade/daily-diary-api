using AutoMapper;
using DailyDiary.Application.Common.Mapping;
using DailyDiary.Application.Shared.Dto;
using DailyDiary.Domain.Diaries;

namespace DailyDiary.Application.Diaries.Dto;

public sealed record DiaryDto : IMapFrom<Diary>
{
    public bool IsPublic { get; init; }
    public string? Text { get; init; }
    public string? Title { get; init; }
    public string? Mood { get; init; }
    public DateTime CreatedAt { get; init; }
    public Guid Id { get; init; }
    public bool IsLiked { get; init; }
    public UserDto User { get; init; }

    public void Mapping(Profile profile)
    {
        profile
            .CreateMap<Diary, DiaryDto>()
            .ForMember(x => x.User, 
                opt => opt.MapFrom(src => src.User));
    }
}
