using Domain.Enum;

namespace Application.DTOs.Notification
{
    public class NotificationResponseDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public NotificationType Type { get; set; }

        public bool IsRead { get; set; }

        public int? RelatedId { get; set; }

        public string? RelatedType { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ReadAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}