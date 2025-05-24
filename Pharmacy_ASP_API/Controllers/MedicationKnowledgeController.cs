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
        public async Task<ActionResult<MedicationKnowledge>> GetById(Guid id)
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
                // Validate required fields
                if (string.IsNullOrWhiteSpace(dto.MedicationName))
                {
                    return BadRequest(new { Message = "MedicationName is required." });
                }

                if (string.IsNullOrWhiteSpace(dto.ClinicalUse))
                {
                    return BadRequest(new { Message = "ClinicalUse is required." });
                }

                if (string.IsNullOrWhiteSpace(dto.ProductType))
                {
                    return BadRequest(new { Message = "ProductType is required." });
                }

                if (string.IsNullOrWhiteSpace(dto.Status))
                {
                    return BadRequest(new { Message = "Status is required." });
                }

                // Validate StockId
                if (dto.StockId == Guid.Empty)
                {
                    return BadRequest(new { Message = "Valid StockId is required." });
                }

                var medKnowledge = new MedicationKnowledge
                {
                    MedicationId = Guid.NewGuid(),
                    MedicationName = dto.MedicationName,
                    ClinicalUse = dto.ClinicalUse,
                    Cost = dto.Cost,
                    ProductType = dto.ProductType,
                    Status = dto.Status,
                    Expirydate = dto.Expirydate,
                    StockId = dto.StockId,
                    Orders = new List<Order>()
                };

                await _medKnowledgeRepo.AddAsync(medKnowledge);
                return CreatedAtAction(nameof(GetById), new { id = medKnowledge.MedicationId }, medKnowledge);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    Message = "An error occurred while creating the medication knowledge.", 
                    Error = ex.Message,
                    InnerError = ex.InnerException?.Message 
                });
            }
        }

        // PUT: api/medicationknowledge/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MedicationKnowledgeUpdateDto dto)
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
                var medKnowledge = new MedicationKnowledge
                {
                    MedicationId = id,
                    MedicationName = dto.MedicationName,
                    ClinicalUse = dto.ClinicalUse,
                    Cost = dto.Cost,
                    ProductType = dto.ProductType,
                    Status = dto.Status,
                    Expirydate = dto.Expirydate,
                    StockId = dto.StockId
                };

                await _medKnowledgeRepo.UpdateAsync(medKnowledge, id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = $"MedicationKnowledge with ID {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    Message = "An error occurred while updating the medication knowledge.", 
                    Error = ex.Message,
                    InnerError = ex.InnerException?.Message 
                });
            }
        }

        // DELETE: api/medicationknowledge/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
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
                return StatusCode(500, new { 
                    Message = "An error occurred while deleting the medication knowledge.", 
                    Error = ex.Message,
                    InnerError = ex.InnerException?.Message 
                });
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
        public Guid StockId { get; set; }
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
        public Guid StockId { get; set; }
    }
}
