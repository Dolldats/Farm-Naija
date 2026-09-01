using Application.DTOs.Delivery;
using Application.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryServices _deliveryServices;

        public DeliveryController(IDeliveryServices deliveryServices)
        {
            _deliveryServices = deliveryServices;
        }

        // GET: api/delivery
        [HttpGet]
        public async Task<IActionResult> GetAllDeliveries()
        {
            var deliveries = await _deliveryServices.GetAllAsync();

            return Ok(deliveries);
        }

        // GET: api/delivery/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDeliveryById(int id)
        {
            var delivery = await _deliveryServices.GetByIdAsync(id);

            if (delivery == null)
            {
                return NotFound(new
                {
                    message = $"Delivery with id {id} not found."
                });
            }

            return Ok(delivery);
        }

        // POST: api/delivery
        [HttpPost]
        public async Task<IActionResult> CreateDelivery(
            [FromBody] CreateDeliveryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdDelivery = await _deliveryServices.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetDeliveryById),
                    new { id = createdDelivery.Id },
                    createdDelivery
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

        // PUT: api/delivery/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateDelivery(
            int id,
            [FromBody] UpdateDeliveryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedDelivery = await _deliveryServices.UpdateAsync(id, dto);

                if (updatedDelivery == null)
                {
                    return NotFound(new
                    {
                        message = $"Delivery with id {id} not found."
                    });
                }

                return Ok(updatedDelivery);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // DELETE: api/delivery/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDelivery(int id)
        {
            var result = await _deliveryServices.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    message = $"Delivery with id {id} not found."
                });
            }

            return NoContent();
        }
    }
}