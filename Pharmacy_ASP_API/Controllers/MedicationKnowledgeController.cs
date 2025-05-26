using Microsoft.AspNetCore.Mvc;
using Pharmacy_ASP_API.Models.Entities;
using Pharmacy_ASP_API.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy_ASP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationKnowledgeController : ControllerBase
    {
        private readonly IRepositories<MedicationKnowledge> _medKnowledgeRepo;

        public MedicationKnowledgeController(IRepositories<MedicationKnowledge> medKnowledgeRepo)
        {
            _medKnowledgeRepo = medKnowledgeRepo;
        }

        // GET: api/medicationknowledge
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicationKnowledge>>> GetAll()
        {
            var medications = await _medKnowledgeRepo.GetAllAsync();
            return Ok(medications);
        }

        // GET: api/medicationknowledge/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicationKnowledge>> GetById(string id)
        {
            var medication = await _medKnowledgeRepo.GetByIdAsync(id);
            if (medication == null)
            {
                return NotFound(new { Message = $"MedicationKnowledge with ID {id} not found." });
            }
            return Ok(medication);
        }

        // POST: api/medicationknowledge
        [HttpPost]
        public async Task<ActionResult<MedicationKnowledge>> Create([FromBody] MedicationKnowledgeCreateDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new { Message = "MedicationKnowledge data is required." });
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
                var medication = new MedicationKnowledge
                {
                    MedicationId = DateTime.UtcNow.Ticks.ToString(),
                    MedicationName = dto.MedicationName,
                    ClinicalUse = dto.ClinicalUse,
                    Cost = dto.Cost,
                    ProductType = dto.ProductType,
                    Status = dto.Status,
                    Expirydate = dto.Expirydate,
                    StockId = dto.StockId
                };

                await _medKnowledgeRepo.AddAsync(medication);
                return CreatedAtAction(nameof(GetById), new { id = medication.MedicationId }, medication);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while creating the medication knowledge.", Error = ex.Message });
            }
        }

        // PUT: api/medicationknowledge/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] MedicationKnowledge medication)
        {
            if (medication == null)
            {
                return BadRequest(new { Message = "MedicationKnowledge data is required." });
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new { Message = "Validation failed.", Errors = errors });
            }

            if (id != medication.MedicationId)
            {
                return BadRequest(new { 
                    Message = "MedicationKnowledge ID mismatch.", 
                    Details = $"The ID in the URL ({id}) does not match the ID in the request body ({medication.MedicationId}). Please ensure both IDs are the same."
                });
            }

            try
            {
                await _medKnowledgeRepo.UpdateAsync(medication, id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = $"MedicationKnowledge with ID {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the medication knowledge.", Error = ex.Message });
            }
        }

        // DELETE: api/medicationknowledge/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _medKnowledgeRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = $"MedicationKnowledge with ID {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the medication knowledge.", Error = ex.Message });
            }
        }
    }

    public class MedicationKnowledgeCreateDto
    {
        [Required]
        public required string MedicationName { get; set; }

        [Required]
        public required string ClinicalUse { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Cost cannot be negative")]
        public decimal Cost { get; set; }

        [Required]
        public required string ProductType { get; set; }

        [Required]
        public required string Status { get; set; }

        [Required]
        public DateTime Expirydate { get; set; }

        [Required]
        public string StockId { get; set; }
    }

    public class MedicationKnowledgeUpdateDto
    {
        [Required]
        public required string MedicationName { get; set; }

        [Required]
        public required string ClinicalUse { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Cost cannot be negative")]
        public decimal Cost { get; set; }

        [Required]
        public required string ProductType { get; set; }

        [Required]
        public required string Status { get; set; }

        [Required]
        public DateTime Expirydate { get; set; }

        [Required]
        public string StockId { get; set; }
    }
}
