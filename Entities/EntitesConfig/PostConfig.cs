using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackoveflowClone.Entities.EntitesConfig
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Question)
                .IsRequired();

            builder.Property(p => p.TextPost)
                .IsRequired();

            builder.Property(p =>p.CreatedDate)
                .IsRequired();

            builder.HasOne(u => u.User)
                .WithMany(p => p.Posts)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(p => p.Tags)
                .WithMany(t => t.Post)
                .UsingEntity<TagAndPostConn>
                (
                w => w.HasOne(wit => wit.Tag)
                .WithMany()
                .HasForeignKey(w => w.TagId),

                w => w.HasOne(wit => wit.Post)
                .WithMany()
                .HasForeignKey(w => w.PostId),

                wit => wit.HasKey(x => new { x.TagId, x.PostId })
                );
        }
    }
}

