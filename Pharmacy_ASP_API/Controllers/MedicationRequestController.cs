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
    public class MedicationRequestController : ControllerBase
    {
        private readonly IRepositories<MedicationRequest> _medRequestRepo;

        public MedicationRequestController(IRepositories<MedicationRequest> medRequestRepo)
        {
            _medRequestRepo = medRequestRepo;
        }

        // GET: api/medicationrequest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicationRequest>>> GetAll() => Ok(await _medRequestRepo.GetAllAsync());

            // GET: api/medicationrequest/{id}
            [HttpGet("{id}")]
        public async Task<ActionResult<MedicationRequest>> GetById(Guid id)
        {
            var request = await _medRequestRepo.GetByIdAsync(id);
            return request == null ? NotFound() : Ok(request);
        }

        // POST: api/medicationrequest
        [HttpPost]
        public async Task<ActionResult<MedicationRequest>> Create(MedicationRequest medRequest)
        {
            await _medRequestRepo.AddAsync(medRequest);
            return CreatedAtAction(nameof(GetById), new { id = medRequest.RequestId }, medRequest);
        }

        // PUT: api/medicationrequest/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, MedicationRequest medRequest)
        {
            await _medRequestRepo.UpdateAsync(medRequest, id);
            return Ok();
        }

        // DELETE: api/medicationrequest/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            

            await _medRequestRepo.DeleteAsync(id);
            return Ok();
        }
    }
}
