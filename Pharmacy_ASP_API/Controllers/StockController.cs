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
        public async Task<ActionResult<Stock>> GetStock(Guid id)
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                stock.StockId = Guid.NewGuid(); // Ensure a new ID is generated
                await _stockRepository.AddAsync(stock);
                return CreatedAtAction(nameof(GetStock), new { id = stock.StockId }, stock);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while creating the stock.", Error = ex.Message });
            }
        }

        // PUT: api/Stock/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(Guid id, [FromBody] Stock stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stock.StockId)
            {
                return BadRequest(new { Message = "Stock ID in URL must match Stock ID in body." });
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
        public async Task<IActionResult> DeleteStock(Guid id)
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

