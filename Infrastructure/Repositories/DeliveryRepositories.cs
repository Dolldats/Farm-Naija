// using Application.Interface.Repository;
// using Domain.Entities;
// using Infrastructure.Data;
// using Microsoft.EntityFrameworkCore;
// using Application.Interface.Repository.IDeliveryRepositories;

// namespace Infrastructure.Repositories
// {
//     public class DeliveryRepositories : IDeliveryRepositories
//     {
//         private readonly FarmNaijaDbcontext _context;

//         public DeliveryRepositories(FarmNaijaDbcontext context)
//         {
//             _context = context;
//         }

//         public async Task<IEnumerable<Delivery>> GetAllAsync()
//         {
//             return await _context.Deliveries
//                 .Include(d => d.Order)
//                 .ToListAsync();
//         }

//         public async Task<Delivery?> GetByIdAsync(int id)
//         {
//             return await _context.Deliveries
//                 .Include(d => d.Order)
//                 .FirstOrDefaultAsync(d => d.Id == id);
//         }

//         public async Task<Delivery> CreateAsync(Delivery delivery)
//         {
//             await _context.Deliveries.AddAsync(delivery);
//             await _context.SaveChangesAsync();

//             return delivery;
//         }

//         public async Task<Delivery> UpdateAsync(Delivery delivery)
//         {
//             _context.Deliveries.Update(delivery);
//             await _context.SaveChangesAsync();

//             return delivery;
//         }

//         public async Task<bool> DeleteAsync(int id)
//         {
//             var delivery = await _context.Deliveries
//                 .FirstOrDefaultAsync(d => d.Id == id);

//             if (delivery == null)
//             {
//                 return false;
//             }

//             _context.Deliveries.Remove(delivery);
//             await _context.SaveChangesAsync();

//             return true;
//         }
//     }
using Application.Interface.Repository;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DeliveryRepositories : IDeliveryRepositories
    {
        private readonly FarmNaijaDbcontext _context;

        public DeliveryRepositories(FarmNaijaDbcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Delivery>> GetAllAsync()
        {
            return await _context.Deliveries
                .Include(d => d.Order)
                .ToListAsync();
        }

        public async Task<Delivery?> GetByIdAsync(int id)
        {
            return await _context.Deliveries
                .Include(d => d.Order)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Delivery> CreateAsync(Delivery delivery)
        {
            await _context.Deliveries.AddAsync(delivery);
            await _context.SaveChangesAsync();

            return delivery;
        }

        public async Task<Delivery> UpdateAsync(Delivery delivery)
        {
            _context.Deliveries.Update(delivery);
            await _context.SaveChangesAsync();

            return delivery;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var delivery = await _context.Deliveries
                .FirstOrDefaultAsync(d => d.Id == id);

            if (delivery == null)
            {
                return false;
            }

            _context.Deliveries.Remove(delivery);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}