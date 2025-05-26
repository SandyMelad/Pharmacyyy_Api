using Pharmacy_ASP_API.Models;
using Microsoft.EntityFrameworkCore;
using Pharmacy_ASP_API.Models.Entities;

namespace Pharmacy_ASP_API.Repositories
{
    public class PatientRepo : IRepositories<Patient>
    {
        private readonly PharmacyDbContext _context;

        public PatientRepo(PharmacyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _context.Patients
                .Include(p => p.Orders)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Patient> GetByIdAsync(string id)
        {
            return await _context.Patients
                .Include(p => p.Orders)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PatientId == id);
        }

        public async Task AddAsync(Patient entity)
        {
            await _context.Patients.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Patient entity, string id)
        {
            var existingPatient = await _context.Patients
                .FirstOrDefaultAsync(p => p.PatientId == id);

            if (existingPatient == null)
            {
                throw new KeyNotFoundException($"Patient with ID {id} not found.");
            }

            // Update scalar properties
            existingPatient.PatientName = entity.PatientName;
            existingPatient.PhoneNo = entity.PhoneNo;
            existingPatient.Address = entity.Address;
            existingPatient.DateOfBirth = entity.DateOfBirth;
            existingPatient.Gender = entity.Gender;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.PatientId == id);

            if (patient == null)
            {
                throw new KeyNotFoundException($"Patient with ID {id} not found.");
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }
    }
}

