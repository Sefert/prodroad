#nullable enable
using DAL.App.Contracts;
using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemProcedureController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public ItemProcedureController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/ItemProcedure
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemProcedure>>> GetItemProcedures()
        {
            var dataList = (await _uow.ItemProcedures
                    .GetAllAsync())
                .ToList();
            return dataList;
        }

        // GET: api/ItemProcedure/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemProcedure>> GetItemProcedure(Guid id)
        {
            var itemProcedure = await _uow.ItemProcedures.FirstOrDefaultAsync(id);
            
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

            var dbRow = await _uow.ItemProcedures.FirstOrDefaultAsync(id);
            
            if (dbRow == null) {return NotFound();}

            _uow.ItemProcedures.ModifyState(dbRow);

            try
            {
                await _uow.SaveChangesAsync();
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
            
            _uow.ItemProcedures.Add(dbRow);
            
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetItemProcedure", new { id = itemProcedure.Id }, itemProcedure);
        }

        // DELETE: api/ItemProcedure/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemProcedure(Guid id)
        {
            var itemProcedure = await _uow.ItemProcedures.FirstOrDefaultAsync(id);
            if (itemProcedure == null)
            {
                return NotFound();
            }

            _uow.ItemProcedures.Remove(itemProcedure);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemProcedureExists(Guid id)
        {
            return _uow.ItemProcedures.Exists(id);
        }
    }
}
