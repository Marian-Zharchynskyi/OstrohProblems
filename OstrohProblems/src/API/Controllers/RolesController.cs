using API.DTOs.Users;
using Application.Common.Interfaces.Queries;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("roles")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Roles = AuthSettings.AdminRole)]
[ApiController]
public class RolesController(ISender sender, IRoleQueries roleQueries) : ControllerBase
{
    [HttpGet("get-all")]
    public async Task<ActionResult<IReadOnlyList<RoleDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await roleQueries.GetAll(cancellationToken);

        return entities.Select(RoleDto.FromDomainModel).ToList();
    }
}