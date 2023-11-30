using AutoMapper;
using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Users.Dto;
using DailyDiary.Domain.Common;
using DailyDiary.Domain.Users;
using MediatR;
using OneOf;

namespace DailyDiary.Application.Users.Commands;

public sealed record CreateUserCommand(string Name, string Email, string Password) 
    : IRequest<OneOf<ApiResponse<UserDto>, Error>>;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, OneOf<ApiResponse<UserDto>, Error>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        => (_userRepository, _mapper) = (userRepository, mapper);
    public async Task<OneOf<ApiResponse<UserDto>, Error>> Handle(
        CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.VerifyEmail(request.Email);
        if (userExists)
            return UserErrors.InvalidEmail;
        
        var email = Email.Create(request.Email).Value;
        var password = Password.Create(request.Password).Value;
        var user = User.Create(email, request.Name, password);

        var entity = await _userRepository.AddAsync(user);
        var dto = _mapper.Map<UserDto>(entity);
        
        return new ApiResponse<UserDto>() { Data = dto, Message = "OK", Success = true};
    }
}