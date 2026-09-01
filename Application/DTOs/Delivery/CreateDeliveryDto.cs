namespace Application.DTOs.Delivery
{
    public class CreateDeliveryDto
    {
        public int OrderId { get; set; }

        public int AddressId { get; set; }

        public string? TrackingNumber { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }
    }
}