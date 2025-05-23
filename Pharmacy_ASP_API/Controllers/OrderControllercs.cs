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
        public async Task<ActionResult<Order>> GetById(Guid id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
           return order == null ? NotFound() : Ok(order);
        }

        // POST: api/order
        [HttpPost]
        public async Task<ActionResult<Order>> Create(Order order)
        {
            await _orderRepo.AddAsync(order);
            return Ok();
        }

        // PUT: api/order/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Order order)
        {
        
            await _orderRepo.UpdateAsync(order, id);
            return Ok();
        }

        // DELETE: api/order/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _orderRepo.DeleteAsync(id);
            return Ok();
        }
    }
}
