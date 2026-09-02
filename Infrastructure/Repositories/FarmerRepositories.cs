using Application.Interface.Repository;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FarmerRepositories : IFarmerRepositories
    {
        private readonly FarmNaijaDbcontext _context;
        public FarmerRepositories(FarmNaijaDbcontext context)
        {
            _context = context;
        }

        public async Task<Farmer> AddFarmerAsync(Farmer farmer)
        {
            await _context.Farmers.AddAsync(farmer);
            await _context.SaveChangesAsync();

            return farmer;
        }

        public async Task DeleteFarmerAsync(Farmer farmer)
        {
            _context.Farmers.Remove(farmer);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Farmer>> GetAllFarmersAsync()
        {
            return await _context.Farmers
                .Include(f => f.User)
                .ToListAsync();
        }

        public async Task<Farmer?> GetFarmerByIdAsync(int id)
        {
            return await _context.Farmers
                .Include(f => f.User)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Farmer?> GetFarmerByUserIdAsync(int userId)
        {
            return await _context.Farmers 
                .Include(f => f.User)
                .FirstOrDefaultAsync(f => f.UserId == userId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFarmerAsync(Farmer farmer)
        {
            _context.Farmers.Update(farmer);
            await _context.SaveChangesAsync();
        }
    }
}
