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
    public class FinanceController : ControllerBase
    {
        private readonly IRepositories<Finance> _financeRepo;

        public FinanceController(IRepositories<Finance> financeRepo)
        {
            _financeRepo = financeRepo;
        }

        // GET: api/finance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Finance>>> GetAll() => Ok(await _financeRepo.GetAllAsync());


        // GET: api/finance/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Finance>> GetById(Guid id)
        {
            var finance = await _financeRepo.GetByIdAsync(id);
             return finance == null ? NotFound() : Ok(finance);
        }

        // POST: api/finance
        [HttpPost]
        public async Task<ActionResult<Finance>> Create([FromBody] Finance finance)
        {
            
            await _financeRepo.AddAsync(finance);
            return Ok();
        }

        // PUT: api/finance/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Finance finance)
        {
          
            await _financeRepo.UpdateAsync(finance, id);
            return Ok();
        }

        // DELETE: api/finance/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _financeRepo.DeleteAsync(id);
            return Ok();
        }
    }
}
