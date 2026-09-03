using Application.DTOs.Customer;
using Application.Interface.Repository;
using Application.Interface.Services;
using Domain.Entities;
using Domain.Enum;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepositories;
        private readonly IUserRepositories _userRepositories;

        public CustomerService(ICustomerRepository customerRepositories, IUserRepositories userRepositories)
        {
            _customerRepositories = customerRepositories;
            _userRepositories = userRepositories;
        }

        public async Task<CustomerResponseDto> CreateCustomerAsync(int userId)
        {
            var user = await _userRepositories.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            if (user.Role != Role.Customer)
            {
                throw new InvalidOperationException("Only users with the Customer role can create a customer profile.");
            }
            var existingCustomer = await _customerRepositories.GetCustomerByUserIdAsync(userId);

            if (existingCustomer != null)
            {
                throw new InvalidOperationException("Customer profile already exists.");
            }

            var customer = new Customer
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _customerRepositories.AddCustomerAsync(customer);

            created.User = user;

            return TakeToCustomerResponseDto(created);
        }

        public async Task<CustomerResponseDto?> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepositories.GetCustomerByIdAsync(id);
            if(customer == null)
            {
                return null;
            }

            return TakeToCustomerResponseDto(customer);
        }

        public async Task<CustomerResponseDto?> GetCustomerByUserIdAsync(int userId)
        {
            var customer = await _customerRepositories.GetCustomerByUserIdAsync(userId);
            if (customer == null)
            {
                return null;
            }

            return TakeToCustomerResponseDto(customer);
        }

        public async Task<IEnumerable<CustomerResponseDto>> GetAllCustomerAsync()
        {
            var customers = await _customerRepositories.GetAllCustomerAsync();

            return customers.Select(TakeToCustomerResponseDto);
        }

        public async Task<CustomerResponseDto?> UpdateCustomerAsync(int id, int userId, UpdateCustomerDto dto)
        {
            var customer = await _customerRepositories.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return null;
            }

            if (customer.UserId != userId)
            {
                throw new UnauthorizedAccessException("You can only update your own profile.");
            }

            customer.User.FullName = dto.FullName;
            customer.User.PhoneNumber = dto.PhoneNumber;
            customer.UpdatedAt = DateTime.UtcNow;
            customer.User.UpdatedAt = DateTime.UtcNow;

            await _customerRepositories.UpdateCustomerAsync(customer);

            return TakeToCustomerResponseDto(customer);
        }

        public async Task<bool> DeleteCustomerAsync(int id, int userId)
        {
            var customer = await _customerRepositories.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return false;
            }

            if (customer.UserId != userId)
            {
                throw new UnauthorizedAccessException("You can only delete your own profile.");
            }

            await _customerRepositories.DeleteCustomerAsync(customer);

            return true;
        }

        private static CustomerResponseDto TakeToCustomerResponseDto(
            Customer customer)
        {
            return new CustomerResponseDto
            {
                Id = customer.Id,
                UserId = customer.UserId,
                FullName = customer.User?.FullName ?? string.Empty,
                Email = customer.User?.Email ?? string.Empty,
                PhoneNumber = customer.User?.PhoneNumber ?? string.Empty,
                CreatedAt = customer.CreatedAt
            };
        }
    }
}