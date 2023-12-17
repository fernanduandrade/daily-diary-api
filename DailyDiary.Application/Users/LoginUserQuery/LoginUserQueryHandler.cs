using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Users.Dto;
using DailyDiary.Domain.Common;
using DailyDiary.Domain.Users;
using MediatR;
using Microsoft.Extensions.Configuration;
using OneOf;
namespace DailyDiary.Application.Users.LoginUserQuery;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, OneOf<ApiResponse<UserLoggedDto>, Error>>
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config;

    public LoginUserQueryHandler(IUserRepository userRepository, IConfiguration configuation)
        => (_userRepository, _config) = (userRepository, configuation);
    public async Task<OneOf<ApiResponse<UserLoggedDto>, Error>> Handle(
        LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(request.Email);
        if (user is null)
            return UserErrors.NotFound;

        var verifyPassword = Password.Verify(request.Password, user.Password.Value);

        if (!verifyPassword)
            return UserErrors.NotFound;

        string token = Token.Create(_config);
        UserLoggedDto dto = new(user.Id, user.Name, request.Email, token);

        return new ApiResponse<UserLoggedDto>() { Data = dto, Message = "OK", Success = true };
    }
}