using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Order;

namespace Application.Interface.Services
{
    public interface IOrderServices
    {
         Task<OrderResponseDto?> GetByIdAsync(int id);

    Task<IEnumerable<OrderResponseDto>> GetAllAsync();

    Task<OrderResponseDto> CreateAsync(CreateOrderDto dto);

    Task<OrderResponseDto?> UpdateAsync(int id, UpdateOrderDto dto);

    Task<bool> DeleteAsync(int id);
    }
}