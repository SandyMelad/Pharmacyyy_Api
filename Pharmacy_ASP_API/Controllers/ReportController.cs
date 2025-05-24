using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy_ASP_API.Models.Entities;
using Pharmacy_ASP_API.Repositories;

namespace Pharmacy_ASP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IRepositories<Report> _reportRepository;
        public ReportController(IRepositories<Report> reportRepository)
        {
            _reportRepository = reportRepository;
        }

        // GET: api/Report
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetAllReports()
        {
            var reports = await _reportRepository.GetAllAsync();
            return Ok(reports);
        }

        // GET: api/Report/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReport(Guid id)
        {
            var report = await _reportRepository.GetByIdAsync(id);
            if (report == null)
            {
                return NotFound(new { Message = $"Report with ID {id} not found." });
            }
            return Ok(report);
        }

        // POST: api/Report
        [HttpPost]
        public async Task<ActionResult<Report>> CreateReport([FromBody] Report report)
        {
            if (report == null)
            {
                return BadRequest(new { Message = "Report data is required." });
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
                // Initialize the report
                report.ReportId = Guid.NewGuid();
                report.Orders = new List<Order>();

                await _reportRepository.AddAsync(report);
                return CreatedAtAction(nameof(GetReport), new { id = report.ReportId }, report);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    Message = "An error occurred while creating the report.", 
                    Error = ex.Message,
                    InnerError = ex.InnerException?.Message 
                });
            }
        }

        // PUT: api/Report/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport(Guid id, [FromBody] Report report)
        {
            if (report == null)
            {
                return BadRequest(new { Message = "Report data is required." });
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new { Message = "Validation failed.", Errors = errors });
            }

            if (id != report.ReportId)
            {
                return BadRequest(new { 
                    Message = "Report ID mismatch.", 
                    Details = $"The ID in the URL ({id}) does not match the ID in the request body ({report.ReportId}). Please ensure both IDs are the same."
                });
            }

            try
            {
                await _reportRepository.UpdateAsync(report, id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = $"Report with ID {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    Message = "An error occurred while updating the report.", 
                    Error = ex.Message,
                    InnerError = ex.InnerException?.Message 
                });
            }
        }

        // DELETE: api/Report/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(Guid id)
        {
            try
            {
                await _reportRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = $"Report with ID {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    Message = "An error occurred while deleting the report.", 
                    Error = ex.Message,
                    InnerError = ex.InnerException?.Message 
                });
            }
        }
    }
}

