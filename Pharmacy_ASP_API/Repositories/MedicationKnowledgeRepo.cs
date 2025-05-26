using Microsoft.EntityFrameworkCore;
using Pharmacy_ASP_API.Models;
using Pharmacy_ASP_API.Models.Entities;

namespace Pharmacy_ASP_API.Repositories
{
    public class MedicationKnowledgeRepo : IRepositories<MedicationKnowledge>
    {
        private readonly PharmacyDbContext _context;

        public MedicationKnowledgeRepo(PharmacyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MedicationKnowledge>> GetAllAsync()
        {
            return await _context.MedicationKnowledges
                .Include(m => m.Stock)
                .Include(m => m.Orders)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<MedicationKnowledge> GetByIdAsync(string id)
        {
            return await _context.MedicationKnowledges
                .Include(m => m.Stock)
                .Include(m => m.Orders)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MedicationId == id);
        }

        public async Task AddAsync(MedicationKnowledge entity)
        {
            // Verify that the Stock exists
            var stockExists = await _context.Stocks
                .AnyAsync(s => s.StockId == entity.StockId);

            if (!stockExists)
            {
                throw new InvalidOperationException($"Stock with ID {entity.StockId} does not exist.");
            }

            if (entity.Orders == null)
            {
                entity.Orders = new List<Order>();
            }

            await _context.MedicationKnowledges.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MedicationKnowledge entity, string id)
        {
            var existing = await _context.MedicationKnowledges
                .Include(m => m.Stock)
                .Include(m => m.Orders)
                .FirstOrDefaultAsync(m => m.MedicationId == id);

            if (existing == null)
            {
                throw new KeyNotFoundException($"MedicationKnowledge with ID {id} not found.");
            }

            // Verify that the new Stock exists if it's being changed
            if (existing.StockId != entity.StockId)
            {
                var stockExists = await _context.Stocks
                    .AnyAsync(s => s.StockId == entity.StockId);

                if (!stockExists)
                {
                    throw new InvalidOperationException($"Stock with ID {entity.StockId} does not exist.");
                }
            }

            // Update scalar properties
            existing.MedicationName = entity.MedicationName;
            existing.ClinicalUse = entity.ClinicalUse;
            existing.Cost = entity.Cost;
            existing.ProductType = entity.ProductType;
            existing.Status = entity.Status;
            existing.Expirydate = entity.Expirydate;
            existing.StockId = entity.StockId;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _context.MedicationKnowledges
                .Include(m => m.Stock)
                .Include(m => m.Orders)
                .FirstOrDefaultAsync(m => m.MedicationId == id);

            if (entity == null)
            {
                throw new KeyNotFoundException($"MedicationKnowledge with ID {id} not found.");
            }

            _context.MedicationKnowledges.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
