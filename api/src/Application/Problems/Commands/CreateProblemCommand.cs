using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Problems.Exceptions;
using Application.Services.SignalRService;
using Domain.Identity.Users;
using Domain.Problems;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Problems.Commands;

public record CreateProblemCommand : IRequest<Result<Problem, ProblemException>>
{
    public required string Title { get; init; }
    public required double Latitude { get; init; }
    public required double Longitude { get; init; }
    public required string Description { get; init; }
    public required List<string> CategoryNames { get; init; }
    public string? Priority { get; init; }
}

public class CreateProblemCommandHandler : IRequestHandler<CreateProblemCommand, Result<Problem, ProblemException>>
{
    private readonly IProblemRepository _problemRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISignalRService _signalRService;

    public CreateProblemCommandHandler(
        IProblemRepository problemRepository,
        IHttpContextAccessor httpContextAccessor,
        ISignalRService signalRService)
    {
        _problemRepository = problemRepository;
        _httpContextAccessor = httpContextAccessor;
        _signalRService = signalRService;
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
                request.CategoryNames,
                request.Priority,
                cancellationToken));
    }

    private async Task<Result<Problem, ProblemException>> CreateEntity(
        string title,
        double latitude,
        double longitude,
        string description,
        List<string> categoryNames,
        string? priority,
        CancellationToken cancellationToken)
    {
        var problemId = ProblemId.New();

        try
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "user_id");

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userIdGuid))
            {
                return new UserIdNotFoundException(problemId);
            }

            var userId = new UserId(userIdGuid);
            var problemPriority = string.IsNullOrEmpty(priority) ? null : Priority.From(priority);

            var problem = Problem.New(
                problemId,
                title,
                latitude,
                longitude,
                description,
                userId,
                problemPriority
            );

            if (categoryNames.Any())
            {
                foreach (var categoryName in categoryNames)
                {
                    problem.AddCategory(categoryName);
                }
            }

            var result = await _problemRepository.Add(problem, cancellationToken);
            
            await _signalRService.SendRefreshToAll(cancellationToken);
            
            return result;
        }
        catch (UnsupportedCategoryException ex)
        {
            return new ProblemUnknownException(problemId, ex);
        }
        catch (Exception ex)
        {
            return new ProblemUnknownException(problemId, ex);
        }
    }
}
