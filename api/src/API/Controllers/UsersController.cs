using API.DTOs.Users;
using API.Modules.Errors;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Queries;
using Application.Services.ImageService;
using Application.Users.Commands;
using Domain.Identity.Roles;
using Domain.Identity.Users;
using Domain.PagedResults;
using Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("users")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UsersController(ISender sender, IUserQueries userQueries, IIdentityService identityService, IImageService imageService) : ControllerBase
{
    [Authorize(Roles = $"{RoleNames.Admin},{RoleNames.User}")]
    [HttpGet("current")]
    public async Task<ActionResult<UserDto>> GetCurrentUser(CancellationToken cancellationToken)
    {
        var userIdOption = identityService.GetUserId();
        
        return await userIdOption.Match(
            async userId =>
            {
                var user = await userQueries.GetById(userId, cancellationToken);
                return user.Match<ActionResult<UserDto>>(
                    u => UserDto.FromDomainModel(u, imageService.GetImageUrl),
                    () => NotFound());
            },
            () => Task.FromResult<ActionResult<UserDto>>(Unauthorized())
        );
    }

    [Authorize(Roles = RoleNames.Admin)]
    [HttpGet("paged")]
    public async Task<ActionResult<PagedResult<UserDto>>> GetPaged(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var (items, totalCount) = await userQueries.GetPaged(page, pageSize, cancellationToken);

        var dtoItems = items.Select(u => UserDto.FromDomainModel(u, imageService.GetImageUrl)).ToList();

        return new PagedResult<UserDto>(
            Items: dtoItems,
            TotalCount: totalCount,
            Page: page,
            PageSize: pageSize
        );
    }
    
    [Authorize(Roles = RoleNames.Admin)]
    [HttpGet("get-all")]
    public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await userQueries.GetAll(cancellationToken);

        return entities.Select(u => UserDto.FromDomainModel(u, imageService.GetImageUrl)).ToList();
    }

    [Authorize(Roles = RoleNames.Admin)]
    [HttpPost("create")]
    public async Task<ActionResult<UserDto>> Create(
        [FromBody] CreateUserDto createUserDto,
        CancellationToken cancellationToken)
    {
        var input = new CreateUserCommand
        {
            Email = createUserDto.Email,
            Password = createUserDto.Password,
            Name = createUserDto.Name,
            Surname = createUserDto.Surname,
            RoleId = createUserDto.RoleId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<UserDto>>(
            user => UserDto.FromDomainModel(user, imageService.GetImageUrl),
            error => error.ToObjectResult());
    }
    
    [Authorize(Roles = RoleNames.Admin)]
    [HttpGet("get-by-id/{userId:guid}")]
    public async Task<ActionResult<UserDto>> Get([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var entity = await userQueries.GetById(new UserId(userId), cancellationToken);

        return entity.Match<ActionResult<UserDto>>(
            p => UserDto.FromDomainModel(p, imageService.GetImageUrl),
            () => NotFound());
    }
    
    [Authorize(Roles = $"{RoleNames.Admin},{RoleNames.User}")]
    [HttpDelete("delete/{userId:guid}")]
    public async Task<ActionResult<UserDto>>
        Delete([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var currentUserIdOption = identityService.GetUserId();
        
        return await currentUserIdOption.Match(
            async currentUserId =>
            {
                var isAdmin = identityService.IsUserInRole(RoleNames.Admin);
                if (!isAdmin && currentUserId.Value != userId)
                {
                    return Forbid();
                }
                
                var input = new DeleteUserCommand()
                {
                    UserId = userId
                };

                var result = await sender.Send(input, cancellationToken);

                return result.Match<ActionResult<UserDto>>(
                    c => UserDto.FromDomainModel(c, imageService.GetImageUrl),
                    e => e.ToObjectResult());
            },
            () => Task.FromResult<ActionResult<UserDto>>(Unauthorized())
        );
    }
    
    [Authorize(Roles = RoleNames.Admin)]
    [HttpPut("update-roles/{userId}")]
    public async Task<ActionResult<UserDto>> UpdateRoles(
        [FromRoute] Guid userId,
        [FromBody] Guid roleId,
        CancellationToken cancellationToken)
    {
        var input = new ChangeRoleForUserCommand
        {
            UserId = userId,
            RoleId = roleId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<UserDto>>(
            user => UserDto.FromDomainModel(user, imageService.GetImageUrl),
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
            u => UserDto.FromDomainModel(u, imageService.GetImageUrl),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin},{RoleNames.User}")]
    [HttpDelete("image/{userId}")]
    public async Task<ActionResult<UserDto>> DeleteImage(
        [FromRoute] Guid userId,
        CancellationToken cancellationToken)
    {
        var input = new DeleteUserImageCommand
        {
            UserId = userId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<UserDto>>(
            u => UserDto.FromDomainModel(u, imageService.GetImageUrl),
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
            Email = user.Email,
            Name = user.Name,
            Surname = user.Surname,
            PhoneNumber = user.PhoneNumber
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<UserDto>>(
            u => UserDto.FromDomainModel(u, imageService.GetImageUrl),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin},{RoleNames.User}")]
    [HttpPut("change-password/{userId:guid}")]
    public async Task<ActionResult<UserDto>> ChangePassword(
        [FromRoute] Guid userId,
        [FromBody] ChangePasswordDto request,
        CancellationToken cancellationToken)
    {
        var input = new ChangePasswordCommand
        {
            UserId = userId,
            CurrentPassword = request.CurrentPassword,
            NewPassword = request.NewPassword
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<UserDto>>(
            u => UserDto.FromDomainModel(u, imageService.GetImageUrl),
            e => e.ToObjectResult());
    }
}