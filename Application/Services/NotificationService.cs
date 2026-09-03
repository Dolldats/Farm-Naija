using Application.DTOs.Notification;
using Application.Interface.Repository;
using Application.Interface.Services;
using Domain.Entities;

namespace Application.Services
{
    public class NotificationServices : INotificationServices
    {
        private readonly INotificationRepositories _notificationRepository;

        public NotificationServices(
            INotificationRepositories notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<NotificationResponseDto?> GetByIdAsync(int id)
        {
            var notification = await _notificationRepository.GetByIdAsync(id);

            if (notification == null)
            {
                return null;
            }

            return MapToResponseDto(notification);}


        public async Task<IEnumerable<NotificationResponseDto>> GetAllAsync()
        {
            var notifications = await _notificationRepository.GetAllAsync();

            return notifications.Select(MapToResponseDto);
        }

        public async Task<NotificationResponseDto> CreateAsync(
            CreateNotificationDto dto)
        {
            var notification = new Notification
            {
                UserId = dto.UserId,
                Title = dto.Title,
                Message = dto.Message,
                Type = dto.Type,
                RelatedId = dto.RelatedId,
                RelatedType = dto.RelatedType,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            var createdNotification =
                await _notificationRepository.CreateAsync(notification);

            return MapToResponseDto(createdNotification);
        }

        public async Task<NotificationResponseDto?> UpdateAsync(
            int id,
            UpdateNotificationDto dto)
        {
            var notification =
                await _notificationRepository.GetByIdAsync(id);

            if (notification == null)
            {
                return null;
            }

            notification.IsRead = dto.IsRead;

            if (dto.IsRead)
            {
                notification.ReadAt ??= DateTime.UtcNow;
            }
            else
            {
                notification.ReadAt = null;
            }

            notification.UpdatedAt = DateTime.UtcNow;

            var updatedNotification =
                await _notificationRepository.UpdateAsync(notification);

            return MapToResponseDto(updatedNotification);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _notificationRepository.DeleteAsync(id);
        }

        private static NotificationResponseDto MapToResponseDto(
            Notification notification)
        {
            return new NotificationResponseDto
            {
                Id = notification.Id,
                UserId = notification.UserId,
                Title = notification.Title,
                Message = notification.Message,
                Type = notification.Type,
                IsRead = notification.IsRead,
                RelatedId = notification.RelatedId,
                RelatedType = notification.RelatedType,
                CreatedAt = notification.CreatedAt,
                ReadAt = notification.ReadAt,
                UpdatedAt = notification.UpdatedAt
            };
        }
    }
}
