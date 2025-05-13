using API.DTOs.Ratings;
using API.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Ratings.Commands;
using Domain.Ratings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("ratings")]
[ApiController]
public class RatingsController(
    ISender sender,
    IRatingQueries ratingQueries)
    : ControllerBase
{
    [HttpGet("get-all")]
    public async Task<ActionResult<IReadOnlyList<RatingDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await ratingQueries.GetAll(cancellationToken);
        return entities.Select(RatingDto.FromDomainModel).ToList();
    }

    [HttpGet("get-by-id/{ratingId:guid}")]
    public async Task<ActionResult<RatingDto>> Get([FromRoute] Guid ratingId, CancellationToken cancellationToken)
    {
        var entity = await ratingQueries.GetById(new RatingId(ratingId), cancellationToken);

        return entity.Match<ActionResult<RatingDto>>(
            r => RatingDto.FromDomainModel(r),
            () => NotFound());
    }

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
