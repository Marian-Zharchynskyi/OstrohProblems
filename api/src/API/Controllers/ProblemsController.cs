using API.DTOs.Problems;
using API.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Problems.Commands;
using Application.Services.ImageService;
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
public class ProblemsController(ISender sender, IProblemQueries problemQueries, IImageService imageService)
    : ControllerBase
{
    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}")]
    [HttpGet("paged")]
    public async Task<ActionResult<PagedResult<ProblemDto>>> GetPaged(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var (items, totalCount) = await problemQueries.GetPaged(page, pageSize, cancellationToken);

        var dtoItems = items.Select(p => ProblemDto.FromDomainModel(p, imageService.GetImageUrl)).ToList();

        return new PagedResult<ProblemDto>(
            Items: dtoItems,
            TotalCount: totalCount,
            Page: page,
            PageSize: pageSize
        );
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}, {RoleNames.Coordinator}")]
    [HttpGet("get-all")]
    public async Task<ActionResult<IReadOnlyList<ProblemSummaryDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await problemQueries.GetAll(cancellationToken);
        return entities.Select(e => ProblemSummaryDto.FromDomainModel(e, imageService.GetImageUrl)).ToList();
    }

    [AllowAnonymous]
    [HttpGet("for-map")]
    public async Task<ActionResult<IReadOnlyList<ProblemSummaryDto>>> GetForMap(CancellationToken cancellationToken)
    {
        var entities = await problemQueries.GetForMap(cancellationToken);
        return entities.Select(e => ProblemSummaryDto.FromDomainModel(e, imageService.GetImageUrl)).ToList();
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}")]
    [HttpGet("by-user/{userId:guid}")]
    public async Task<ActionResult<IReadOnlyList<ProblemDto>>> GetByUser(
        [FromRoute] Guid userId,
        CancellationToken cancellationToken)
    {
        var entities = await problemQueries.GetByUserId(new UserId(userId), cancellationToken);
        return entities.Select(p => ProblemDto.FromDomainModel(p, imageService.GetImageUrl)).ToList();
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}")]
    [HttpGet("by-user-filtered/{userId:guid}")]
    public async Task<ActionResult<IReadOnlyList<ProblemDto>>> GetByUserFiltered(
        [FromRoute] Guid userId,
        [FromQuery] UserProblemsFilterDto filter,
        CancellationToken cancellationToken)
    {
        var entities = await problemQueries.GetByUserIdFiltered(
            new UserId(userId),
            filter.SearchTerm,
            filter.Status,
            filter.Category,
            filter.Priority,
            filter.SortBy,
            filter.SortDescending,
            filter.DateFilter,
            cancellationToken);
        return entities.Select(p => ProblemDto.FromDomainModel(p, imageService.GetImageUrl)).ToList();
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.Coordinator}")]
    [HttpGet("by-coordinator/{coordinatorId:guid}")]
    public async Task<ActionResult<IReadOnlyList<ProblemDto>>> GetByCoordinator(
        [FromRoute] Guid coordinatorId,
        CancellationToken cancellationToken)
    {
        var entities = await problemQueries.GetByCoordinatorId(new UserId(coordinatorId), cancellationToken);
        return entities.Select(p => ProblemDto.FromDomainModel(p, imageService.GetImageUrl)).ToList();
    }

    [AllowAnonymous]
    [HttpGet("get-by-id/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> Get([FromRoute] Guid problemId,
        CancellationToken cancellationToken)
    {
        var entity = await problemQueries.GetById(new ProblemId(problemId), cancellationToken);

        return entity.Match<ActionResult<ProblemDto>>(
            problem => ProblemDto.FromDomainModel(problem, imageService.GetImageUrl),
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
            CategoryNames = request.CategoryNames,
            Priority = request.Priority
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
            CategoryNames = request.CategoryNames
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
            problem => ProblemDto.FromDomainModel(problem, imageService.GetImageUrl),
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
            r => ProblemDto.FromDomainModel(r, imageService.GetImageUrl),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}")]
    [HttpPut("delete-image/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> DeleteImage([FromRoute] Guid problemId, Guid problemImageId,
        CancellationToken cancellationToken)
    {
        var input = new DeleteProblemImageCommand()
        {
            ProblemId = problemId,
            ProblemImageId = problemImageId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            r => ProblemDto.FromDomainModel(r, imageService.GetImageUrl),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.Coordinator}")]
    [HttpPut("assign-coordinator/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> AssignCoordinator(
        [FromRoute] Guid problemId,
        [FromBody] AssignCoordinatorDto request,
        CancellationToken cancellationToken)
    {
        var input = new AssignCoordinatorCommand
        {
            ProblemId = problemId,
            CoordinatorId = request.CoordinatorId,
            Priority = request.Priority
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            problem => ProblemDto.FromDomainModel(problem, imageService.GetImageUrl),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.Coordinator}")]
    [HttpPut("reject/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> Reject(
        [FromRoute] Guid problemId,
        [FromBody] RejectProblemDto request,
        CancellationToken cancellationToken)
    {
        var input = new RejectProblemCommand
        {
            ProblemId = problemId,
            CoordinatorId = request.CoordinatorId,
            RejectionReason = request.RejectionReason
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            problem => ProblemDto.FromDomainModel(problem, imageService.GetImageUrl),
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
            problem => ProblemDto.FromDomainModel(problem, imageService.GetImageUrl),
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
            problem => ProblemDto.FromDomainModel(problem, imageService.GetImageUrl),
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
            problem => ProblemDto.FromDomainModel(problem, imageService.GetImageUrl),
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
            problem => ProblemDto.FromDomainModel(problem, imageService.GetImageUrl),
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
            problem => ProblemDto.FromDomainModel(problem, imageService.GetImageUrl),
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
            problem => ProblemDto.FromDomainModel(problem, imageService.GetImageUrl),
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
        return entities.Select(p => ProblemDto.FromDomainModel(p, imageService.GetImageUrl)).ToList();
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.Coordinator}")]
    [HttpPut("upload-coordinator-images/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> UploadCoordinatorImages(
        [FromRoute] Guid problemId,
        IFormFileCollection imagesFiles,
        CancellationToken cancellationToken)
    {
        var input = new UploadCoordinatorImagesCommand
        {
            ProblemId = problemId,
            ImagesFiles = imagesFiles
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            r => ProblemDto.FromDomainModel(r, imageService.GetImageUrl),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.Coordinator}")]
    [HttpPut("delete-coordinator-image/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> DeleteCoordinatorImage(
        [FromRoute] Guid problemId,
        [FromBody] DeleteCoordinatorImageDto request,
        CancellationToken cancellationToken)
    {
        var input = new DeleteCoordinatorImageCommand
        {
            ProblemId = problemId,
            CoordinatorImageId = request.CoordinatorImageId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            r => ProblemDto.FromDomainModel(r, imageService.GetImageUrl),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}")]
    [HttpPut("update-description/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> UpdateDescription(
        [FromRoute] Guid problemId,
        [FromBody] UpdateProblemDescriptionDto request,
        CancellationToken cancellationToken)
    {
        var input = new UpdateProblemDescriptionCommand
        {
            ProblemId = problemId,
            Description = request.Description
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            problem => ProblemDto.FromDomainModel(problem, imageService.GetImageUrl),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}")]
    [HttpPut("update-title-and-categories/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> UpdateTitleAndCategories(
        [FromRoute] Guid problemId,
        [FromBody] UpdateProblemTitleAndCategoriesDto request,
        CancellationToken cancellationToken)
    {
        var input = new UpdateProblemTitleAndCategoriesCommand
        {
            ProblemId = problemId,
            Title = request.Title,
            CategoryNames = request.CategoryNames
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            problem => ProblemDto.FromDomainModel(problem, imageService.GetImageUrl),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.Coordinator}")]
    [HttpPut("update-location/{problemId:guid}")]
    public async Task<ActionResult<ProblemDto>> UpdateLocation(
        [FromRoute] Guid problemId,
        [FromBody] UpdateProblemLocationDto request,
        CancellationToken cancellationToken)
    {
        var input = new UpdateProblemLocationCommand
        {
            ProblemId = problemId,
            Latitude = request.Latitude,
            Longitude = request.Longitude
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ProblemDto>>(
            problem => ProblemDto.FromDomainModel(problem, imageService.GetImageUrl),
            e => e.ToObjectResult());
    }
}