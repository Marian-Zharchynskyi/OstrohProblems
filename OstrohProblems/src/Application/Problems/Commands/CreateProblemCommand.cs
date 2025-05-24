using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Problems.Exceptions;
using Domain.Identity.Users;
using Domain.Problems;
using Domain.Statuses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Problems.Commands;

public record CreateProblemCommand : IRequest<Result<Problem, ProblemException>>
{
    public required string Title { get; init; }
    public required double Latitude { get; init; }
    public required double Longitude { get; init; }
    public required string Description { get; init; }
    public required StatusId StatusId { get; init; }
    public required List<Guid> ProblemCategoryIds { get; init; }
}

public class CreateProblemCommandHandler : IRequestHandler<CreateProblemCommand, Result<Problem, ProblemException>>
{
    private readonly IProblemRepository _problemRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateProblemCommandHandler(
        IProblemRepository problemRepository,
        ICategoryRepository categoryRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _problemRepository = problemRepository;
        _categoryRepository = categoryRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<Problem, ProblemException>> Handle(
        CreateProblemCommand request,
        CancellationToken cancellationToken)
    {
        var existingProblem = await _problemRepository.SearchByTitle(request.Title, cancellationToken);

        return await existingProblem.Match(
            p => Task.FromResult<Result<Problem, ProblemException>>(new ProblemAlreadyExistsException(p.Id)),
            async () => await CreateEntity(
                request.Title,
                request.Latitude,
                request.Longitude,
                request.Description,
                request.StatusId,
                request.ProblemCategoryIds,
                cancellationToken));
    }

    private async Task<Result<Problem, ProblemException>> CreateEntity(
        string title,
        double latitude,
        double longitude,
        string description,
        StatusId statusId,
        List<Guid> categoryIds,
        CancellationToken cancellationToken)
    {
        var problemId = ProblemId.New();

        try
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?
                .Claims.FirstOrDefault(c => c.Type == "id");

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userIdGuid))
            {
                return new UserIdNotFoundException(problemId);
            }

            var userId = new UserId(userIdGuid);

            var problem = Problem.New(
                problemId,
                title,
                latitude,
                longitude,
                description,
                statusId,
                userId
            );

            if (categoryIds.Any())
            {
                var categories = await _categoryRepository.GetByIdsAsync(categoryIds, cancellationToken);
                foreach (var category in categories)
                {
                    problem.AddCategory(category);
                }
            }

            return await _problemRepository.Add(problem, cancellationToken);
        }
        catch (Exception ex)
        {
            return new ProblemUnknownException(problemId, ex);
        }
    }
}
