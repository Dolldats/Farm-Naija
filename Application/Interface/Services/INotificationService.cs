using Application.DTOs.Notification;

namespace Application.Interface.Services
{
    public interface INotificationServices
    {
        Task<NotificationResponseDto?> GetByIdAsync(int id);

        Task<IEnumerable<NotificationResponseDto>> GetAllAsync();

        Task<NotificationResponseDto> CreateAsync(CreateNotificationDto dto);

        Task<NotificationResponseDto?> UpdateAsync(
            int id,
            UpdateNotificationDto dto);

        Task<bool> DeleteAsync(int id);
    }
}