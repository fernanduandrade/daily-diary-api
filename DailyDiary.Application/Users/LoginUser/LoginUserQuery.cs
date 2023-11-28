using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Users.Dto;
using DailyDiary.Domain.Common;
using OneOf;
using MediatR;

namespace DailyDiary.Application.Users.LoginUser;

public sealed record LoginUserQuery(string Email, string Password)
    : IRequest<OneOf<ApiResponse<UserLoggedDto>, Error>> {}