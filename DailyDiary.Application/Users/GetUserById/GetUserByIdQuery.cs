using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Users.Dto;
using DailyDiary.Domain.Common;
using MediatR;
using OneOf;

namespace DailyDiary.Application.Users.GetUserById;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<OneOf<ApiResponse<UserDto>, Error>> {}