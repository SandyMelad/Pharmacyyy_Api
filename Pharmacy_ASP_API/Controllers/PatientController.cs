using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy_ASP_API.Models.Entities;
using Pharmacy_ASP_API.Repositories;

namespace Pharmacy_ASP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IRepositories<Patient> _patientRepository;

        public PatientController(IRepositories<Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }

        // GET: api/Patient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetAllPatients()
        {
            var patients = await _patientRepository.GetAllAsync();
            return Ok(patients);
        }

        // GET: api/Patient/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(Guid id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null)
            {
                return NotFound(new { Message = $"Patient with ID {id} not found." });
            }
            return Ok(patient);
        }

        // POST: api/Patient
        [HttpPost]
        public async Task<ActionResult<Patient>> CreatePatient([FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                patient.PatientId = Guid.NewGuid(); // Ensure a new ID is generated
                await _patientRepository.AddAsync(patient);
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while creating the patient.", Error = ex.Message });
            }
        }

        // PUT: api/Patient/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(Guid id, [FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patient.PatientId)
            {
                return BadRequest(new { Message = "Patient ID in URL must match Patient ID in body." });
            }

            try
            {
                await _patientRepository.UpdateAsync(patient, id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = $"Patient with ID {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the patient.", Error = ex.Message });
            }
        }

        // DELETE: api/Patient/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(Guid id)
        {
            try
            {
                await _patientRepository.DeleteAsync(id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = $"Patient with ID {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the patient.", Error = ex.Message });
            }
        }
    }
}

