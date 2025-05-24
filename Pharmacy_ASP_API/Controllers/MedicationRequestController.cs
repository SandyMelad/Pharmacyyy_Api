using Microsoft.AspNetCore.Mvc;
using Pharmacy_ASP_API.Models.Entities;
using Pharmacy_ASP_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy_ASP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationRequestController : ControllerBase
    {
        private readonly IRepositories<MedicationRequest> _medRequestRepo;

        public MedicationRequestController(IRepositories<MedicationRequest> medRequestRepo)
        {
            _medRequestRepo = medRequestRepo;
        }

        // GET: api/medicationrequest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicationRequest>>> GetAll()
        {
            var requests = await _medRequestRepo.GetAllAsync();
            return Ok(requests);
        }

        // GET: api/medicationrequest/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicationRequest>> GetById(Guid id)
        {
            var request = await _medRequestRepo.GetByIdAsync(id);
            if (request == null)
            {
                return NotFound(new { Message = $"MedicationRequest with ID {id} not found." });
            }
            return Ok(request);
        }

        // POST: api/medicationrequest
        [HttpPost]
        public async Task<ActionResult<MedicationRequest>> Create([FromBody] MedicationRequest medRequest)
        {
            if (medRequest == null)
            {
                return BadRequest(new { Message = "MedicationRequest data is required." });
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new { Message = "Validation failed.", Errors = errors });
            }

            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(medRequest.DrOutBed))
                {
                    return BadRequest(new { Message = "DrOutBed is required." });
                }

                if (string.IsNullOrWhiteSpace(medRequest.DrInBed))
                {
                    return BadRequest(new { Message = "DrInBed is required." });
                }

                if (string.IsNullOrWhiteSpace(medRequest.Status))
                {
                    return BadRequest(new { Message = "Status is required." });
                }

                if (string.IsNullOrWhiteSpace(medRequest.DoseInstruction))
                {
                    return BadRequest(new { Message = "DoseInstruction is required." });
                }

                if (string.IsNullOrWhiteSpace(medRequest.authoredTime))
                {
                    return BadRequest(new { Message = "AuthoredTime is required." });
                }

                // Validate OrderId


                // Set default values
                medRequest.RequestId = Guid.NewGuid();
                medRequest.StatusTime = DateTime.UtcNow;

                await _medRequestRepo.AddAsync(medRequest);
                return CreatedAtAction(nameof(GetById), new { id = medRequest.RequestId }, medRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    Message = "An error occurred while creating the medication request.", 
                    Error = ex.Message,
                    InnerError = ex.InnerException?.Message 
                });
            }
        }

        // PUT: api/medicationrequest/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MedicationRequest medRequest)
        {
            if (medRequest == null)
            {
                return BadRequest(new { Message = "MedicationRequest data is required." });
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new { Message = "Validation failed.", Errors = errors });
            }

            if (id != medRequest.RequestId)
            {
                return BadRequest(new { 
                    Message = "MedicationRequest ID mismatch.", 
                    Details = $"The ID in the URL ({id}) does not match the ID in the request body ({medRequest.RequestId}). Please ensure both IDs are the same."
                });
            }

            try
            {
                await _medRequestRepo.UpdateAsync(medRequest, id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = $"MedicationRequest with ID {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    Message = "An error occurred while updating the medication request.", 
                    Error = ex.Message,
                    InnerError = ex.InnerException?.Message 
                });
            }
        }

        // DELETE: api/medicationrequest/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _medRequestRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = $"MedicationRequest with ID {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    Message = "An error occurred while deleting the medication request.", 
                    Error = ex.Message,
                    InnerError = ex.InnerException?.Message 
                });
            }
        }
    }
}
