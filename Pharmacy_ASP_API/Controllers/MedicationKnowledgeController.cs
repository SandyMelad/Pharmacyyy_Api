using Microsoft.AspNetCore.Mvc;
using Pharmacy_ASP_API.Models.Entities;
using Pharmacy_ASP_API.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IEnumerable<MedicationKnowledge>>> GetAll() => Ok(await _medKnowledgeRepo.GetAllAsync());


        // GET: api/medicationknowledge/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicationKnowledge>> GetById(Guid id)
        {
            var med = await _medKnowledgeRepo.GetByIdAsync(id);
           return med == null ? NotFound() : Ok(med);
        }

        // POST: api/medicationknowledge
        [HttpPost]
        public async Task<ActionResult<MedicationKnowledge>> Create([FromBody] MedicationKnowledge medKnowledge)
        {
        
            await _medKnowledgeRepo.AddAsync(medKnowledge);
            return Ok();
        }

        // PUT: api/medicationknowledge/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MedicationKnowledge medKnowledge)
        {
            await _medKnowledgeRepo.UpdateAsync(medKnowledge, id);
            return Ok();
        }

        // DELETE: api/medicationknowledge/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            await _medKnowledgeRepo.DeleteAsync(id);
            return Ok();
        }
    }
}
