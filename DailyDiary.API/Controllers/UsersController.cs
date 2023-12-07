using DailyDiary.API.Controllers.Base;
using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Users.Commands;
using DailyDiary.Application.Users.Dto;
using DailyDiary.Application.Users.Queries;
using DailyDiary.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DailyDiary.API.Controllers;

public class UsersController : BaseController
{
    [SwaggerOperation(Summary = "Creates a user")]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand command)
    {
        var result = await Mediator.Send(command);

        return result.Match<IActionResult>(
            user => Created($"api/users/{user.Data.Id}", user),
            error => BadRequest(error));
    }
    
    [Authorize]
    [SwaggerOperation(Summary = "Finds a user by the id")]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] GetUserByIdQuery query)
    {
        var result = await Mediator.Send(query);

        return result.Match<IActionResult>(
            user => Ok(user),
            error => BadRequest(error));
    }

    [SwaggerOperation(Summary = "Validates a user access")]
    [ProducesResponseType(typeof(ApiResponse<UserLoggedDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserQuery query)
    {
        var result = await Mediator.Send(query);

        return result.Match<IActionResult>(
            user => Ok(user),
                error => BadRequest(error));
    }
}