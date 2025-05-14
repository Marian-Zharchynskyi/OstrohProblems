using API.DTOs.Users;
using API.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Users.Commands;
using Domain.Identity.Roles;
using Domain.Identity.Users;
using Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("users")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UsersController(ISender sender, IUserQueries userQueries) : ControllerBase
{
    [Authorize(Roles = RoleNames.Admin)]
    [HttpGet("get-all")]
    public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await userQueries.GetAll(cancellationToken);

        return entities.Select(UserDto.FromDomainModel).ToList();
    }

    [Authorize(Roles = $"{RoleNames.Admin}")]
    [HttpGet("get-by-id/{userId:guid}")]
    public async Task<ActionResult<UserDto>> Get([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var entity = await userQueries.GetById(new UserId(userId), cancellationToken);

        return entity.Match<ActionResult<UserDto>>(
            p => UserDto.FromDomainModel(p),
            () => NotFound());
    }

    [Authorize(Roles = RoleNames.Admin)]
    [HttpDelete("delete/{userId:guid}")]
    public async Task<ActionResult<UserDto>>
        Delete([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var input = new DeleteUserCommand()
        {
            UserId = userId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<UserDto>>(
            c => UserDto.FromDomainModel(c),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = RoleNames.Admin)]
    [HttpPut("update-roles/{userId}")]
    public async Task<ActionResult<UserDto>> UpdateRoles(
        [FromRoute] Guid userId,
        [FromBody] List<Guid> roleIds,
        CancellationToken cancellationToken)
    {
        var input = new ChangeRolesForUserCommand
        {
            UserId = userId,
            RoleIds = roleIds
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<UserDto>>(
            user => UserDto.FromDomainModel(user),
            error => error.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin},{RoleNames.User}")]
    [HttpPut("image/{userId}")]
    public async Task<ActionResult<UserDto>> Upload(
        [FromRoute] Guid userId,
        IFormFile imageFile,
        CancellationToken cancellationToken)
    {
        var input = new UploadUserImageCommand
        {
            UserId = userId,
            ImageFile = imageFile
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<UserDto>>(
            u => UserDto.FromDomainModel(u),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin},{RoleNames.User}")]
    [HttpPut("update/{userId:guid}")]
    public async Task<ActionResult<UserDto>> UpdateUser(
        [FromRoute] Guid userId,
        [FromBody] UpdateUserVm user,
        CancellationToken cancellationToken)
    {
        var input = new UpdateUserCommand
        {
            UserId = userId,
            UserName = user.UserName,
            Email = user.Email
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<UserDto>>(
            u => UserDto.FromDomainModel(u),
            e => e.ToObjectResult());
    }
}