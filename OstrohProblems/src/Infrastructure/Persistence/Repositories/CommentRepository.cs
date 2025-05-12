using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Comments;
using Domain.Problems;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class CommentRepository : ICommentQueries, ICommentRepository
{
    private readonly ApplicationDbContext _context;

    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Comment>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Comments
            .Include(x => x.User)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<Comment>> GetById(CommentId id, CancellationToken cancellationToken)
    {
        var entity = await _context.Comments
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<Comment>() : Option.Some(entity);
    }

    public async Task<IReadOnlyList<Comment>> GetAllByProblemId(ProblemId problemId, CancellationToken cancellationToken)
    {
        return await _context.Comments
            .Where(c => c.ProblemId == problemId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Comment> Add(Comment comment, CancellationToken cancellationToken)
    {
        await _context.Comments.AddAsync(comment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return comment;
    }

    public async Task<Comment> Update(Comment comment, CancellationToken cancellationToken)
    {
        _context.Comments.Update(comment);
        await _context.SaveChangesAsync(cancellationToken);
        return comment;
    }

    public async Task<Comment> Delete(Comment comment, CancellationToken cancellationToken)
    {
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync(cancellationToken);
        return comment;
    }
}