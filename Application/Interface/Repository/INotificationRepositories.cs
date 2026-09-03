using Domain.Entities;
namespace Application.Interface.Repository
{
    public interface INotificationRepositories
    {
        Task<Notification?> GetByIdAsync(int id);

        Task<IEnumerable<Notification>> GetAllAsync();

        Task<Notification> CreateAsync(Notification notification);

        Task<Notification> UpdateAsync(Notification notification);

        Task<bool> DeleteAsync(int id);
    }
}