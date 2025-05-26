using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy_ASP_API.Models.Entities;
using Pharmacy_ASP_API.Repositories;

namespace Pharmacy_ASP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IRepositories<Stock> _stockRepository;
        public StockController(IRepositories<Stock> stockRepository)
        {
            _stockRepository = stockRepository;
        }

        // GET: api/Stock
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stock>>> GetAllStocks()
        {
            var stocks = await _stockRepository.GetAllAsync();
            return Ok(stocks);
        }

        // GET: api/Stock/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Stock>> GetStock(string id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound(new { Message = $"Stock with ID {id} not found." });
            }
            return Ok(stock);
        }

        // POST: api/Stock
        [HttpPost]
        public async Task<ActionResult<Stock>> CreateStock([FromBody] Stock stock)
        {
            if (stock == null)
            {
                return BadRequest(new { Message = "Stock data is required." });
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
                // Initialize the stock
                stock.StockId = DateTime.UtcNow.Ticks.ToString();
                stock.MedicationKnowledges = new List<MedicationKnowledge>();
                stock.Orders = new List<Order>();

                await _stockRepository.AddAsync(stock);
                return CreatedAtAction(nameof(GetStock), new { id = stock.StockId }, stock);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    Message = "An error occurred while creating the stock.", 
                    Error = ex.Message,
                    InnerError = ex.InnerException?.Message 
                });
            }
        }

        // PUT: api/Stock/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(string id, [FromBody] Stock stock)
        {
            if (stock == null)
            {
                return BadRequest(new { Message = "Stock data is required." });
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new { Message = "Validation failed.", Errors = errors });
            }

            if (id != stock.StockId)
            {
                return BadRequest(new { 
                    Message = "Stock ID mismatch.", 
                    Details = $"The ID in the URL ({id}) does not match the ID in the request body ({stock.StockId}). Please ensure both IDs are the same."
                });
            }

            try
            {
                await _stockRepository.UpdateAsync(stock, id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = $"Stock with ID {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the stock.", Error = ex.Message });
            }
        }

        // DELETE: api/Stock/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(string id)
        {
            try
            {
                await _stockRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = $"Stock with ID {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the stock.", Error = ex.Message });
            }
        }
    }
}

