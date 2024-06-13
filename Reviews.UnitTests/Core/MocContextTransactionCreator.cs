using Reviews.Application.Abstractions.Persistence;
using Reviews.Persistence;

namespace Reviews.UnitTests.Core;

public class MocContextTransactionCreator : IContextTransactionCreator
{
    private readonly ApplicationDbContext _dbContext;

    public MocContextTransactionCreator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return new MocContextTransaction(await _dbContext.Database.BeginTransactionAsync(cancellationToken));
    }
}