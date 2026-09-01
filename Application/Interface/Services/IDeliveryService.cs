using Application.DTOs.Delivery;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface.Services
{
    public interface IDeliveryServices
    {
        Task<DeliveryResponseDto?> GetByIdAsync(int id);

        Task<IEnumerable<DeliveryResponseDto>> GetAllAsync();

        Task<DeliveryResponseDto> CreateAsync(CreateDeliveryDto dto);

        Task<DeliveryResponseDto?> UpdateAsync(int id, UpdateDeliveryDto dto);

        Task<bool> DeleteAsync(int id);
    }
}