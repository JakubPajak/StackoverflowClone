using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackoveflowClone.Entities.EntitesConfig
{
    public class ComentConfig : IEntityTypeConfiguration<Coment>
    {
        public void Configure(EntityTypeBuilder<Coment> builder)
        {
            builder.Property(c => c.Text)
                .IsRequired();

            builder.Property(c => c.CreatedDate)
                .IsRequired();

            builder.HasOne(u => u.User)
                .WithMany(c => c.Coments)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(p => p.Post)
                .WithMany(c => c.Coments)
                .HasForeignKey(k => k.PostId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}

