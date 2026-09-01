using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IDeliveryRepositories
    {
        Task<Delivery?> GetByIdAsync(int id);

        Task<IEnumerable<Delivery>> GetAllAsync();

        Task<Delivery> CreateAsync(Delivery delivery);

        Task<Delivery> UpdateAsync(Delivery delivery);

        Task<bool> DeleteAsync(int id);
    }
}