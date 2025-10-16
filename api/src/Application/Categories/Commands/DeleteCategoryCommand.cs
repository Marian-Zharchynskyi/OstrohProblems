using Application.Categories.Exceptions;
using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Application.Categories.Commands;

public record DeleteCategoryCommand : IRequest<Result<Category, CategoryException>>
{
    public required Guid ProblemCategoryId { get; init; }
}

public class DeleteProblemCategoryCommandHandler(
    ICategoryRepository categoryRepository)
    : IRequestHandler<DeleteCategoryCommand, Result<Category, CategoryException>>
{
    public async Task<Result<Category, CategoryException>> Handle(
        DeleteCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var problemCategoryId = new CategoryId(request.ProblemCategoryId);
        var existingProblemCategory = await categoryRepository.GetById(problemCategoryId, cancellationToken);

        return await existingProblemCategory.Match<Task<Result<Category, CategoryException>>>(
            async problemCategory => await DeleteEntity(problemCategory, cancellationToken),
            () => Task.FromResult<Result<Category, CategoryException>>(
                new CategoryNotFoundException(problemCategoryId)));
    }

    private async Task<Result<Category, CategoryException>> DeleteEntity(
        Category category,
        CancellationToken cancellationToken)
    {
        try
        {
            return await categoryRepository.Delete(category, cancellationToken);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23503")
        {
            return new CategoryHasRelatedException(category.Id);
        }
        catch (Exception exception)
        {
            return new CategoryUnknownException(category.Id, exception);
        }
    }
}
