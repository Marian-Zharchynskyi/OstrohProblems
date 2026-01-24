using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Problems.Exceptions;
using Domain.Problems;
using MediatR;

namespace Application.Problems.Commands;

public record UpdateProblemTitleAndCategoriesCommand : IRequest<Result<Problem, ProblemException>>
{
    public required Guid ProblemId { get; init; }
    public required string Title { get; init; }
    public List<string>? CategoryNames { get; init; }
}

public class UpdateProblemTitleAndCategoriesCommandHandler(
    IProblemRepository problemRepository)
    : IRequestHandler<UpdateProblemTitleAndCategoriesCommand, Result<Problem, ProblemException>>
{
    public async Task<Result<Problem, ProblemException>> Handle(
        UpdateProblemTitleAndCategoriesCommand request,
        CancellationToken cancellationToken)
    {
        var problemId = new ProblemId(request.ProblemId);
        var existingProblem = await problemRepository.GetById(problemId, cancellationToken);

        return await existingProblem.Match<Task<Result<Problem, ProblemException>>>(
            async problem =>
            {
                try
                {
                    var existingWithTitle = await problemRepository.SearchByTitle(request.Title, cancellationToken);
                    var titleConflict = existingWithTitle.Match(
                        some => some.Id.Value != problem.Id.Value,
                        () => false);
                    if (titleConflict)
                    {
                        return new ProblemWithTitleAlreadyExistsException(problem.Id, request.Title);
                    }

                    problem.UpdateTitleAndCategories(request.Title, request.CategoryNames);
                    return await problemRepository.Update(problem, cancellationToken);
                }
                catch (UnsupportedCategoryException ex)
                {
                    return new ProblemUnknownException(problem.Id, ex);
                }
                catch (Exception exception)
                {
                    return new ProblemUnknownException(problem.Id, exception);
                }
            },
            () => Task.FromResult<Result<Problem, ProblemException>>(
                new ProblemNotFoundException(problemId))
        );
    }
}
