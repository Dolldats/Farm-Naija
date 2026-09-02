using Application.DTOs.Farmer;
using Application.Interface.Repository;
using Application.Interface.Services;
using Domain.Entities;
using Domain.Enum;

namespace FarmNigeria.Services
{
    public class FarmerService : IFarmerServices
    {
        private readonly IFarmerRepositories _farmerRepositories;
        private readonly IUserRepositories _userRepositories;

        public FarmerService(
            IFarmerRepositories farmerRepositories,
            IUserRepositories userRepositories)
        {
            _farmerRepositories = farmerRepositories;
            _userRepositories = userRepositories;
        }

        public async Task<FarmerResponseDto> CreateFarmerAsync(
            int userId,
            CreateFarmerDto dto)
        {
            var user = await _userRepositories.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            if (user.Role != Role.Farmer)
            {
                throw new InvalidOperationException("Only users with the Farmer role can create a farmer profile.");
            }

            var existingFarmer = await _farmerRepositories.GetFarmerByUserIdAsync(userId);

            if (existingFarmer != null)
            {
                throw new InvalidOperationException("Farmer profile already exists.");
            }

            var farmer = new Farmer
            {
                UserId = userId,
                FarmName = dto.FarmName,
                FarmDescription = dto.FarmDescription,
                FarmLocation = dto.FarmLocation,
                IsVerified = false,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _farmerRepositories.AddFarmerAsync(farmer);

            created.User = user;

            return TakeToFarmerResponseDto(created);
        }

        public async Task<FarmerResponseDto?> GetFarmerByIdAsync(int id)
        {
            var farmer = await _farmerRepositories.GetFarmerByIdAsync(id);
            if(farmer == null)
            {
                return null;
            }   

            return TakeToFarmerResponseDto(farmer);
        }

        public async Task<FarmerResponseDto?> GetFarmerByUserIdAsync(
            int userId)
        {
            var farmer = await _farmerRepositories.GetFarmerByUserIdAsync(userId);
            if(farmer == null)
            {
                return null;
            }

            return TakeToFarmerResponseDto(farmer);
        }

        public async Task<IEnumerable<FarmerResponseDto>> GetAllFarmerAsync()
        {
            var farmers = await _farmerRepositories.GetAllFarmersAsync();

            return farmers.Select(TakeToFarmerResponseDto);
        }

        public async Task<FarmerResponseDto?> UpdateFarmerAsync(int id, int userId, UpdateFarmerDto dto)
        {
            var farmer = await _farmerRepositories.GetFarmerByIdAsync(id);

            if (farmer == null)
            {
                return null;
            }

            if (farmer.UserId != userId)
            {
                throw new UnauthorizedAccessException("You can only update your own farmer profile.");
            }

            farmer.FarmName = dto.FarmName;
            farmer.FarmDescription = dto.FarmDescription;
            farmer.FarmLocation = dto.FarmLocation;
            farmer.UpdatedAt = DateTime.UtcNow;

            await _farmerRepositories.UpdateFarmerAsync(farmer);

            return TakeToFarmerResponseDto(farmer);
        }

        public async Task<bool> DeleteFarmerAsync(int id, int userId)
        {
            var farmer = await _farmerRepositories.GetFarmerByIdAsync(id);

            if (farmer == null)
            {
                return false;
            }

            if (farmer.UserId != userId)
            {
                throw new UnauthorizedAccessException("You can only delete your own farmer profile.");
            }

            await _farmerRepositories.DeleteFarmerAsync(farmer);
            await _farmerRepositories.SaveChangesAsync();

            return true;
        }

        private static FarmerResponseDto TakeToFarmerResponseDto(
            Farmer farmer)
        {
            return new FarmerResponseDto
            {
                Id = farmer.Id,
                UserId = farmer.UserId,
                FullName = farmer.User?.FullName ?? string.Empty,
                FarmName = farmer.FarmName,
                FarmDescription = farmer.FarmDescription,
                FarmLocation = farmer.FarmLocation,
                IsVerified = farmer.IsVerified,
                CreatedAt = farmer.CreatedAt
            };
        }
    }
}