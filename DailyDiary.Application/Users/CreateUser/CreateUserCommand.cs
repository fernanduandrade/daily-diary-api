using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Shared.Dto;
using DailyDiary.Domain.Common;
using MediatR;
using OneOf;


namespace DailyDiary.Application.Users.CreateUser;

public sealed record CreateUserCommand(string Name, string Email, string Password) 
    : IRequest<OneOf<ApiResponse<UserDto>, Error>>;