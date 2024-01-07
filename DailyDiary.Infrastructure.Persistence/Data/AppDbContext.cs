using System.Reflection;
using DailyDiary.Domain.Common;
using DailyDiary.Domain.Diaries;
using DailyDiary.Domain.DiaryLikes;
using DailyDiary.Domain.LikesCounter;
using DailyDiary.Domain.UserLikes;
using DailyDiary.Domain.Users;
using DailyDiary.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace DailyDiary.Infrastructure.Persistence.Data;

public class AppDbContext : DbContext
{
    private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
    
    public DbSet<User> Users => Set<User>();
    public DbSet<Diary> Diaries => Set<Diary>();
    public DbSet<DiaryLike> DiaryLikes => Set<DiaryLike>();
    public DbSet<LikeCounter> LikesCounter => Set<LikeCounter>();
    public DbSet<UserLike> UserLikes => Set<UserLike>();

    public AppDbContext(DbContextOptions<AppDbContext> options, PublishDomainEventsInterceptor publishDomainEventsInterceptor) : base(options)
    {
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}