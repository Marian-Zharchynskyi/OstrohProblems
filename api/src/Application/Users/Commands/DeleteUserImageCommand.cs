using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Services.ImageService;
using Application.Users.Exceptions;
using Domain.Identity.Users;
using MediatR;

namespace Application.Users.Commands;

public record DeleteUserImageCommand : IRequest<Result<User, UserException>>
{
    public Guid UserId { get; init; }
}

public class DeleteUserImageCommandHandler(
    IUserRepository userRepository,
    IImageService imageService)
    : IRequestHandler<DeleteUserImageCommand, Result<User, UserException>>
{
    public async Task<Result<User, UserException>> Handle(DeleteUserImageCommand request,
        CancellationToken cancellationToken)
    {
        var userId = new UserId(request.UserId);
        var existingUser = await userRepository.GetById(userId, cancellationToken);

        return await existingUser.Match<Task<Result<User, UserException>>>(
            async user => await DeleteImage(user, cancellationToken),
            () => Task.FromResult<Result<User, UserException>>(
                new UserNotFoundException(userId)));
    }

    private async Task<Result<User, UserException>> DeleteImage(
        User user,
        CancellationToken cancellationToken)
    {
        if (user.UserImage is null)
        {
            return new ImageNotFoundException(user.Id);
        }

        var deleteResult = await imageService.DeleteImageAsync(user.UserImage.FilePath);

        return await deleteResult.Match<Task<Result<User, UserException>>>(
            async _ =>
            {
                user.RemoveUserImage();
                var updatedUser = await userRepository.Update(user, cancellationToken);
                return updatedUser;
            },
            error => Task.FromResult<Result<User, UserException>>(new ImageSaveException(user.Id)));
    }
}
