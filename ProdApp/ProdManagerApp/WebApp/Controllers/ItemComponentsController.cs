using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.Controllers
{
    public class ItemComponentsController : Controller
    {
        private readonly AppDbContext _context;

        public ItemComponentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ItemComponents
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ItemComponents.Include(i => i.Component).Include(i => i.Item);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ItemComponents/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemComponent = await _context.ItemComponents
                .Include(i => i.Component)
                .Include(i => i.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemComponent == null)
            {
                return NotFound();
            }

            return View(itemComponent);
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
                _context.Add(itemComponent);
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

            var itemComponent = await _context.ItemComponents.FindAsync(id);
            if (itemComponent == null)
            {
                return NotFound();
            }
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
                    _context.Update(itemComponent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemComponentExists(itemComponent.Id))
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

            var itemComponent = await _context.ItemComponents
                .Include(i => i.Component)
                .Include(i => i.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemComponent == null)
            {
                return NotFound();
            }

            return View(itemComponent);
        }

        // POST: ItemComponents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var itemComponent = await _context.ItemComponents.FindAsync(id);
            _context.ItemComponents.Remove(itemComponent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemComponentExists(Guid id)
        {
            return _context.ItemComponents.Any(e => e.Id == id);
        }
    }
}
