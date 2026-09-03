using Application.DTOs.Notification;
using Application.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationServices _notificationServices;

        public NotificationController(INotificationServices notificationServices)
        {
            _notificationServices = notificationServices;
        }

        // GET: api/notification
        [HttpGet]
        public async Task<IActionResult> GetAllNotifications()
        {
            var notifications = await _notificationServices.GetAllAsync();

            return Ok(notifications);
        }

        // GET: api/notification/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetNotificationById(int id)
        {
            var notification = await _notificationServices.GetByIdAsync(id);

            if (notification == null)
            {
                return NotFound(new
                {
                    message = $"Notification with id {id} not found."
                });
            }

            return Ok(notification);
        }

        // POST: api/notification
        [HttpPost]
        public async Task<IActionResult> CreateNotification(
            [FromBody] CreateNotificationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdNotification =
                    await _notificationServices.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetNotificationById),
                    new { id = createdNotification.Id },
                    createdNotification
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

        // PUT: api/notification/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateNotification(
            int id,
            [FromBody] UpdateNotificationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedNotification =
                    await _notificationServices.UpdateAsync(id, dto);

                if (updatedNotification == null)
                {
                    return NotFound(new
                    {
                        message = $"Notification with id {id} not found."
                    });
                }

                return Ok(updatedNotification);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // DELETE: api/notification/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var result = await _notificationServices.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    message = $"Notification with id {id} not found."
                });
            }

            return NoContent();
        }
    }
}