using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Users.Dto;
using DailyDiary.Domain.Common;
using MediatR;
using OneOf;

namespace DailyDiary.Application.Users.LoginUserQuery;

public sealed record LoginUserQuery(string Email, string Password)
    : IRequest<OneOf<ApiResponse<UserLoggedDto>, Error>> {}