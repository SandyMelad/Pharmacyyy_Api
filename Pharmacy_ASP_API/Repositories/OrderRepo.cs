using Microsoft.EntityFrameworkCore;
using Pharmacy_ASP_API.Models;
using Pharmacy_ASP_API.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy_ASP_API.Repositories
{
    public class OrderRepo : IRepositories<Order>
    {
        private readonly PharmacyDbContext _context;

        public OrderRepo(PharmacyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.Medication)
                .Include(o => o.MedicationRequest)
                .Include(o => o.MedicationKnowledges)
                .Include(o => o.Stock)
                .Include(o => o.Patient)
                .Include(o => o.Report)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Order> GetByIdAsync(string id)
        {
            return await _context.Orders
                .Include(o => o.Medication)
                .Include(o => o.MedicationRequest)
                .Include(o => o.MedicationKnowledges)
                .Include(o => o.Stock)
                .Include(o => o.Patient)
                .Include(o => o.Report)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task AddAsync(Order entity)
        {
            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order entity, string id)
        {
            var existing = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (existing == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }

            // Update scalar properties
            existing.MedicationId = entity.MedicationId;
            existing.MedicationRequestId = entity.MedicationRequestId;
            existing.OrderTime = entity.OrderTime;
            existing.Quantity = entity.Quantity;
            existing.PatientId = entity.PatientId;
            existing.ReportId = entity.ReportId;
            existing.StockId = entity.StockId;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (entity == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }

            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
