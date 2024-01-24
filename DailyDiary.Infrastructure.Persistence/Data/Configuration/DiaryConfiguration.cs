using DailyDiary.Domain.Diaries;
using DailyDiary.Domain.LikesCounter;
using DailyDiary.Domain.UserLikes;
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
        builder.Property(x => x.UserId).HasColumnName("user_id");
        
        builder
            .HasOne<UserLike>(x => x.UserLike)
            .WithOne(x => x.Diary)
            .HasForeignKey<UserLike>(q => q.DiaryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne<LikeCounter>(x => x.LikeCounter)
            .WithOne(x => x.Diary)
            .HasForeignKey<LikeCounter>(x => x.DiaryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}