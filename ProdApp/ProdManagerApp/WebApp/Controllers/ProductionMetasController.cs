using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class ProductionMetasController : Controller
    {
        private readonly AppDbContext _context;

        public ProductionMetasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProductionMetas
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ProductionMetas.Include(p => p.Supply);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ProductionMetas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionMeta = await _context.ProductionMetas
                .Include(p => p.Supply)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productionMeta == null)
            {
                return NotFound();
            }

            return View(productionMeta);
        }

        // GET: ProductionMetas/Create
        public IActionResult Create()
        {
            ViewData["SupplyId"] = new SelectList(_context.Supplys, "Id", "Id");
            return View();
        }

        // POST: ProductionMetas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Line,RealStartDate,RealEndDate,SupplyId,ApplicationUserId,StartDate,EndDate,StartTime,EndTime,Id")] ProductionMeta productionMeta)
        {
            if (ModelState.IsValid)
            {
                productionMeta.Id = Guid.NewGuid();
                _context.Add(productionMeta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplyId"] = new SelectList(_context.Supplys, "Id", "Id", productionMeta.SupplyId);
            return View(productionMeta);
        }

        // GET: ProductionMetas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionMeta = await _context.ProductionMetas.FindAsync(id);
            if (productionMeta == null)
            {
                return NotFound();
            }
            ViewData["SupplyId"] = new SelectList(_context.Supplys, "Id", "Id", productionMeta.SupplyId);
            return View(productionMeta);
        }

        // POST: ProductionMetas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Line,RealStartDate,RealEndDate,SupplyId,ApplicationUserId,StartDate,EndDate,StartTime,EndTime,Id")] ProductionMeta productionMeta)
        {
            if (id != productionMeta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productionMeta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductionMetaExists(productionMeta.Id))
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
            ViewData["SupplyId"] = new SelectList(_context.Supplys, "Id", "Id", productionMeta.SupplyId);
            return View(productionMeta);
        }

        // GET: ProductionMetas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionMeta = await _context.ProductionMetas
                .Include(p => p.Supply)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productionMeta == null)
            {
                return NotFound();
            }

            return View(productionMeta);
        }

        // POST: ProductionMetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var productionMeta = await _context.ProductionMetas.FindAsync(id);
            _context.ProductionMetas.Remove(productionMeta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductionMetaExists(Guid id)
        {
            return _context.ProductionMetas.Any(e => e.Id == id);
        }
    }
}
