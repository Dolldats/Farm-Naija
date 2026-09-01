using Domain.Enum;

namespace Application.DTOs.Delivery
{
    public class UpdateDeliveryDto
    {
        public DeliveryStatus Status { get; set; }

        public string? TrackingNumber { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public DateTime? ShippedAt { get; set; }

        public DateTime? DeliveredAt { get; set; }
    }
}