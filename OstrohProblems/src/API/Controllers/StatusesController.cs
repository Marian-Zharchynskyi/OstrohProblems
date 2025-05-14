using API.DTOs;
using API.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Statuses.Commands;
using Domain.Statuses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("problem-statuses")]
[ApiController]
public class StatusesController(ISender sender, IStatusQueries statusQueries) : ControllerBase
{
    [HttpGet("get-all")]
    public async Task<ActionResult<IReadOnlyList<StatusDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await statusQueries.GetAll(cancellationToken);
        return entities.Select(StatusDto.FromDomainModel).ToList();
    }

    [HttpGet("get-by-id/{problemStatusId:guid}")]
    public async Task<ActionResult<StatusDto>> Get([FromRoute] Guid problemStatusId,
        CancellationToken cancellationToken)
    {
        var entity = await statusQueries.GetById(new StatusId(problemStatusId), cancellationToken);

        return entity.Match<ActionResult<StatusDto>>(
            ps => StatusDto.FromDomainModel(ps),
            () => NotFound());
    }

    [HttpPost("create")]
    public async Task<ActionResult<StatusDto>> Create(
        [FromBody] StatusDto request,
        CancellationToken cancellationToken)
    {
        var input = new CreateStatusCommand
        {
            Name = request.Name,
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<StatusDto>>(
            ps => StatusDto.FromDomainModel(ps),
            e => e.ToObjectResult());
    }

    [HttpPut("update")]
    public async Task<ActionResult<StatusDto>> Update(
        [FromBody] StatusDto request,
        CancellationToken cancellationToken)
    {
        var input = new UpdateStatusCommand
        {
            ProblemStatusId = request.Id!.Value,
            Name = request.Name
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<StatusDto>>(
            ps => StatusDto.FromDomainModel(ps),
            e => e.ToObjectResult());
    }

    [HttpDelete("delete/{problemStatusId:guid}")]
    public async Task<ActionResult<StatusDto>> Delete(
        [FromRoute] Guid problemStatusId, CancellationToken cancellationToken)
    {
        var input = new DeleteStatusCommand
        {
            ProblemStatusId = problemStatusId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<StatusDto>>(
            ps => StatusDto.FromDomainModel(ps),
            e => e.ToObjectResult());
    }
}