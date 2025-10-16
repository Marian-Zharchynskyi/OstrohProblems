using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Services.ImageService;
using Application.Users.Exceptions;
using Domain.Identity.Users;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Users.Commands;

public record UploadUserImageCommand : IRequest<Result<User, UserException>>
{
    public Guid UserId { get; init; }
    public IFormFile ImageFile { get; init; }
}

public class UploadUserImageCommandHandler(
    IUserRepository userRepository,
    IImageService imageService)
    : IRequestHandler<UploadUserImageCommand, Result<User, UserException>>
{
    public async Task<Result<User, UserException>> Handle(UploadUserImageCommand request,
        CancellationToken cancellationToken)
    {
        var userId = new UserId(request.UserId);
        var existingUser = await userRepository.GetById(userId, cancellationToken);

        return await existingUser.Match<Task<Result<User, UserException>>>(
            async user => await UploadOrReplaceImage(user, request.ImageFile, cancellationToken),
            () => Task.FromResult<Result<User, UserException>>(
                new UserNotFoundException(userId)));
    }

    private async Task<Result<User, UserException>> UploadOrReplaceImage(
        User user,
        IFormFile imageFile,
        CancellationToken cancellationToken)
    {
        var imageSaveResult = await imageService.SaveImageFromFileAsync(
            ImagePaths.UserImagePath, imageFile, user.UserImage?.FilePath);

        return await imageSaveResult.Match<Task<Result<User, UserException>>>(
            async imageName =>
            {
                var imageEntity = UserImage.New(UserImageId.New(), user.Id, imageName);
                user.UpdateUserImage(imageEntity);

                var updatedUser = await userRepository.Update(user, cancellationToken);
                return updatedUser;
            },
            () => Task.FromResult<Result<User, UserException>>(new ImageSaveException(user.Id)));
    }
}