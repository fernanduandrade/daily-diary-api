using System.Reflection;
using DailyDiary.Domain.Diaries;
using DailyDiary.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace DailyDiary.Infrastructure.Persistence.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Diary> Diaries => Set<Diary>(); 

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}