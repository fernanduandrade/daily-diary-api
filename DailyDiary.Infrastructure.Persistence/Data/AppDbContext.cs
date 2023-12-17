using System.Reflection;
using DailyDiary.Domain.Diaries;
using DailyDiary.Domain.Likes;
using DailyDiary.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace DailyDiary.Infrastructure.Persistence.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Diary> Diaries => Set<Diary>();
    public DbSet<Like> Likes => Set<Like>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}