using DailyDiary.Domain.Diaries;
using DailyDiary.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyDiary.Infrastructure.Persistence.Data.Configuration;

public class DiaryConfiguration : IEntityTypeConfiguration<Diary>
{
    public void Configure(EntityTypeBuilder<Diary> builder)
    {
        builder.ToTable("diaries");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");
        builder.Property(x => x.Title).HasColumnName("title");
        builder.Property(x => x.Text).HasColumnName("text");
        builder.Property(x => x.IsPublic).HasColumnName("is_public");
        builder.Property(x => x.Mood).HasColumnName("mood");
    }
}