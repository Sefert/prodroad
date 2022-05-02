#nullable enable
using BLL.App.Contracts;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public PriceController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Price
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Price>>> GetPrices()
        {
            var dataList = (await _bll.Prices
                    .GetAllAsync())
                .ToList();
            return dataList;
        }

        // GET: api/Price/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Price>> GetPrice(Guid id)
        {
            var price = await _bll.Prices.FirstOrDefaultAsync(id);
            
            if (price == null)
            {
                return NotFound();
            }

            return price;
        }

        // PUT: api/Price/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrice(Guid id, Price price)
        {
            if (id != price.Id)
            {
                return BadRequest();
            }

            var dbRow = await _bll.Prices.FirstOrDefaultAsync(id);
            
            if (dbRow == null) {return NotFound();}

            _bll.Prices.ModifyState(dbRow);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceExists(id))
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

        // POST: api/Price
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Price>> PostPrice(Price price)
        {
            var dbRow = new Price()
            {
                ItemId = price.ItemId,
                ItemWarehouseId = price.ItemWarehouseId,
                PureCost = price.PureCost
            };
            _bll.Prices.Add(dbRow);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPrice", new { id = price.Id }, price);
        }

        // DELETE: api/Price/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrice(Guid id)
        {
            var price = await _bll.Prices.FirstOrDefaultAsync(id);
            if (price == null)
            {
                return NotFound();
            }

            _bll.Prices.Remove(price);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool PriceExists(Guid id)
        {
            return _bll.Prices.Exists(id);
        }
    }
}
