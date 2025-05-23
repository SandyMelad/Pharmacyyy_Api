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

        public async Task<MedicationKnowledge> GetByIdAsync(Guid id)
        {
            return await _context.MedicationKnowledges
                .Include(m => m.Stock)
                .Include(m => m.Orders)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MedicationId == id);
        }

        public async Task AddAsync(MedicationKnowledge entity)
        {
            await _context.MedicationKnowledges.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MedicationKnowledge entity, Guid id)
        {
            var existing = await _context.MedicationKnowledges
                .FirstOrDefaultAsync(m => m.MedicationId == id);

            if (existing == null)
            {
                throw new KeyNotFoundException($"MedicationKnowledge with ID {id} not found.");
            }

            // Update scalar properties
            existing.MedicationName = entity.MedicationName;
            existing.ClinicalUse = entity.ClinicalUse;
            existing.Cost = entity.Cost;
            existing.ProductType = entity.ProductType;
            existing.Status = entity.Status;
            existing.StockId = entity.StockId;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.MedicationKnowledges
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
