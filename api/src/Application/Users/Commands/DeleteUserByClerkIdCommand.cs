using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Services.ImageService;
using Application.Users.Exceptions;
using Domain.Identity.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands;

public record DeleteUserByClerkIdCommand : IRequest<Result<User, UserException>>
{
    public required string ClerkUserId { get; init; }
}

public class DeleteUserByClerkIdCommandHandler(
    IUserRepository userRepository,
    IProblemRepository problemRepository,
    ICommentRepository commentRepository,
    IRatingRepository ratingRepository,
    IImageService imageService)
    : IRequestHandler<DeleteUserByClerkIdCommand, Result<User, UserException>>
{
    public async Task<Result<User, UserException>> Handle(
        DeleteUserByClerkIdCommand request,
        CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.SearchByClerkId(request.ClerkUserId, cancellationToken);

        return await existingUser.Match<Task<Result<User, UserException>>>(
            async user =>
            {
                if (user == null) throw new ArgumentNullException(nameof(user));
                return await DeleteEntity(user, cancellationToken);
            },
            () => Task.FromResult<Result<User, UserException>>
                (new UserNotFoundException(UserId.Empty)));
    }

    private async Task<Result<User, UserException>> DeleteEntity(
        User user,
        CancellationToken cancellationToken)
    {
        await DeleteImageByUser(user);
        
        try
        {
            await DeleteUserProblems(user.Id, cancellationToken);
            await DeleteUserComments(user.Id, cancellationToken);
            await DeleteUserRatings(user.Id, cancellationToken);
            
            return await userRepository.Delete(user, cancellationToken);
        }
        catch (DbUpdateException dbEx)
        {
            return new UserUnknownException(user.Id, new Exception(
                "Не вдалося видалити користувача через зв'язки з іншими сутностями. " +
                "Можливо, користувач призначений координатором до проблем.", dbEx));
        }
        catch (Exception exception)
        {
            return new UserUnknownException(user.Id, exception);
        }
    }

    private async Task DeleteUserProblems(UserId userId, CancellationToken cancellationToken)
    {
        var userProblems = await problemRepository.GetByCreatedBy(userId, cancellationToken);
        
        foreach (var problem in userProblems)
        {
            await problemRepository.Delete(problem, cancellationToken);
        }
    }

    private async Task DeleteUserComments(UserId userId, CancellationToken cancellationToken)
    {
        var userComments = await commentRepository.GetByCreatedBy(userId, cancellationToken);
        
        foreach (var comment in userComments)
        {
            await commentRepository.Delete(comment, cancellationToken);
        }
    }

    private async Task DeleteUserRatings(UserId userId, CancellationToken cancellationToken)
    {
        var userRatings = await ratingRepository.GetByCreatedBy(userId, cancellationToken);
        
        foreach (var rating in userRatings)
        {
            await ratingRepository.Delete(rating, cancellationToken);
        }
    }

    private async Task DeleteImageByUser(User user)
    {
        var userImage = user.UserImage?.FilePath;
        if (!string.IsNullOrEmpty(userImage))
        {
            await imageService.DeleteImageAsync(userImage);
        }
    }
}
