using Application.DTOs.Order;
using Application.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        // GET: api/order
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderServices.GetAllAsync();

            return Ok(orders);
        }

        // GET: api/order/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderServices.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound(new
                {
                    message = $"Order with id {id} not found."
                });
            }

            return Ok(order);
        }

        // POST: api/order
        [HttpPost]
        public async Task<IActionResult> CreateOrder(
            [FromBody] CreateOrderDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdOrder = await _orderServices.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetOrderById),
                    new { id = createdOrder.Id },
                    createdOrder
                );
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // PUT: api/order/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOrder(
            int id,
            [FromBody] UpdateOrderDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedOrder = await _orderServices.UpdateAsync(id, dto);

                if (updatedOrder == null)
                {
                    return NotFound(new
                    {
                        message = $"Order with id {id} not found."
                    });
                }

                return Ok(updatedOrder);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // DELETE: api/order/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderServices.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    message = $"Order with id {id} not found."
                });
            }

            return NoContent();
        }
    }
}