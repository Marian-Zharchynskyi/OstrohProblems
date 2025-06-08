using API.DTOs.Users;
using Application.Common.Interfaces.Queries;
using Domain.Identity.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("roles")]
[ApiController]
public class RolesController(IRoleQueries roleQueries) : ControllerBase
{
    [Authorize(Roles = RoleNames.Admin)]
    [HttpGet("get-all")]
    public async Task<ActionResult<IReadOnlyList<RoleDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await roleQueries.GetAll(cancellationToken);

        return entities.Select(RoleDto.FromDomainModel).ToList();
    }

    [Authorize(Roles = RoleNames.Admin)]
    [HttpGet("get-by-id/{roleId:guid}")]
    public async Task<ActionResult<RoleDto>> GetById([FromRoute] Guid roleId, CancellationToken cancellationToken)
    {
        var entity = await roleQueries.GetById(new RoleId(roleId), cancellationToken);

        return entity.Match<ActionResult<RoleDto>>(
            r => RoleDto.FromDomainModel(r),
            () => NotFound()
        );
    }

    [Authorize(Roles = RoleNames.Admin)]
    [HttpGet("get-by-name/{roleName}")]
    public async Task<ActionResult<RoleDto>> GetByName([FromRoute] string roleName, CancellationToken cancellationToken)
    {
        var entity = await roleQueries.GetByName(roleName, cancellationToken);

        return entity.Match<ActionResult<RoleDto>>(
            r => RoleDto.FromDomainModel(r),
            () => NotFound()
        );
    }
}