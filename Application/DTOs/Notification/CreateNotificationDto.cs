using Domain.Enum;
namespace Application.DTOs.Notification
{
    public class CreateNotificationDto
    {
        public int UserId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public NotificationType Type { get; set; }

        public int? RelatedId { get; set; }

        public string? RelatedType { get; set; }
    }
}