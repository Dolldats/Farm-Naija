using Application.DTOs.Delivery;
using Application.Interface.Repository;
using Application.Interface.Services;
using Domain.Enum;
using Domain.Entities;

namespace Application.Services
{
    public class DeliveryService : IDeliveryServices
    {
        private readonly IDeliveryRepositories _deliveryRepository;

        public DeliveryService(IDeliveryRepositories deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public async Task<DeliveryResponseDto?> GetByIdAsync(int id)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(id);

            if (delivery == null)
                return null;

            return MapToResponseDto(delivery);
        }

        public async Task<IEnumerable<DeliveryResponseDto>> GetAllAsync()
        {
            var deliveries = await _deliveryRepository.GetAllAsync();

            return deliveries.Select(MapToResponseDto);
        }

        public async Task<DeliveryResponseDto> CreateAsync(CreateDeliveryDto dto)
        {
            var delivery = new Delivery
            {
                OrderId = dto.OrderId,
                AddressId = dto.AddressId,
                TrackingNumber = dto.TrackingNumber,
                EstimatedDeliveryDate = dto.EstimatedDeliveryDate,
                Status = Domain.Enum.DeliveryStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            var createdDelivery = await _deliveryRepository.CreateAsync(delivery);

            return MapToResponseDto(createdDelivery);
        }

        public async Task<DeliveryResponseDto?> UpdateAsync(
            int id,
            UpdateDeliveryDto dto)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(id);

            if (delivery == null)
                return null;

            delivery.Status = dto.Status;
            delivery.TrackingNumber = dto.TrackingNumber;
            delivery.EstimatedDeliveryDate = dto.EstimatedDeliveryDate;
            delivery.ShippedAt = dto.ShippedAt;
            delivery.DeliveredAt = dto.DeliveredAt;
            delivery.UpdatedAt = DateTime.UtcNow;

            var updatedDelivery = await _deliveryRepository.UpdateAsync(delivery);

            return MapToResponseDto(updatedDelivery);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _deliveryRepository.DeleteAsync(id);
        }

        private static DeliveryResponseDto MapToResponseDto(Delivery delivery)
        {
            return new DeliveryResponseDto
            {
                Id = delivery.Id,
                OrderId = delivery.OrderId,
                AddressId = delivery.AddressId,
                TrackingNumber = delivery.TrackingNumber,
                Status = delivery.Status,
                ShippedAt = delivery.ShippedAt,
                DeliveredAt = delivery.DeliveredAt,
                EstimatedDeliveryDate = delivery.EstimatedDeliveryDate,
                CreatedAt = delivery.CreatedAt,
                UpdatedAt = delivery.UpdatedAt
            };
        }
    }
}