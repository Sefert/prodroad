using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain.App;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDatasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IOrderDataRepo _repo;

        public OrderDatasController(AppDbContext context)
        {
            _context = context;
            _repo = new OrderDataRepo(context);
        }

        // GET: api/OrderDatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderData>>> GetOrderDatas()
        {
            return Ok(await _repo.GetAllAsync(false));
        }

        // GET: api/OrderDatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderData>> GetOrderData(Guid id)
        {
            if (!await _repo.ExistsAsync(id))
            {
                return NotFound();
            }

            return Ok(await _repo.FirstOrDefaultAsync(id));
        }

        // PUT: api/OrderDatas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderData(Guid id, OrderData orderData)
        {
            if (id != orderData.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repo.ExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrderDatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderData>> PostOrderData(OrderData orderData)
        {
            _repo.Add(orderData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderData", new { id = orderData.Id }, orderData);
        }

        // DELETE: api/OrderDatas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderData(Guid id)
        {
            if (!await _repo.ExistsAsync(id))
            {
                return NotFound();
            }
            
            var component = await _repo.FirstOrDefaultAsync(id);
            if (component == null) return NotFound();
            _repo.Remove(component);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
