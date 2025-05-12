using API.DTOs;
using API.DTOs.Comments;
using API.Modules.Errors;
using Application.Comments.Commands;
using Application.Common.Interfaces.Queries;
using Domain.Comments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("comments")]
[ApiController]
public class CommentsController(
    ISender sender,
    ICommentQueries commentQueries)
    : ControllerBase
{
    [HttpGet("get-all")]
    public async Task<ActionResult<IReadOnlyList<CommentDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await commentQueries.GetAll(cancellationToken);
        return entities.Select(CommentDto.FromDomainModel).ToList();
    }

    [HttpGet("get-by-id/{commentId:guid}")]
    public async Task<ActionResult<CommentDto>> Get([FromRoute] Guid commentId, CancellationToken cancellationToken)
    {
        var entity = await commentQueries.GetById(new CommentId(commentId), cancellationToken);

        return entity.Match<ActionResult<CommentDto>>(
            c => CommentDto.FromDomainModel(c),
            () => NotFound());
    }

    [HttpPost("create")]
    public async Task<ActionResult<CreateCommentDto>> Create(
        [FromBody] CreateCommentDto request,
        CancellationToken cancellationToken)
    {
        var input = new CreateCommentCommand
        {
            Content = request.Content,
            ProblemId = request.ProblemId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<CreateCommentDto>>(
            c => CreateCommentDto.FromDomainModel(c),
            e => e.ToObjectResult());
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult<CreateCommentDto>> Update(
        [FromRoute] Guid id,
        [FromBody] CreateCommentDto request,
        CancellationToken cancellationToken)
    {
        var input = new UpdateCommentCommand
        {
            CommentId = id,
            Content = request.Content
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<CreateCommentDto>>(
            c => CreateCommentDto.FromDomainModel(c),
            e => e.ToObjectResult());
    }

    [HttpDelete("delete/{commentId:guid}")]
    public async Task<ActionResult<CommentDto>> Delete(
        [FromRoute] Guid commentId, CancellationToken cancellationToken)
    {
        var input = new DeleteCommentCommand
        {
            CommentId = commentId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<CommentDto>>(
            c => CommentDto.FromDomainModel(c),
            e => e.ToObjectResult());
    }
}
