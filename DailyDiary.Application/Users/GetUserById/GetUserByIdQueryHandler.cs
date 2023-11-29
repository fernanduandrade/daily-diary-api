using AutoMapper;
using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Users.Dto;
using DailyDiary.Domain.Common;
using DailyDiary.Domain.Users;
using MediatR;
using OneOf;

namespace DailyDiary.Application.Users.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, OneOf<ApiResponse<UserDto>, Error>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        => (_userRepository, _mapper) = (userRepository, mapper);
    public async Task<OneOf<ApiResponse<UserDto>, Error>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user is null)
            return UserErrors.NotFound;

        var dto = _mapper.Map<UserDto>(user);
        return new ApiResponse<UserDto>() { Data = dto, Message = "OK", Success = true };
    }
}