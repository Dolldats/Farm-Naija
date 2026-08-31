using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
namespace Application.Interface.Repository
{
    public interface IOrderRepositories
    {
         Task<Order?> GetByIdAsync(int id);

    Task<IEnumerable<Order>> GetAllAsync();

    Task<Order> CreateAsync(Order order);

    Task<Order> UpdateAsync(Order order);

    Task<bool> DeleteAsync(int id);
    }
}