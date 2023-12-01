using AutoMapper;
using DailyDiary.Application.Common.Interfaces;
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
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork)
        => (_userRepository, _mapper, _unitOfWork) = (userRepository, mapper, unitOfWork);
    public async Task<OneOf<ApiResponse<UserDto>, Error>> Handle(
        CreateUserCommand request, CancellationToken cancellationToken)
    {
        var emailExists = await _userRepository.GetByEmail(request.Email);
        if (emailExists is not null)
            return UserErrors.InvalidEmail;
        
        var email = Email.Create(request.Email).Value;
        var password = Password.Create(request.Password).Value;
        var user = User.Create(email, request.Name, password);

        await _userRepository.AddAsync(user);
        await _unitOfWork.Commit(cancellationToken);
        var dto = _mapper.Map<UserDto>(user);
        
        return new ApiResponse<UserDto>() { Data = dto, Message = "OK", Success = true};
    }
}