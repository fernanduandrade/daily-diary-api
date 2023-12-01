using DailyDiary.Application.Common.Interfaces;
using DailyDiary.Infrastructure.Persistence.Data;

namespace DailyDiary.Infrastructure.Persistence.Common;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _context;
    
    public UnitOfWork(AppDbContext context)
        => (_context) = (context);

    public async Task<bool> Commit(CancellationToken cancellationToken)
     => await _context.SaveChangesAsync(cancellationToken) > 0;

    public void Dispose()
    {
        _context.Dispose();
    }
}