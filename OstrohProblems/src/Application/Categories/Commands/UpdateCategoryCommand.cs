using Application.Categories.Exceptions;
using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Categories;
using MediatR;

namespace Application.Categories.Commands;

public record UpdateCategoryCommand : IRequest<Result<Category, CategoryException>>
{
    public required Guid ProblemCategoryId { get; init; }
    public required string Name { get; init; }
}

public class UpdateProblemCategoryCommandHandler(
    ICategoryRepository categoryRepository)
    : IRequestHandler<UpdateCategoryCommand, Result<Category, CategoryException>>
{
    public async Task<Result<Category, CategoryException>> Handle(
        UpdateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var problemCategoryId = new CategoryId(request.ProblemCategoryId);
        var existingProblemCategory = await categoryRepository.GetById(problemCategoryId, cancellationToken);

        return await existingProblemCategory.Match<Task<Result<Category, CategoryException>>>(
            async problemCategory => await UpdateProblemCategory(problemCategory, request.Name, cancellationToken),
            () => Task.FromResult<Result<Category, CategoryException>>(
                new CategoryNotFoundException(problemCategoryId))
        );
    }

    private async Task<Result<Category, CategoryException>> UpdateProblemCategory(
        Category category,
        string name,
        CancellationToken cancellationToken)
    {
        try
        {
            category.UpdateName(name);
            return await categoryRepository.Update(category, cancellationToken);
        }
        catch (Exception exception)
        {
            return new CategoryUnknownException(category.Id, exception);
        }
    }
}