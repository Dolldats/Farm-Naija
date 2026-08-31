using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Application.DTOs.Order;

public class OrderItemResponseDto
{
    public int  ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice { get; set; }
}