#nullable enable
using BLL.App.Contracts;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemProcedureController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ItemProcedureController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ItemProcedure
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemProcedure>>> GetItemProcedures()
        {
            var dataList = (await _bll.ItemProcedures
                    .GetAllAsync())
                .ToList();
            return dataList;
        }

        // GET: api/ItemProcedure/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemProcedure>> GetItemProcedure(Guid id)
        {
            var itemProcedure = await _bll.ItemProcedures.FirstOrDefaultAsync(id);
            
            if (itemProcedure == null)
            {
                return NotFound();
            }

            return itemProcedure;
        }

        // PUT: api/ItemProcedure/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemProcedure(Guid id, ItemProcedure itemProcedure)
        {
            if (id != itemProcedure.Id)
            {
                return BadRequest();
            }

            var dbRow = await _bll.ItemProcedures.FirstOrDefaultAsync(id);
            
            if (dbRow == null) {return NotFound();}

            _bll.ItemProcedures.ModifyState(dbRow);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemProcedureExists(id))
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

        // POST: api/ItemProcedure
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemProcedure>> PostItemProcedure(ItemProcedure itemProcedure)
        {
            var dbRow = new ItemProcedure()
            {
                ProcedureId = itemProcedure.ProcedureId,
                ItemId = itemProcedure.ItemId,
                CreatedUsed= itemProcedure.CreatedUsed,
                Quantity = itemProcedure.Quantity
            };     
            
            _bll.ItemProcedures.Add(dbRow);
            
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetItemProcedure", new { id = itemProcedure.Id }, itemProcedure);
        }

        // DELETE: api/ItemProcedure/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemProcedure(Guid id)
        {
            var itemProcedure = await _bll.ItemProcedures.FirstOrDefaultAsync(id);
            if (itemProcedure == null)
            {
                return NotFound();
            }

            _bll.ItemProcedures.Remove(itemProcedure);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemProcedureExists(Guid id)
        {
            return _bll.ItemProcedures.Exists(id);
        }
    }
}
