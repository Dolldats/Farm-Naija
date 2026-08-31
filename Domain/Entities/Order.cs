using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enum;
namespace Domain.Entities
{
    public class Order
    {

        public int Id { get; set; }

        public int CustomerId { get; set; } = default!;

        public int AddressId { get; set; } = default!;

        public decimal TotalAmount { get; set; } = default!;

        public OrderStatus Status { get; set; }
        public string? ProfileImage { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public DateTime? UpdatedAt { get; set; }

    }
}