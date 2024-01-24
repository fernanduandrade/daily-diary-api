using DailyDiary.Domain.LikesCounter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyDiary.Infrastructure.Persistence.Data.Configuration;

public class LikeCounterConfiguration :IEntityTypeConfiguration<LikeCounter>
{
    public void Configure(EntityTypeBuilder<LikeCounter> builder)
    {
        builder.ToTable("diaries_likes_counter");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");
        builder.Property(x => x.Counter).HasColumnName("counter");
        builder.Property(x => x.DiaryId).HasColumnName("diary_id");
    }
}