using Application.DTOs.Order;
using Application.Interface.Repository;
using Application.Interface.Services;
using Domain.Entities;

namespace Application.Services
{
    public class OrderService : IOrderServices
    {
        private readonly IOrderRepositories _orderRepository;

        public OrderService(IOrderRepositories orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderResponseDto?> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                return null;

            return MapToResponseDto(order);
        }

        public async Task<IEnumerable<OrderResponseDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();

            return orders.Select(MapToResponseDto);
        }

        public async Task<OrderResponseDto> CreateAsync(CreateOrderDto dto)
        {
            var order = new Order
            {
                CustomerId = dto.CustomerId,
                AddressId = dto.AddressId,
                Status = Domain.Enum.OrderStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                TotalAmount = 0
            };

            foreach (var item in dto.Items)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                order.OrderItems.Add(orderItem);
            }

            var createdOrder = await _orderRepository.CreateAsync(order);

            return MapToResponseDto(createdOrder);
        }

        public async Task<OrderResponseDto?> UpdateAsync(
            int id,
            UpdateOrderDto dto)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                return null;

            order.Status = dto.Status;
            order.UpdatedAt = DateTime.UtcNow;

            var updatedOrder = await _orderRepository.UpdateAsync(order);

            return MapToResponseDto(updatedOrder);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _orderRepository.DeleteAsync(id);
        }

        private static OrderResponseDto MapToResponseDto(Order order)
        {
            return new OrderResponseDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                AddressId = order.AddressId,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                CreatedAt = order.CreatedAt,

                Items = order.OrderItems.Select(item => new OrderItemResponseDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.TotalPrice
                }).ToList()
            };
        }
    }
}