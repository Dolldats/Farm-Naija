using Application.DTOs.Farmer;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Services
{
    public interface IFarmerServices
    {
        Task<FarmerResponseDto?> CreateFarmerAsync(int userId, CreateFarmerDto dto);
        Task<FarmerResponseDto?> GetFarmerByIdAsync(int id);
        Task<FarmerResponseDto?> GetFarmerByUserIdAsync(int userId);
        Task<IEnumerable<FarmerResponseDto>> GetAllFarmerAsync();
        Task<FarmerResponseDto?> UpdateFarmerAsync(int id, int userId, UpdateFarmerDto dto);
        Task<bool> DeleteFarmerAsync(int id, int userId);
    }
}
