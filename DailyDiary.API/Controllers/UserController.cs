using DailyDiary.API.Controllers.Base;
using DailyDiary.Application.Users.CreateUser;
using DailyDiary.Application.Users.GetUserById;
using DailyDiary.Application.Users.LoginUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyDiary.API.Controllers;

public class UserController : BaseController
{
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand command)
    {
        var result = await Mediator.Send(command);

        return result.Match<IActionResult>(
            user => Ok(user),
            error => BadRequest(error));
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Create([FromQuery] GetUserByIdQuery query)
    {
        var result = await Mediator.Send(query);

        return result.Match<IActionResult>(
            user => Ok(user),
            error => BadRequest(error));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserQuery query)
    {
        var result = await Mediator.Send(query);

        return result.Match<IActionResult>(
            user => Ok(user),
                error => BadRequest(error));
    }
}