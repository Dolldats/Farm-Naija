using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(d => d.OrderId)
                   .IsRequired();

            builder.Property(d => d.AddressId)
                   .IsRequired();

            builder.Property(d => d.TrackingNumber)
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(d => d.Status)
                   .IsRequired();

            builder.Property(d => d.ShippedAt)
                   .IsRequired(false);

            builder.Property(d => d.DeliveredAt)
                   .IsRequired(false);

            builder.Property(d => d.EstimatedDeliveryDate)
                   .IsRequired(false);

            builder.Property(d => d.CreatedAt)
                   .IsRequired();

            builder.Property(d => d.UpdatedAt)
                   .IsRequired(false);

            // One Order can have one Delivery
            builder.HasOne(d => d.Order)
                   .WithOne()
                   .HasForeignKey<Delivery>(d => d.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}