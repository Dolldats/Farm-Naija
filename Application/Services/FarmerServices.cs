using Application.DTOs.Farmer;
using Application.Interface.Repository;
using Application.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FarmerServices : IFarmerServices
    {
        private readonly IFarmerRepositories _farmerRepositories;
        private readonly IUserRepositories _userRepositories;

        public FarmerServices(IFarmerRepositories farmerRepositories, IUserRepositories userRepositories)
        {
            _farmerRepositories = farmerRepositories;
            _userRepositories = userRepositories;
        }

        public Task<FarmerResponseDto> CreateFarmerAsync(int userId, CreateFarmerDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteFarmerAsync(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FarmerResponseDto>> GetAllFarmerAsync()
        {
            throw new NotImplementedException();
        }

        public Task<FarmerResponseDto> GetFarmerByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<FarmerResponseDto> GetFarmerByUserIdAsyn(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<FarmerResponseDto> UpdateFarmerAsync(int id, int userId, UpdateFarmerDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
