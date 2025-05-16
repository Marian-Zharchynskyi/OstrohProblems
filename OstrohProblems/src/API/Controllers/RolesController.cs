using API.DTOs.Users;
using Application.Common.Interfaces.Queries;
using Domain.Identity;
using Domain.Identity.Roles;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("roles")]
// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
// [Authorize(Roles = RoleNames.Admin)]
[ApiController]
public class RolesController(IRoleQueries roleQueries) : ControllerBase
{
    [HttpGet("get-all")]
    public async Task<ActionResult<IReadOnlyList<RoleDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await roleQueries.GetAll(cancellationToken);

        return entities.Select(RoleDto.FromDomainModel).ToList();
    }

    [HttpGet("get-by-id/{roleId:guid}")]
    public async Task<ActionResult<RoleDto>> GetById([FromRoute] Guid roleId, CancellationToken cancellationToken)
    {
        var entity = await roleQueries.GetById(new RoleId(roleId), cancellationToken);

        return entity.Match<ActionResult<RoleDto>>(
            r => RoleDto.FromDomainModel(r),
            () => NotFound()
        );
    }

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