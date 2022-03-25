#nullable enable
using DAL.App.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.App;
using WebApp.DTO;

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
        public async Task<ActionResult<IEnumerable<ItemProcedureDTO>>> GetItemProcedures()
        {
            var dataList = (await _uow.ItemProcedures
                    .GetAllAsync())
                .Select(row => new ItemProcedureDTO()
                {
                    ProcedureId = row.ProcedureId,
                    ItemId = row.ItemId,
                    CreatedUsed= row.CreatedUsed,
                    Quantity = row.Quantity
                })
                .ToList();
            return dataList;
        }

        // GET: api/ItemProcedure/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemProcedureDTO>> GetItemProcedure(Guid id)
        {
            var dbRow = await _uow.ItemProcedures.FirstOrDefaultAsync(id);
            
            if (dbRow == null)
            {
                return NotFound();
            }
            
            var itemProcedure = new ItemProcedureDTO()
            {
                Id = dbRow.Id,
                ProcedureId = dbRow.ProcedureId,
                ItemId = dbRow.ItemId,
                CreatedUsed= dbRow.CreatedUsed,
                Quantity = dbRow.Quantity
            };
            
            return itemProcedure;
        }

        // PUT: api/ItemProcedure/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemProcedure(Guid id, ItemProcedureDTO itemProcedure)
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
        public async Task<ActionResult<ItemProcedureDTO>> PostItemProcedure(ItemProcedureDTO itemProcedure)
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
