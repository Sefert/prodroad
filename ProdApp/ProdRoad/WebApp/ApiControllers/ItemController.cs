#nullable enable
using DAL.App.Contracts;
using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;


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
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            var dataList = (await _uow.Items
                    .GetAllAsync(User.GetUserId()))
                .ToList();
            return dataList;
        }

        // GET: api/Item/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(Guid id)
        {
            var item = await _uow.Items.FirstOrDefaultAsync(User.GetUserId(),id);
            
            if (item== null)
            {
                return NotFound();
            }
            
            return item;
        }

        // PUT: api/Item/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(Guid id, Item item)
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
        public async Task<ActionResult<Item>> PostItem(Item item)
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
