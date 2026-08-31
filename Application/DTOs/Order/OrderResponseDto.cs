using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enum;
namespace Application.DTOs.Order;

public class OrderResponseDto
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int AddressId { get; set; }

    public decimal TotalAmount { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<OrderItemResponseDto> Items { get; set; } = new();
}