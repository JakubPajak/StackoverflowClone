using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackoveflowClone.Entities.EntitesConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.BirthDate)
                .IsRequired();

            builder.Property(u => u.PhoneNumber)
                .IsRequired();
                
            builder.Property(u => u.PassHash)
                .IsRequired();

            builder.HasOne(u => u.Address)
                .WithOne(a => a.User)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}

