using Application.Common;
using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using Application.Problems.Exceptions;
using Application.Services.SignalRService;
using Domain.Problems;
using MediatR;

namespace Application.Problems.Commands;

public record UpdateProblemCommand : IRequest<Result<Problem, ProblemException>>
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required double Latitude { get; init; }
    public required double Longitude { get; init; }
    public required string Description { get; init; }
    public List<Guid>? ProblemCategoryIds { get; init; } 
}

public class UpdateProblemCommandHandler(
    IProblemRepository problemRepository,
    ICategoryRepository categoryRepository,
    ISignalRService signalRService) 
    : IRequestHandler<UpdateProblemCommand, Result<Problem, ProblemException>>
{
    public async Task<Result<Problem, ProblemException>> Handle(
        UpdateProblemCommand request,
        CancellationToken cancellationToken)
    {
        var problemId = new ProblemId(request.Id);
        var existingProblem = await problemRepository.GetById(problemId, cancellationToken);

        return await existingProblem.Match<Task<Result<Problem, ProblemException>>>(
            async problem => await UpdateEntity(
                problem,
                request.Title,
                request.Latitude,
                request.Longitude,
                request.Description,
                request.ProblemCategoryIds, 
                cancellationToken),
            () => Task.FromResult<Result<Problem, ProblemException>>(
                new ProblemNotFoundException(problemId))
        );
    }

    private async Task<Result<Problem, ProblemException>> UpdateEntity(
        Problem problem,
        string title,
        double latitude,
        double longitude,
        string description,
        List<Guid>? categoryIds,
        CancellationToken cancellationToken)
    {
        try
        {
            var existingWithTitle = await problemRepository.SearchByTitle(title, cancellationToken);
            var titleConflict = existingWithTitle.Match(
                some => some.Id.Value != problem.Id.Value,
                () => false);
            if (titleConflict)
            {
                return new ProblemWithTitleAlreadyExistsException(problem.Id, title);
            }

            problem.UpdateProblem(title, latitude, longitude, description);

            if (categoryIds is not null && categoryIds.Any())
            {
                var existingCategoryIds = problem.Categories.Select(c => c.Id.Value).ToHashSet();
                var newCategoryIds = categoryIds.Except(existingCategoryIds).ToList();

                if (newCategoryIds.Any())
                {
                    var newCategories = await categoryRepository.GetByIdsAsync(newCategoryIds, cancellationToken);
                    foreach (var category in newCategories)
                    {
                        problem.AddCategory(category);
                    }
                }
            }

            return await problemRepository.Update(problem, cancellationToken);
        }
        catch (Exception exception)
        {
            return new ProblemUnknownException(problem.Id, exception);
        }
    }
}
