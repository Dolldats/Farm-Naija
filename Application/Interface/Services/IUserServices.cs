using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Services
{
    public interface IUserServices
    {
        Task<IEnumerable<UserResponseDto>> GetAllUserAsync();
        Task<UserResponseDto?> GetUserByIdAsync(int id);
        Task<UserResponseDto> CreateUserAsync(RegisterUserDto dto);
        Task<UserResponseDto?> UpdatePatientAsync(int id, UpdateUserDto dto);
        Task<bool> DeletePatientAsync(int id);
    }
}
