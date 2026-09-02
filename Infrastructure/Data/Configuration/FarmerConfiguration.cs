using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configuration
{
    public class FarmerConfiguration : IEntityTypeConfiguration<Farmer>
    {
        public void Configure(EntityTypeBuilder<Farmer> builder)
        {
            builder.HasKey(f => f.Id);

            builder.HasOne(f => f.User)
                .WithOne()
                .HasForeignKey<Farmer>(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(f => f.UserId)
                .IsUnique();

            builder.Property(f => f.FarmName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(f => f.FarmDescription)
                .HasMaxLength(500);

            builder.Property(f => f.FarmLocation)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(f => f.IsVerified)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(f => f.CreatedAt)
                .IsRequired();

            builder.Property(f => f.UpdatedAt)
                .IsRequired(false);
        }
    }
}
