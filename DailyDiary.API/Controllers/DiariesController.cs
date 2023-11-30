using DailyDiary.API.Controllers.Base;
using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Diaries.Commands;
using DailyDiary.Application.Diaries.Dto;
using DailyDiary.Application.Diaries.Queries;
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
    
    [SwaggerOperation(Summary = "Deletes a diary")]
    [ProducesResponseType(204)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteDiaryCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
    
    [SwaggerOperation(Summary = "Returns diary by id")]
    [ProducesResponseType(typeof(ApiResponse<DiaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] GetDiaryByIdQuery query)
    {
        var result = await Mediator.Send(query);

        return result.Match<IActionResult>(
            diary => Ok(diary),
            error => BadRequest(error));
    }
    
    [SwaggerOperation(Summary = "Updates a diary")]
    [ProducesResponseType(typeof(ApiResponse<DiaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [HttpPut]
    public async Task<IActionResult> Update(UpdateDiaryCommand command)
    {
        var result = await Mediator.Send(command);

        return result.Match<IActionResult>(
            diary => Ok(diary),
            error => BadRequest(error));
    }
    
}