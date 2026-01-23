using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Services;
using Application.Services.TokenService;
using Application.Users.Exceptions;
using Domain.Identity.Users;
using Domain.ViewModels;
using MediatR;

namespace Application.Users.Commands;

public record UpdateUserCommand : IRequest<Result<User, UserException>>
{
    public required Guid UserId { get; init; }
    public required string Email { get; init; }
    public string? Name { get; init; }
    public string? Surname { get; init; }
    public string? PhoneNumber { get; init; }
}

public class UpdateUserCommandHandle(
    IUserRepository userRepository,
    IClerkApiService clerkApiService)
    : IRequestHandler<UpdateUserCommand, Result<User, UserException>>
{
    public async Task<Result<User, UserException>> Handle(UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        var userId = new UserId(request.UserId);
        var existingUser = await userRepository.GetById(userId, cancellationToken);

        return await existingUser.Match(
            async u =>
            {
                var existingEmail =
                    await userRepository.SearchByEmailForUpdate(userId, request.Email, cancellationToken);

                return await existingEmail.Match(
                    e => Task.FromResult<Result<User, UserException>>(
                        new UserByThisEmailAlreadyExistsException(userId)),
                    async () => await UpdateEntity(u, request.Email, request.Name, request.Surname, request.PhoneNumber, cancellationToken));
            },
            () => Task.FromResult<Result<User, UserException>>(
                new UserNotFoundException(userId)));
    }

    private async Task<Result<User, UserException>> UpdateEntity(
        User user,
        string email,
        string? name,
        string? surname,
        string? phoneNumber,
        CancellationToken cancellationToken)
    {
        try
        {
            // Check if email is being changed and if user has ClerkId
            bool emailChanged = user.Email != email;
            
            // If email is being changed and user has Clerk account, try to update Clerk first
            if (emailChanged && !string.IsNullOrEmpty(user.ClerkId))
            {
                try
                {
                    await clerkApiService.UpdateUserAsync(user.ClerkId, name, surname, email);
                }
                catch (InvalidOperationException ex) when (ex.Message == "OAUTH_EMAIL_CANNOT_BE_CHANGED")
                {
                    // OAuth email cannot be changed - return error without updating DB
                    return new OAuthEmailCannotBeChangedException(user.Id);
                }
            }
            
            // Update user in database
            user.UpdateUser(email, name, surname, phoneNumber);
            var updatedUser = await userRepository.Update(user, cancellationToken);
            
            // If email wasn't changed but other fields were, still sync with Clerk
            if (!emailChanged && !string.IsNullOrEmpty(user.ClerkId))
            {
                await clerkApiService.UpdateUserAsync(user.ClerkId, name, surname, null);
            }
            
            return updatedUser;
        }
        catch (Exception exception)
        {
            return new UserUnknownException(user.Id, exception);
        }
    }
}
