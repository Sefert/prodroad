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
    public class ProductionsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Productions
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Productions.Include(p => p.Component).Include(p => p.Item).Include(p => p.ProductionMeta);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Productions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var production = await _context.Productions
                .Include(p => p.Component)
                .Include(p => p.Item)
                .Include(p => p.ProductionMeta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (production == null)
            {
                return NotFound();
            }

            return View(production);
        }

        // GET: Productions/Create
        public IActionResult Create()
        {
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name");
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name");
            ViewData["ProductionMetaId"] = new SelectList(_context.ProductionMetas, "Id", "Line");
            return View();
        }

        // POST: Productions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Quantity,UsedProduced,ProductionMetaId,ComponentId,ItemId,StartDate,EndDate,StartTime,EndTime,Id")] Production production)
        {
            if (ModelState.IsValid)
            {
                production.Id = Guid.NewGuid();
                _context.Add(production);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name", production.ComponentId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", production.ItemId);
            ViewData["ProductionMetaId"] = new SelectList(_context.ProductionMetas, "Id", "Line", production.ProductionMetaId);
            return View(production);
        }

        // GET: Productions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var production = await _context.Productions.FindAsync(id);
            if (production == null)
            {
                return NotFound();
            }
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name", production.ComponentId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", production.ItemId);
            ViewData["ProductionMetaId"] = new SelectList(_context.ProductionMetas, "Id", "Line", production.ProductionMetaId);
            return View(production);
        }

        // POST: Productions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Quantity,UsedProduced,ProductionMetaId,ComponentId,ItemId,StartDate,EndDate,StartTime,EndTime,Id")] Production production)
        {
            if (id != production.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(production);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductionExists(production.Id))
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
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name", production.ComponentId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", production.ItemId);
            ViewData["ProductionMetaId"] = new SelectList(_context.ProductionMetas, "Id", "Line", production.ProductionMetaId);
            return View(production);
        }

        // GET: Productions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var production = await _context.Productions
                .Include(p => p.Component)
                .Include(p => p.Item)
                .Include(p => p.ProductionMeta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (production == null)
            {
                return NotFound();
            }

            return View(production);
        }

        // POST: Productions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var production = await _context.Productions.FindAsync(id);
            _context.Productions.Remove(production);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductionExists(Guid id)
        {
            return _context.Productions.Any(e => e.Id == id);
        }
    }
}
