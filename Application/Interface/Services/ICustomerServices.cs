using Application.DTOs.Customer;

namespace Application.Interface.Services
{
    public interface ICustomerService
    {
        Task<CustomerResponseDto> CreateCustomerAsync(int userId);

        Task<CustomerResponseDto?> GetCustomerByIdAsync(int id);

        Task<CustomerResponseDto?> GetCustomerByUserIdAsync(int userId);

        Task<IEnumerable<CustomerResponseDto>> GetAllCustomerAsync();

        Task<CustomerResponseDto?> UpdateCustomerAsync(int id, int userId, UpdateCustomerDto dto);

        Task<bool> DeleteCustomerAsync(int id, int userId);
    }
}