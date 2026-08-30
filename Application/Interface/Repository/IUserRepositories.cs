using Application.DTOs.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IUserRepositories 
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetAllUsersByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
        void Update(User user);
        void Delete(User user);
        Task SaveChangesAsync();
    }
}
