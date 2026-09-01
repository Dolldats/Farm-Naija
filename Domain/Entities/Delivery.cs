using Domain.Enum;

namespace Domain.Entities
{
    public class Delivery 
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int AddressId { get; set; }

        public string? TrackingNumber { get; set; }

        public DeliveryStatus Status { get; set; }

        public DateTime? ShippedAt { get; set; }

        public DateTime? DeliveredAt { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation property
        public Order Order { get; set; } = null!;
    }
}