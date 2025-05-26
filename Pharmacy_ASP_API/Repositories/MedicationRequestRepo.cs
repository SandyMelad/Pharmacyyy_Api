using Microsoft.EntityFrameworkCore;
using Pharmacy_ASP_API.Models;
using Pharmacy_ASP_API.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy_ASP_API.Repositories
{
    public class MedicationRequestRepo : IRepositories<MedicationRequest>
    {
        private readonly PharmacyDbContext _context;

        public MedicationRequestRepo(PharmacyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MedicationRequest>> GetAllAsync()
        {
            return await _context.MedicationRequests
                .Include(mr => mr.Order)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<MedicationRequest> GetByIdAsync(string id)
        {
            return await _context.MedicationRequests
                .Include(mr => mr.Order)
                .AsNoTracking()
                .FirstOrDefaultAsync(mr => mr.RequestId == id);
        }

        public async Task AddAsync(MedicationRequest entity)
        {
            await _context.MedicationRequests.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MedicationRequest entity, string id)
        {
            var existing = await _context.MedicationRequests
                .Include(mr => mr.Order)
                .FirstOrDefaultAsync(mr => mr.RequestId == id);

            if (existing == null)
            {
                throw new KeyNotFoundException($"MedicationRequest with ID {id} not found.");
            }

            // Update scalar properties
            existing.DrOutBed = entity.DrOutBed;
            existing.DrInBed = entity.DrInBed;
            existing.Status = entity.Status;
            existing.StatusTime = entity.StatusTime;
            existing.Note = entity.Note;
            existing.DoseInstruction = entity.DoseInstruction;
            existing.authoredTime = entity.authoredTime;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _context.MedicationRequests
                .Include(mr => mr.Order)
                .FirstOrDefaultAsync(mr => mr.RequestId == id);

            if (entity == null)
            {
                throw new KeyNotFoundException($"MedicationRequest with ID {id} not found.");
            }

            _context.MedicationRequests.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
