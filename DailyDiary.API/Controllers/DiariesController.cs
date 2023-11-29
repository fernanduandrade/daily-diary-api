using DailyDiary.API.Controllers.Base;
using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Diaries.CreateDiary;
using DailyDiary.Application.Diaries.DTO;
using DailyDiary.Application.Diaries.GetUserDairies;
using DailyDiary.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DailyDiary.API.Controllers;

[Authorize]
public class DiariesController : BaseController
{
    [SwaggerOperation(Summary = "Creates a diary")]
    [ProducesResponseType(typeof(ApiResponse<DiaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateDiaryCommand command)
    {
        var result = await Mediator.Send(command);

        return result.Match<IActionResult>(
            diary => Ok(diary),
            error => BadRequest(error));
    }

    [SwaggerOperation(Summary = "Return all diaries created by userId")]
    [ProducesResponseType(typeof(ApiResponse<List<DiaryDto>>), StatusCodes.Status200OK)]
    [HttpGet("users/{userId}")]
    public async Task<IActionResult> GetUserDiaries([FromRoute] GetUserDiariesQuery query)
    {
        var result = await Mediator.Send(query);

        return Ok(result);
    }
}