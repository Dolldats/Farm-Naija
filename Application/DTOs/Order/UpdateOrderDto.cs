using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enum;

namespace Application.DTOs.Order
{
    public class UpdateOrderDto
    {
         public OrderStatus Status { get; set; }

    }
}