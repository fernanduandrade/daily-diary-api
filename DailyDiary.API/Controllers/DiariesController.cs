using DailyDiary.API.Controllers.Base;
using DailyDiary.Application.Diaries.CreateDiary;
using DailyDiary.Application.Diaries.GetUserDairies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyDiary.API.Controllers;

[Authorize]
public class DiariesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateDiaryCommand command)
    {
        var result = await Mediator.Send(command);

        return result.Match<IActionResult>(
            diary => Ok(diary),
            error => BadRequest(error));
    }

    [HttpGet("users/{userId}")]
    public async Task<IActionResult> GetUserDiaries([FromRoute] GetUserDiariesQuery query)
    {
        var result = await Mediator.Send(query);

        return Ok(result);
    }
}