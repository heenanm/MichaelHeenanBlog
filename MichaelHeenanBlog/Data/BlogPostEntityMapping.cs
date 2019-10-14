using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MichaelHeenanBlog.Data
{
    public class BlogPostEntityMapping: IEntityTypeConfiguration<BlogPostEntity>
    {
            public void Configure(EntityTypeBuilder<BlogPostEntity> builder)
            {
                //builder.ToTable("BlogPosts");
                //builder.HasKey(x => x.Id);
                //builder.Property(x => x.RowVersion)
                //    .IsRowVersion();

                //builder.HasOne(x => x.Tags).WithMany(x => x.);

                //builder.HasMany(x => x.Messages).WithOne(x => x.Conversation).OnDelete(DeleteBehavior.Restrict);
                //builder.HasMany(x => x.Users).WithOne(x => x.Conversation).OnDelete(DeleteBehavior.Restrict);

                //builder.HasIndex(x => x.CreatedAt);
                //builder.HasIndex(x => x.SuspendedAt);

                //builder.Property(x => x.RecentMessage_CreatedAt)
                //    .HasConversion(x => x, x => x == null ? x : DateTime.SpecifyKind(x.Value, DateTimeKind.Utc).ToLocalTime());
                //builder.Property(x => x.SuspendedAt)
                //    .HasConversion(x => x, x => x == null ? x : DateTime.SpecifyKind(x.Value, DateTimeKind.Utc).ToLocalTime());

                //builder.Property(x => x.CreatedAt)
                //    .HasConversion(x => x, x => DateTime.SpecifyKind(x, DateTimeKind.Utc).ToLocalTime())
                //    .HasDefaultValueSql("GETUTCDATE()");
                //builder.Property(x => x.UpdatedAt)
                //    .HasConversion(x => x, x => DateTime.SpecifyKind(x, DateTimeKind.Utc).ToLocalTime())
                //    .HasDefaultValueSql("GETUTCDATE()");
            }
    }
}

