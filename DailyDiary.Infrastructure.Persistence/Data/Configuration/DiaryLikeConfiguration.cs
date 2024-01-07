using DailyDiary.Domain.DiaryLikes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyDiary.Infrastructure.Persistence.Data.Configuration;

public class LikeConfiguration : IEntityTypeConfiguration<DiaryLike>
{
    public void Configure(EntityTypeBuilder<DiaryLike> builder)
    {
        builder.ToTable("diary_likes");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");
        builder.Property(x => x.DiaryId).HasColumnName("diary_id");
    }
}