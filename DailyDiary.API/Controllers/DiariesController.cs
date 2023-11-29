using DailyDiary.API.Controllers.Base;
using DailyDiary.Application.Diaries.CreateDiary;
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
}