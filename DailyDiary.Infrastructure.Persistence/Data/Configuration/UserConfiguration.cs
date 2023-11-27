using DailyDiary.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyDiary.Infrastructure.Persistence.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder.ToTable("users");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Name).HasColumnName("name");

        builder.OwnsOne(x => x.Email)
            .Property(x => x.Value).HasColumnName("email");
        builder.OwnsOne(x => x.Password)
            .Property(x => x.Value).HasColumnName("password");
    }
}