using API.DTOs.Problems;
using API.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Problems.Commands;
using Domain.Identity.Roles;
using Domain.Identity.Users;
using Domain.PagedResults;
using Domain.Problems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("problems")]
[ApiController]
public class ProblemsController(ISender sender, IProblemQueries problemQueries) : ControllerBase
{
    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}")]
    [HttpGet("paged")]
    public async Task<ActionResult<PagedResult<ProblemDto>>> GetPaged(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var (items, totalCount) = await problemQueries.GetPaged(page, pageSize, cancellationToken);

        var dtoItems = items.Select(ProblemDto.FromDomainModel).ToList();

        return new PagedResult<ProblemDto>(
            Items: dtoItems,
            TotalCount: totalCount,
            Page: page,
            PageSize: pageSize
        );
    }
    
    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}, {RoleNames.Coordinator}")]
    [HttpGet("get-all")]
    public async Task<ActionResult<IReadOnlyList<ProblemDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await problemQueries.GetAll(cancellationToken);
        return entities.Select(ProblemDto.FromDomainModel).ToList();
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}")]
    [HttpGet("by-user/{userId:guid}")]
    public async Task<ActionResult<IReadOnlyList<ProblemDto>>> GetByUser(
        [FromRoute] Guid userId,
        CancellationToken cancellationToken)
    {
        var entities = await problemQueries.GetByUserId(new UserId(userId), cancellationToken);
        return entities.Select(ProblemDto.FromDomainModel).ToList();
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.Coordinator}")]
    [HttpGet("by-coordinator/{coordinatorId:guid}")]
    public async Task<ActionResult<IReadOnlyList<ProblemDto>>> GetByCoordinator(
        [FromRoute] Guid coordinatorId,
        CancellationToken cancellationToken)
    {
        var entities = await problemQueries.GetByCoordinatorId(new UserId(coordinatorId), cancellationToken);
        return entities.Select(ProblemDto.FromDomainModel).ToList();
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}, {RoleNames.Coordinator}")]
    [HttpGet("get-by-id/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> Get([FromRoute] Guid problemId,
        CancellationToken cancellationToken)
    {
        var entity = await problemQueries.GetById(new ProblemId(problemId), cancellationToken);

        return entity.Match<ActionResult<ProblemDto>>(
            problem => ProblemDto.FromDomainModel(problem),
            () => NotFound());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}")]
    [HttpPost("create")]
    public async Task<ActionResult<CreateProblemDto>> Create(
        [FromBody] CreateProblemDto request,
        CancellationToken cancellationToken)
    {
        var input = new CreateProblemCommand
        {
            Title = request.Title,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Description = request.Description,
            ProblemCategoryIds = request.ProblemCategoryIds
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<CreateProblemDto>>(
            problem => CreateProblemDto.FromDomainModel(problem),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}")]
    [HttpPut("update/{problemId:guid}")]
    public async Task<ActionResult<CreateProblemDto>> Update(
        [FromRoute] Guid problemId,
        [FromBody] CreateProblemDto request,
        CancellationToken cancellationToken)
    {
        var input = new UpdateProblemCommand
        {
            Id = problemId,
            Title = request.Title,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Description = request.Description,
            ProblemCategoryIds = request.ProblemCategoryIds 
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<CreateProblemDto>>(
            problem => CreateProblemDto.FromDomainModel(problem),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}")]
    [HttpDelete("delete/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> Delete(
        [FromRoute] Guid problemId, 
        CancellationToken cancellationToken)
    {
        var input = new DeleteProblemCommand
        {
            ProblemId = problemId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            problem => ProblemDto.FromDomainModel(problem),
            e => e.ToObjectResult());
    }
    
    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}")]
    [HttpPut("upload-images/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> Upload([FromRoute] Guid problemId, IFormFileCollection imagesFiles,
        CancellationToken cancellationToken)
    {
        var input = new UploadProblemImagesCommand()
        {
            ProblemId = problemId,
            ImagesFiles = imagesFiles
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            r => ProblemDto.FromDomainModel(r),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}")]
    [HttpPut("delete-image/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> Upload([FromRoute] Guid problemId, Guid problemImageId,
        CancellationToken cancellationToken)
    {
        var input = new DeleteProblemImageCommand()
        {
            ProblemId = problemId,
            ProblemImageId = problemImageId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            r => ProblemDto.FromDomainModel(r),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.Coordinator}")]
    [HttpPut("assign-coordinator/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> AssignCoordinator(
        [FromRoute] Guid problemId,
        [FromBody] Guid coordinatorId,
        CancellationToken cancellationToken)
    {
        var input = new AssignCoordinatorCommand
        {
            ProblemId = problemId,
            CoordinatorId = coordinatorId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            problem => ProblemDto.FromDomainModel(problem),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.Coordinator}")]
    [HttpPut("reject/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> Reject(
        [FromRoute] Guid problemId,
        [FromBody] string rejectionReason,
        CancellationToken cancellationToken)
    {
        var input = new RejectProblemCommand
        {
            ProblemId = problemId,
            RejectionReason = rejectionReason
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            problem => ProblemDto.FromDomainModel(problem),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.Coordinator}")]
    [HttpPut("set-coordinator-comment/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> SetCoordinatorComment(
        [FromRoute] Guid problemId,
        [FromBody] string comment,
        CancellationToken cancellationToken)
    {
        var input = new SetCoordinatorCommentCommand
        {
            ProblemId = problemId,
            Comment = comment
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            problem => ProblemDto.FromDomainModel(problem),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.Coordinator}")]
    [HttpPut("update-current-state/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> UpdateCurrentState(
        [FromRoute] Guid problemId,
        [FromBody] UpdateCurrentStateDto request,
        CancellationToken cancellationToken)
    {
        var input = new UpdateCurrentStateCommand
        {
            ProblemId = problemId,
            CurrentState = request.CurrentState
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            problem => ProblemDto.FromDomainModel(problem),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.Coordinator}")]
    [HttpPut("update-status/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> UpdateStatus(
        [FromRoute] Guid problemId,
        [FromBody] string status,
        CancellationToken cancellationToken)
    {
        var input = new UpdateProblemStatusCommand
        {
            ProblemId = problemId,
            Status = status
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            problem => ProblemDto.FromDomainModel(problem),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.Coordinator}")]
    [HttpPut("start/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> StartProblem(
        [FromRoute] Guid problemId,
        [FromBody] string? currentState,
        CancellationToken cancellationToken)
    {
        var input = new StartProblemCommand
        {
            ProblemId = problemId,
            CurrentState = currentState
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            problem => ProblemDto.FromDomainModel(problem),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.Coordinator}")]
    [HttpPut("complete/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> CompleteProblem(
        [FromRoute] Guid problemId,
        [FromBody] string currentState,
        CancellationToken cancellationToken)
    {
        var input = new CompleteProblemCommand
        {
            ProblemId = problemId,
            CurrentState = currentState
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            problem => ProblemDto.FromDomainModel(problem),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.Coordinator}")]
    [HttpPut("restore/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> RestoreProblem(
        [FromRoute] Guid problemId,
        CancellationToken cancellationToken)
    {
        var input = new RestoreProblemCommand
        {
            ProblemId = problemId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            problem => ProblemDto.FromDomainModel(problem),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.Coordinator}")]
    [HttpGet("by-status/{status}")]
    public async Task<ActionResult<IReadOnlyList<ProblemDto>>> GetByStatus(
        [FromRoute] string status,
        CancellationToken cancellationToken)
    {
        var problemStatus = ProblemStatus.From(status);
        var entities = await problemQueries.GetByStatus(problemStatus, cancellationToken);
        return entities.Select(ProblemDto.FromDomainModel).ToList();
    }
}