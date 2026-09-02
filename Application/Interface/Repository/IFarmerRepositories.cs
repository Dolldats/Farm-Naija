using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IFarmerRepositories
    {
        Task<Farmer?> GetFarmerByIdAsync(int id);
        Task<Farmer?> GetFarmerByUserIdAsync(int userId);
        Task<IEnumerable<Farmer>> GetAllFarmersAsync();
        Task<Farmer> AddFarmerAsync(Farmer farmer);
        Task UpdateFarmerAsync(Farmer farmer);
        Task DeleteFarmerAsync(Farmer farmer);
        Task SaveChangesAsync();

    }
}
