using DailyDiary.Domain.LikesCounter;

namespace DailyDiary.Infrastructure.Persistence.Data.Repositories;

public class LikeCounterRepository : ILikeCounterRepository
{
    private readonly AppDbContext _context;

    public LikeCounterRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public void Create(LikeCounter likeCounter)
    {
        _context.Add(likeCounter);
    }

    public void Increment(Guid diaryLikeId)
    {
        try
        {
            var entity = _context.LikesCounter.FirstOrDefault(x => x.DiaryLikeId == diaryLikeId);
            entity.Increment();
        }
        catch (Exception er)
        {
            Console.WriteLine("aaa");
        }
        
        
    }

    public void Decrement(Guid diaryLikeId)
    {
        var entity = _context.LikesCounter.FirstOrDefault(x => x.DiaryLikeId == diaryLikeId);
        entity.Decrement();
    }
}