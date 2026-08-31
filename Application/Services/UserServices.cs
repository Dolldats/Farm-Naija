using Application.DTOs.User;
using Application.Interface.Repository;
using Application.Interface.Services;
using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepositories _userRepositories;

        public UserServices(IUserRepositories userRepositories)
        {
            _userRepositories = userRepositories;
        }

        public async Task<UserResponseDto> CreateUserAsync(RegisterUserDto dto)
        {
            var existingUser = await _userRepositories.GetUserByEmailAsync(dto.Email);

            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with this email already exists.");
            }

            if (!System.Enum.TryParse<Role>(dto.Role, ignoreCase: true, out var role))
            {
                throw new ArgumentException($"Invalid role: {dto.Role}. Valid values are: Customer, Farmer, Admin.");
            }

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                Role = role,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepositories.AddUserAsync(user);
            await _userRepositories.SaveChangesAsync();

            return MapToUserResponseDto(user);
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            var user = await _userRepositories.GetAllUsersByIdAsync(id);

            if (user == null)
            {
                return false;
            }

            _userRepositories.Delete(user);
            await _userRepositories.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUserAsync()
        {
            var users = await _userRepositories.GetAllUsersAsync();
            return users.Select(MapToUserResponseDto);
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepositories.GetAllUsersByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            return MapToUserResponseDto(user);
        }

        public async Task<UserResponseDto?> UpdatePatientAsync(int id, UpdateUserDto dto)
        {
            var user = await _userRepositories.GetAllUsersByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            if (!System.Enum.TryParse<Role>(dto.Role, ignoreCase: true, out var role))
            {
                throw new ArgumentException($"Invalid role: {dto.Role}. Valid values are: Customer, Farmer, Admin.");
            }

            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.Address = dto.Address;
            user.Role = role;
            user.ProfileImage = dto.ProfileImage;
            user.UpdatedAt = DateTime.UtcNow;

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            _userRepositories.Update(user);
            await _userRepositories.SaveChangesAsync();

            return MapToUserResponseDto(user);
        }

        private static UserResponseDto MapToUserResponseDto(User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Role = user.Role.ToString(),
                ProfileImage = user.ProfileImage,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
