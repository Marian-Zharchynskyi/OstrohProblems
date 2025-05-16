using API.DTOs.Categories;
using API.Modules.Errors;
using Application.Categories.Commands;
using Application.Common.Interfaces.Queries;
using Domain.Categories;
using Domain.PagedResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("problem-categories")]
[ApiController]
public class CategoriesController(ISender sender, ICategoryQueries categoryQueries) : ControllerBase
{
    [HttpGet("paged")]
    public async Task<ActionResult<PagedResult<CategoryDto>>> GetPaged(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var (items, totalCount) = await categoryQueries.GetPaged(page, pageSize, cancellationToken);

        var dtoItems = items.Select(CategoryDto.FromDomainModel).ToList();

        return new PagedResult<CategoryDto>(
            Items: dtoItems,
            TotalCount: totalCount,
            Page: page,
            PageSize: pageSize
        );
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await categoryQueries.GetAll(cancellationToken);
        return entities.Select(CategoryDto.FromDomainModel).ToList();
    }

    [HttpGet("get-by-id/{problemCategoryId:guid}")]
    public async Task<ActionResult<CategoryDto>> Get([FromRoute] Guid problemCategoryId,
        CancellationToken cancellationToken)
    {
        var entity = await categoryQueries.GetById(new CategoryId(problemCategoryId), cancellationToken);

        return entity.Match<ActionResult<CategoryDto>>(
            pc => CategoryDto.FromDomainModel(pc),
            () => NotFound());
    }

    [HttpPost("create")]
    public async Task<ActionResult<CategoryDto>> Create(
        [FromBody] CategoryDto request,
        CancellationToken cancellationToken)
    {
        var input = new CreateCategoryCommand
        {
            Name = request.Name,
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<CategoryDto>>(
            pc => CategoryDto.FromDomainModel(pc),
            e => e.ToObjectResult());
    }

    [HttpPut("update")]
    public async Task<ActionResult<CategoryDto>> Update(
        [FromBody] CategoryDto request,
        CancellationToken cancellationToken)
    {
        var input = new UpdateCategoryCommand
        {
            ProblemCategoryId = request.Id!.Value,
            Name = request.Name
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<CategoryDto>>(
            pc => CategoryDto.FromDomainModel(pc),
            e => e.ToObjectResult());
    }

    [HttpDelete("delete/{problemCategoryId:guid}")]
    public async Task<ActionResult<CategoryDto>> Delete(
        [FromRoute] Guid problemCategoryId, CancellationToken cancellationToken)
    {
        var input = new DeleteCategoryCommand
        {
            ProblemCategoryId = problemCategoryId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<CategoryDto>>(
            pc => CategoryDto.FromDomainModel(pc),
            e => e.ToObjectResult());
    }
}