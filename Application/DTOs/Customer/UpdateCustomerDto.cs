using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Customer
{
    public class UpdateCustomerDto
    {
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}