using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Comments;
using Domain.Problems;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class CommentRepository(ApplicationDbContext context) : ICommentQueries, ICommentRepository
{
    public async Task<(IReadOnlyList<Comment> Items, int TotalCount)> GetPaged(int page, int pageSize,
        CancellationToken cancellationToken)
    {
        var query = context.Comments
            .Include(x => x.User)
            .Include(x => x.Problem)
            .AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }
    
    public async Task<IReadOnlyList<Comment>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Comments
            .Include(x => x.User)
            .Include(x => x.Problem)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<Comment>> GetById(CommentId id, CancellationToken cancellationToken)
    {
        var entity = await context.Comments
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<Comment>() : Option.Some(entity);
    }

    public async Task<IReadOnlyList<Comment>> GetAllByProblemId(ProblemId problemId, CancellationToken cancellationToken)
    {
        return await context.Comments
            .Where(c => c.ProblemId == problemId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Comment> Add(Comment comment, CancellationToken cancellationToken)
    {
        await context.Comments.AddAsync(comment, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return comment;
    }

    public async Task<Comment> Update(Comment comment, CancellationToken cancellationToken)
    {
        context.Comments.Update(comment);
        await context.SaveChangesAsync(cancellationToken);
        return comment;
    }

    public async Task<Comment> Delete(Comment comment, CancellationToken cancellationToken)
    {
        context.Comments.Remove(comment);
        await context.SaveChangesAsync(cancellationToken);
        return comment;
    }
}