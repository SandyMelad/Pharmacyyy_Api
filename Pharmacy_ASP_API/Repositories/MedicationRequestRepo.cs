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
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<MedicationRequest> GetByIdAsync(Guid id)
        {
            return await _context.MedicationRequests
                .AsNoTracking()
                .FirstOrDefaultAsync(mr => mr.RequestId == id);
        }

        public async Task AddAsync(MedicationRequest entity)
        {
            await _context.MedicationRequests.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MedicationRequest entity, Guid id)
        {
            var existing = await _context.MedicationRequests
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
            existing.OrderId = entity.OrderId;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.MedicationRequests
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
