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
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Roles = RoleNames.Admin)]
[ApiController]
public class RolesController(IRoleQueries roleQueries) : ControllerBase
{
    [HttpGet("get-all")]
    public async Task<ActionResult<IReadOnlyList<RoleDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await roleQueries.GetAll(cancellationToken);

        return entities.Select(RoleDto.FromDomainModel).ToList();
    }
}