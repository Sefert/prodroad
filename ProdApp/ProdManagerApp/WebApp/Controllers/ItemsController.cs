using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Domain.App;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ItemsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Items.GetAllAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var item = await _uow.Items.FirstOrDefaultAsync(id.Value, false);

            if (item == null) return NotFound();
            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Type,Unit,Id")] Item item)
        {
            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid();
                _uow.Items.Add(item);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var item = await _uow.Items.FirstOrDefaultAsync(id.Value, false);

            if (item == null) return NotFound();
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Type,Unit,Id")] Item item)
        {
            if (id != item.Id) return NotFound();

            if (!ModelState.IsValid || !await _uow.Items.ExistsAsync(item.Id))
                return View(item);

            _uow.Items.Update(item);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            
            var item = await _uow.Items.FirstOrDefaultAsync(id.Value, false);
            
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var item = await _uow.Items.FirstOrDefaultAsync(id);
            if (item == null) return NotFound();
            _uow.Items.Remove(item);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
