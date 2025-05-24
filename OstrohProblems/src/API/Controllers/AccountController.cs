using API.DTOs.Identity;
using API.Modules.Errors;
using Application.Identity.Commands;
using Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("account")]
[ApiController]
public class AccountController(ISender sender) : ControllerBase
{
    [HttpPost("signup")]
    public async Task<ActionResult<JwtVm>> SignUpAsync(
        [FromBody] SignUpDto request,
        CancellationToken cancellationToken)
    {
        var input = new SignUpCommand
        {
            Email = request.Email,
            Password = request.Password,
            Name = request.Name,
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<JwtVm>>(
            f => f,
            e => e.ToObjectResult());
    }

    [HttpPost("signin")]
    public async Task<ActionResult<JwtVm>> SignInAsync(
        [FromBody] SignInDto request,
        CancellationToken cancellationToken)
    {
        var input = new SignInCommand
        {
            Email = request.Email,
            Password = request.Password
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<JwtVm>>(
            f => f,
            e => e.ToObjectResult());
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<JwtVm>> RefreshTokensAsync([FromBody] JwtVm model,
        CancellationToken cancellationToken)
    {
        var input = new RefreshTokenCommand()
        {
            AccessToken = model.AccessToken,
            RefreshToken = model.RefreshToken
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<JwtVm>>(
            f => f,
            e => e.ToObjectResult());
    }
}