using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Services.HashPasswordService;
using Application.Users.Exceptions;
using Domain.Identity.Users;
using MediatR;

namespace Application.Users.Commands;

public record SetPasswordCommand : IRequest<Result<User, UserException>>
{
    public required Guid UserId { get; init; }
    public required string NewPassword { get; init; }
}

public class SetPasswordCommandHandler(
    IUserRepository userRepository,
    IHashPasswordService hashPasswordService,
    IClerkApiService clerkApiService)
    : IRequestHandler<SetPasswordCommand, Result<User, UserException>>
{
    public async Task<Result<User, UserException>> Handle(
        SetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var userId = new UserId(request.UserId);
        var existingUser = await userRepository.GetById(userId, cancellationToken);

        return await existingUser.Match(
            async user => await SetPassword(user, request.NewPassword, cancellationToken),
            () => Task.FromResult<Result<User, UserException>>(
                new UserNotFoundException(userId)));
    }

    private async Task<Result<User, UserException>> SetPassword(
        User user,
        string newPassword,
        CancellationToken cancellationToken)
    {
        try
        {
            var newPasswordHash = hashPasswordService.HashPassword(newPassword);
            user.UpdatePassword(newPasswordHash);
            var updatedUser = await userRepository.Update(user, cancellationToken);
            
            // Sync password with Clerk if user has a ClerkId
            if (!string.IsNullOrEmpty(user.ClerkId))
            {
                await clerkApiService.UpdateUserPasswordAsync(user.ClerkId, newPassword);
            }
            
            return updatedUser;
        }
        catch (Exception exception)
        {
            return new UserUnknownException(user.Id, exception);
        }
    }
}
