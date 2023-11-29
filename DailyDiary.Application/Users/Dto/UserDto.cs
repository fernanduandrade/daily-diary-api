using AutoMapper;
using DailyDiary.Application.Common.Mapping;
using DailyDiary.Domain.Users;

namespace DailyDiary.Application.Users.Dto;


public sealed record UserDto : IMapFrom<User>
{
    public Guid Id { get; init; }
    public string? Email { get; init; }
    public string? Name { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserDto>()
            .ForMember(
                dest => dest.Email,
                map => map.MapFrom(x => x.Email.Value))
            .ForMember(
                dest => dest.Name,
                map => map.MapFrom(x => x.Name));
    }
}