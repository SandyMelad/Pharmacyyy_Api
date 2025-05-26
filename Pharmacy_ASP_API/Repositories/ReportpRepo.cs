using Pharmacy_ASP_API.Models;
using Microsoft.EntityFrameworkCore;
using Pharmacy_ASP_API.Models.Entities;

namespace Pharmacy_ASP_API.Repositories
{
    public class ReportpRepo : IRepositories<Report>
    {
        private readonly PharmacyDbContext _context;

        public ReportpRepo(PharmacyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Report>> GetAllAsync()
        {
            return await _context.Reports
                .Include(r => r.Orders)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Report> GetByIdAsync(string id)
        {
            return await _context.Reports
                .Include(r => r.Orders)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ReportId == id);
        }

        public async Task AddAsync(Report entity)
        {
            if (entity.Orders == null)
            {
                entity.Orders = new List<Order>();
            }

            await _context.Reports.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Report entity, string id)
        {
            var existingReport = await _context.Reports
                .Include(r => r.Orders)
                .FirstOrDefaultAsync(r => r.ReportId == id);

            if (existingReport == null)
            {
                throw new KeyNotFoundException($"Report with ID {id} not found.");
            }

            // Update scalar properties
            existingReport.TotalSales = entity.TotalSales;
            existingReport.OrderCount = entity.OrderCount;
            existingReport.StockAcidPrice = entity.StockAcidPrice;

            // Don't update the Orders collection here as it should be managed through Order entity

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var report = await _context.Reports
                .Include(r => r.Orders)
                .FirstOrDefaultAsync(r => r.ReportId == id);

            if (report == null)
            {
                throw new KeyNotFoundException($"Report with ID {id} not found.");
            }

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
        }
    }
}
