using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ProductionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductionRepo _repo;

        public ProductionsController(AppDbContext context)
        {
            _context = context;
            _repo = new ProductionRepo(context);
        }

        // GET: Productions
        public async Task<IActionResult> Index()
        {
            
            return View(await _repo.GetAllAsync());
        }

        // GET: Productions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await _repo.FirstOrDefaultAsync((Guid) id));
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
                _repo.Add(production);
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

            var production = await _repo.FirstOrDefaultAsync((Guid) id);

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
                    _repo.Update(production);
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

            return View(await _repo.FirstOrDefaultAsync((Guid) id));
        }

        // POST: Productions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var production = await _repo.FirstOrDefaultAsync(id);
            _repo.Remove(production);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
