namespace DailyDiary.Application.Common.Interfaces;

public interface IUnitOfWork
{ 
    Task<bool> Commit(CancellationToken cancellationToken);
}