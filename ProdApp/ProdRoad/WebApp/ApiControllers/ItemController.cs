#nullable enable
using DAL.App.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using WebApp.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="admin,manager,user",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public ItemController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems()
        {
            var dataList = (await _uow.Items
                    .GetAllAsync(User.GetUserId()))
                .Select(row => new ItemDTO()
                {
                    Id = row.Id,
                    ItemId = row.ItemId,
                    AppUserId = row.AppUserId,
                    Name = row.Name,
                    Type = row.Type,
                    Unit = row.Unit,
                    Quantity = row.Quantity
                })
                .ToList();
            return dataList;
        }

        // GET: api/Item/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItem(Guid id)
        {
            var dbRow = await _uow.Items.FirstOrDefaultAsync(User.GetUserId(),id);
            
            if (dbRow == null)
            {
                return NotFound();
            }
            
            var item = new ItemDTO()
            {
                Id = dbRow.Id,
                AppUserId = dbRow.AppUserId,
                Name = dbRow.Name,
                Type = dbRow.Type,
                Unit = dbRow.Unit,
                Quantity = dbRow.Quantity
            };

            return item;
        }

        // PUT: api/Item/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(Guid id, ItemDTO item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            var dbRow = await _uow.Items.FirstOrDefaultAsync(User.GetUserId(),id);
            
            if (dbRow == null) {return NotFound();}

            dbRow.Name!.SetTranslation(item.Name!);
            dbRow.Type!.SetTranslation(item.Type!);
            dbRow.Unit!.SetTranslation(item.Unit!);

            _uow.Items.ModifyState(dbRow);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Item
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemDTO>> PostItem(ItemDTO item)
        {
            var dbRow = new Item()
            {
                AppUserId = item.AppUserId,
                Quantity = item.Quantity
            };
            
            dbRow.Name!.SetTranslation(item.Name!);
            dbRow.Type!.SetTranslation(item.Type!);
            dbRow.Unit!.SetTranslation(item.Unit!);
            
            _uow.Items.Add(dbRow);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        // DELETE: api/Item/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _uow.Items.FirstOrDefaultAsync(User.GetUserId(),id);
            if (item == null)
            {
                return NotFound();
            }

            _uow.Items.Remove(item);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(Guid id)
        {
            return _uow.Items.Exists(id);
        }
    }
}
