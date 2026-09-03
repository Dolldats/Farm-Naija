using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(n => n.UserId)
                   .IsRequired();

            builder.Property(n => n.Title)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(n => n.Message)
                   .HasMaxLength(1000)
                   .IsRequired();

            builder.Property(n => n.Type)
                   .IsRequired();

            builder.Property(n => n.IsRead)
                   .IsRequired();

            builder.Property(n => n.RelatedId)
                   .IsRequired(false);

            builder.Property(n => n.RelatedType)
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(n => n.CreatedAt)
                   .IsRequired();

            builder.Property(n => n.ReadAt)
                   .IsRequired(false);

            builder.Property(n => n.UpdatedAt)
                   .IsRequired(false);
        }
    }
}