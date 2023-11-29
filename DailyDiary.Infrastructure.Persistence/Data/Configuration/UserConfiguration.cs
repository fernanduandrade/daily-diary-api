using DailyDiary.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyDiary.Infrastructure.Persistence.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder.ToTable("users");
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Diaries)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
        
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");
        builder.OwnsOne(x => x.Email)
            .Property(x => x.Value).HasColumnName("email");
        builder.OwnsOne(x => x.Password)
            .Property(x => x.Value).HasColumnName("password");
    }
}