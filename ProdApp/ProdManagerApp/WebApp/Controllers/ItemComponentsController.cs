using System;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain.App;

namespace WebApp.Controllers
{
    public class ItemComponentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IItemComponentRepo _repo;

        public ItemComponentsController(AppDbContext context)
        {
            _context = context;
            _repo = new ItemComponentRepo(_context);
        }

        // GET: ItemComponents
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAllAsync());
        }

        // GET: ItemComponents/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await _repo.FirstOrDefaultAsync((Guid) id));
        }

        // GET: ItemComponents/Create
        public IActionResult Create()
        {
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name");
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name");
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
                _repo.Add(itemComponent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name", itemComponent.ComponentId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", itemComponent.ItemId);
            return View(itemComponent);
        }

        // GET: ItemComponents/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemComponent = await _repo.FirstOrDefaultAsync((Guid) id);

            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name", itemComponent.ComponentId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", itemComponent.ItemId);
            return View(itemComponent);
        }

        // POST: ItemComponents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ComponentId,ItemId,Id")] ItemComponent itemComponent)
        {
            if (id != itemComponent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(itemComponent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repo.ExistsAsync(id).Result)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name", itemComponent.ComponentId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", itemComponent.ItemId);
            return View(itemComponent);
        }

        // GET: ItemComponents/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await _repo.FirstOrDefaultAsync((Guid) id));
        }

        // POST: ItemComponents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var itemComponent = await _repo.FirstOrDefaultAsync(id);
            _repo.Remove(itemComponent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
