using Domain.Entities;

namespace Application.Interface.Repository
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerByIdAsync(int id);

        Task<Customer?> GetCustomerByUserIdAsync(int userId);

        Task<IEnumerable<Customer>> GetAllCustomerAsync();

        Task<Customer> AddCustomerAsync(Customer customer);

        Task UpdateCustomerAsync(Customer customer);

        Task DeleteCustomerAsync(Customer customer);
    }
}