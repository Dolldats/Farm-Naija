using Application.DTOs.User;
using Application.Interface.Repository;
using Application.Interface.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Task<UserResponseDto> CreateUserAsync(RegisterUserDto dto)
        {
            throw new NotImplementedException();
            //var existingUser = await _userRepositories.GetUserByEmailAsync(dto.Email);

            //if (existingUser != null)
            //{
            //    throw new InvalidOperationException("A User with this Email already exists.");
            //}

            //var user = new User
            //{
            //    FullName = dto.FullName,
            //    Email = dto.Email,
            //    PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            //    PhoneNumber = dto.PhoneNumber,
            //    Address = dto.Address,

            //};

            //await _patientRepositories.AddAsync(patient);
            //await _patientRepositories.SaveChangesAsync();
        }

        public Task<bool> DeletePatientAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserResponseDto>> GetAllUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserResponseDto?> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponseDto?> UpdatePatientAsync(int id, UpdateUserDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
