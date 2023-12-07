namespace DailyDiary.Application.Users.Dto;

public sealed record UserLoggedDto(Guid Id, string Name, string Email, string Token);