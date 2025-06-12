using API.DTOs.Statuses;
using API.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Statuses.Commands;
using Domain.Identity.Roles;
using Domain.Statuses;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("problem-statuses")]
[ApiController]
public class StatusesController(ISender sender, IStatusQueries statusQueries) : ControllerBase
{
    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}")]
    [HttpGet("get-all")]
    public async Task<ActionResult<IReadOnlyList<StatusDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await statusQueries.GetAll(cancellationToken);
        return entities.Select(StatusDto.FromDomainModel).ToList();
    }
    
    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}")]
    [HttpGet("get-by-id/{problemStatusId:guid}")]
    public async Task<ActionResult<StatusDto>> Get([FromRoute] Guid problemStatusId,
        CancellationToken cancellationToken)
    {
        var entity = await statusQueries.GetById(new StatusId(problemStatusId), cancellationToken);

        return entity.Match<ActionResult<StatusDto>>(
            ps => StatusDto.FromDomainModel(ps),
            () => NotFound());
    }
    
    [Authorize(Roles = RoleNames.Admin)]
    [HttpPost("create")]
    public async Task<ActionResult<CreateStatusDto>> Create(
        [FromBody] StatusDto request,
        CancellationToken cancellationToken)
    {
        var input = new CreateStatusCommand
        {
            Name = request.Name,
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<CreateStatusDto>>(
            ps => CreateStatusDto.FromDomainModel(ps),
            e => e.ToObjectResult());
    }
    
    [Authorize(Roles = RoleNames.Admin)]
    [HttpPut("update/{id:guid}")]
    public async Task<ActionResult<CreateStatusDto>> Update(
        [FromRoute] Guid id,
        [FromBody] CreateStatusDto request,
        CancellationToken cancellationToken)
    {
        var input = new UpdateStatusCommand
        {
            ProblemStatusId = id,
            Name = request.Name
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<CreateStatusDto>>(
            ps => CreateStatusDto.FromDomainModel(ps),
            e => e.ToObjectResult());
    }
    
    [Authorize(Roles = RoleNames.Admin)]
    [HttpDelete("delete/{id:guid}")]
    public async Task<ActionResult<StatusDto>> Delete(
        [FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var input = new DeleteStatusCommand
        {
            ProblemStatusId = id
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<StatusDto>>(
            ps => StatusDto.FromDomainModel(ps),
            e => e.ToObjectResult());
    }
}