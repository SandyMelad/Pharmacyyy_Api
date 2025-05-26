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
    public class OrderController : ControllerBase
    {
        private readonly IRepositories<Order> _orderRepo;

        public OrderController(IRepositories<Order> orderRepo)
        {
            _orderRepo = orderRepo;
        }

        // GET: api/order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll() => Ok(await _orderRepo.GetAllAsync());

        // GET: api/order/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetById(string id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            return order == null ? NotFound(new { Message = $"Order with ID {id} not found." }) : Ok(order);
        }

        // POST: api/order
        [HttpPost]
        public async Task<ActionResult<Order>> Create([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(order.PatientId))
            {
                return BadRequest(new { Message = "Patient ID is required." });
            }

            try
            {
                order.OrderId = DateTime.UtcNow.Ticks.ToString();
                order.OrderTime = DateTime.UtcNow;
                await _orderRepo.AddAsync(order);
                return CreatedAtAction(nameof(GetById), new { id = order.OrderId }, order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while creating the order.", Error = ex.Message });
            }
        }

        // PUT: api/order/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.OrderId)
            {
                return BadRequest(new { Message = "Order ID in URL must match Order ID in body." });
            }

            try
            {
                await _orderRepo.UpdateAsync(order, id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = $"Order with ID {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the order.", Error = ex.Message });
            }
        }

        // DELETE: api/order/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _orderRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = $"Order with ID {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the order.", Error = ex.Message });
            }
        }
    }
}
