using API.DTOs.Ratings;
using API.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Ratings.Commands;
using Domain.Identity.Roles;
using Domain.PagedResults;
using Domain.Ratings;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("ratings")]
[ApiController]
public class RatingsController(ISender sender, IRatingQueries ratingQueries) : ControllerBase
{
    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}")]
    [HttpGet("paged")]
    public async Task<ActionResult<PagedResult<RatingDto>>> GetPaged(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var (items, totalCount) = await ratingQueries.GetPaged(page, pageSize, cancellationToken);

        var dtoItems = items.Select(RatingDto.FromDomainModel).ToList();

        return new PagedResult<RatingDto>(
            Items: dtoItems,
            TotalCount: totalCount,
            Page: page,
            PageSize: pageSize
        );
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}")]
    [HttpGet("get-all")]
    public async Task<ActionResult<IReadOnlyList<RatingDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await ratingQueries.GetAll(cancellationToken);
        return entities.Select(RatingDto.FromDomainModel).ToList();
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}")]
    [HttpGet("get-by-id/{ratingId:guid}")]
    public async Task<ActionResult<RatingDto>> Get([FromRoute] Guid ratingId, CancellationToken cancellationToken)
    {
        var entity = await ratingQueries.GetById(new RatingId(ratingId), cancellationToken);

        return entity.Match<ActionResult<RatingDto>>(
            r => RatingDto.FromDomainModel(r),
            () => NotFound());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}")]
    [HttpPost("create")]
    public async Task<ActionResult<CreateRatingDto>> Create(
        [FromBody] CreateRatingDto request,
        CancellationToken cancellationToken)
    {
        var input = new CreateRatingCommand
        {
            Points = request.Points,
            ProblemId = request.ProblemId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<CreateRatingDto>>(
            r => CreateRatingDto.FromDomainModel(r),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}")]
    [HttpPut("update/{id}")]
    public async Task<ActionResult<CreateRatingDto>> Update(
        [FromRoute] Guid id,
        [FromBody] CreateRatingDto request,
        CancellationToken cancellationToken)
    {
        var input = new UpdateRatingCommand
        {
            RatingId = id,
            Points = request.Points
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<CreateRatingDto>>(
            r => CreateRatingDto.FromDomainModel(r),
            e => e.ToObjectResult());
    }

    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}")]
    [HttpDelete("delete/{ratingId:guid}")]
    public async Task<ActionResult<RatingDto>> Delete(
        [FromRoute] Guid ratingId, CancellationToken cancellationToken)
    {
        var input = new DeleteRatingCommand
        {
            RatingId = ratingId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<RatingDto>>(
            r => RatingDto.FromDomainModel(r),
            e => e.ToObjectResult());
    }
}