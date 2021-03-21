using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.App;

namespace WebApp.Controllers
{
    public class ItemComponentsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ItemComponentsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: ItemComponents
        public async Task<IActionResult> Index()
        {
            return View(await _uow.ItemComponents.GetAllAsync());
        }

        // GET: ItemComponents/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var itemComponent = await _uow.ItemComponents.FirstOrDefaultAsync(id.Value, false);

            if (itemComponent == null) return NotFound();
            return View(itemComponent);
        }

        // GET: ItemComponents/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ComponentId"] = new SelectList(await _uow.Components.GetAllAsync(), "Id", "Name");
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: ItemComponents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComponentId,ItemId,Id")] ItemComponent itemComponent)
        {
            if (ModelState.IsValid)
            {
                itemComponent.Id = Guid.NewGuid();
                _uow.ItemComponents.Add(itemComponent);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(await _uow.Components.GetAllAsync(), "Id", "Name", itemComponent.ComponentId);
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(), "Id", "Name", itemComponent.ItemId);
            return View(itemComponent);
        }

        // GET: ItemComponents/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var itemComponent = await _uow.ItemComponents.FirstOrDefaultAsync(id.Value, false);

            if (itemComponent == null) return NotFound();

            ViewData["ComponentId"] = new SelectList(await _uow.Components.GetAllAsync(), "Id", "Name",
                itemComponent.ComponentId);
            ViewData["ItemId"] =
                new SelectList(await _uow.Components.GetAllAsync(), "Id", "Name", itemComponent.ItemId);
            return View(itemComponent);
        }

        // POST: ItemComponents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ComponentId,ItemId,Id")] ItemComponent itemComponent)
        {
            if (id != itemComponent.Id) return NotFound();

            if (!ModelState.IsValid || !await _uow.ItemComponents.ExistsAsync(itemComponent.Id))
                return View(itemComponent);

            ViewData["ComponentId"] = new SelectList(await _uow.Components.GetAllAsync(), "Id", "Name",
                itemComponent.ComponentId);
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(), "Id", "Name", itemComponent.ItemId);
            return View(itemComponent);
        }

        // GET: ItemComponents/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var itemComponent = await _uow.ItemComponents.FirstOrDefaultAsync(id.Value, false);

            if (itemComponent == null) return NotFound();
            return View(itemComponent);
        }

        // POST: ItemComponents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var itemComponent = await _uow.ItemComponents.FirstOrDefaultAsync(id);
            if (itemComponent == null) return NotFound();
            _uow.ItemComponents.Remove(itemComponent);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
