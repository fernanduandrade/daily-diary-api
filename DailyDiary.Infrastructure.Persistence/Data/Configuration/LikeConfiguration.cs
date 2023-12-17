using DailyDiary.Domain.Likes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyDiary.Infrastructure.Persistence.Data.Configuration;

public class LikeConfiguration : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.ToTable("likes");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.DiaryId).HasColumnName("diary_id");
    }
}