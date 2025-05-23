using Pharmacy_ASP_API.Models;
using Microsoft.EntityFrameworkCore;
using Pharmacy_ASP_API.Models.Entities;

namespace Pharmacy_ASP_API.Repositories
{
    public class FinanceRepo : IRepositories<Finance>
    {
        private readonly PharmacyDbContext _context;

        public FinanceRepo(PharmacyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Finance>> GetAllAsync()
        {
            return await _context.Finances
                .Include(f => f.Reports)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Finance> GetByIdAsync(Guid id)
        {
            // Since Finance has a composite key, we need to handle this differently
            return await _context.Finances
                .Include(f => f.Reports)
                .FirstOrDefaultAsync(f => f.ReportId == id);
        }

        public async Task AddAsync(Finance entity)
        {
            await _context.Finances.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Finance entity, Guid id)
        {
            var existing = await _context.Finances
                .FirstOrDefaultAsync(f => f.ReportId == id);
            
            if (existing == null)
            {
                throw new KeyNotFoundException($"Finance record with ReportId {id} not found.");
            }

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Finances
                .FirstOrDefaultAsync(f => f.ReportId == id);
            
            if (entity == null)
            {
                throw new KeyNotFoundException($"Finance record with ReportId {id} not found.");
            }

            _context.Finances.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
