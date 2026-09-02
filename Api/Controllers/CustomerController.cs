using Application.DTOs.Customer;
using Application.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(
            ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create()
        {
            try
            {
                var userId = GetUserId();

                var customer = await _customerService.CreateCustomerAsync(userId);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = customer.Id },
                    customer);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.GetAllCustomerAsync();

            return Ok(customers);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Customer not found."
                });
            }

            return Ok(customer);
        }

        [HttpGet("my-profile")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = GetUserId();

            var customer = await _customerService.GetCustomerByUserIdAsync(userId);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Customer profile not found."
                });
            }

            return Ok(customer);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Update(int id, UpdateCustomerDto dto)
        {
            try
            {
                var userId = GetUserId();

                var customer = await _customerService.UpdateCustomerAsync(id, userId, dto);

                if (customer == null)
                {
                    return NotFound(new
                    {
                        message = "Customer not found."
                    });
                }

                return Ok(customer);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = GetUserId();

                var deleted = await _customerService.DeleteCustomerAsync(id, userId);

                if (!deleted)
                    return NotFound(new
                    {
                        message = "Customer not found."
                    });

                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        private int GetUserId()
        {
            var userId =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

            if (!int.TryParse(userId, out var id))
                throw new UnauthorizedAccessException(
                    "Invalid user identity.");

            return id;
        }
    }
}