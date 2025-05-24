using Microsoft.EntityFrameworkCore;
using Pharmacy_ASP_API.Models.Entities;
using Pharmacy_ASP_API.Models;

namespace Pharmacy_ASP_API.Repositories
{
    public class StockRepo : IRepositories<Stock>
    {
        private readonly PharmacyDbContext _context;

        public StockRepo(PharmacyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
            return await _context.Stocks
                .Include(s => s.Orders)
                .Include(s => s.MedicationKnowledges)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Stock> GetByIdAsync(Guid id)
        {
            return await _context.Stocks
                .Include(s => s.Orders)
                .Include(s => s.MedicationKnowledges)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.StockId == id);
        }

        public async Task AddAsync(Stock entity)
        {
            if (entity.Orders == null)
            {
                entity.Orders = new List<Order>();
            }

            if (entity.MedicationKnowledges == null)
            {
                entity.MedicationKnowledges = new List<MedicationKnowledge>();
            }

            await _context.Stocks.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Stock entity, Guid id)
        {
            var existingStock = await _context.Stocks
                .Include(s => s.Orders)
                .Include(s => s.MedicationKnowledges)
                .FirstOrDefaultAsync(s => s.StockId == id);

            if (existingStock == null)
            {
                throw new KeyNotFoundException($"Stock with ID {id} not found.");
            }

            // Update scalar properties
            existingStock.Quantity = entity.Quantity;
            existingStock.WarningDate = entity.WarningDate;

            // Don't update the collections here as they should be managed through their respective entities

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var stock = await _context.Stocks
                .Include(s => s.Orders)
                .Include(s => s.MedicationKnowledges)
                .FirstOrDefaultAsync(s => s.StockId == id);

            if (stock == null)
            {
                throw new KeyNotFoundException($"Stock with ID {id} not found.");
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
        }
    }
}

