using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackoveflowClone.Entities.EntitesConfig
{
    public class TagsConfig : IEntityTypeConfiguration<Tags>
    {
        public void Configure(EntityTypeBuilder<Tags> builder)
        {
            builder.Property(t => t.Tag)
                .IsRequired()
                .HasMaxLength(15);
        }
    }
}

