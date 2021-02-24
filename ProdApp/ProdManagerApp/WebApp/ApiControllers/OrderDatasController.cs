using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDatasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderDatasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/OrderDatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderData>>> GetOrderDatas()
        {
            return await _context.OrderDatas.ToListAsync();
        }

        // GET: api/OrderDatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderData>> GetOrderData(Guid id)
        {
            var orderData = await _context.OrderDatas.FindAsync(id);

            if (orderData == null)
            {
                return NotFound();
            }

            return orderData;
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
                if (!OrderDataExists(id))
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
            _context.OrderDatas.Add(orderData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderData", new { id = orderData.Id }, orderData);
        }

        // DELETE: api/OrderDatas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderData(Guid id)
        {
            var orderData = await _context.OrderDatas.FindAsync(id);
            if (orderData == null)
            {
                return NotFound();
            }

            _context.OrderDatas.Remove(orderData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderDataExists(Guid id)
        {
            return _context.OrderDatas.Any(e => e.Id == id);
        }
    }
}
