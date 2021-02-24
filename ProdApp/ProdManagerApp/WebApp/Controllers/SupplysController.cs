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
    public class SupplysController : Controller
    {
        private readonly AppDbContext _context;

        public SupplysController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Supplys
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Supplys.Include(s => s.Component).Include(s => s.Item).Include(s => s.Warehouse);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Supplys/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supply = await _context.Supplys
                .Include(s => s.Component)
                .Include(s => s.Item)
                .Include(s => s.Warehouse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supply == null)
            {
                return NotFound();
            }

            return View(supply);
        }

        // GET: Supplys/Create
        public IActionResult Create()
        {
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name");
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name");
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "Address");
            return View();
        }

        // POST: Supplys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Quantity,ItemId,ComponentId,WarehouseId,Id")] Supply supply)
        {
            if (ModelState.IsValid)
            {
                supply.Id = Guid.NewGuid();
                _context.Add(supply);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name", supply.ComponentId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", supply.ItemId);
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "Address", supply.WarehouseId);
            return View(supply);
        }

        // GET: Supplys/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supply = await _context.Supplys.FindAsync(id);
            if (supply == null)
            {
                return NotFound();
            }
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name", supply.ComponentId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", supply.ItemId);
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "Address", supply.WarehouseId);
            return View(supply);
        }

        // POST: Supplys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Quantity,ItemId,ComponentId,WarehouseId,Id")] Supply supply)
        {
            if (id != supply.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplyExists(supply.Id))
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
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name", supply.ComponentId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", supply.ItemId);
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "Address", supply.WarehouseId);
            return View(supply);
        }

        // GET: Supplys/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supply = await _context.Supplys
                .Include(s => s.Component)
                .Include(s => s.Item)
                .Include(s => s.Warehouse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supply == null)
            {
                return NotFound();
            }

            return View(supply);
        }

        // POST: Supplys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supply = await _context.Supplys.FindAsync(id);
            _context.Supplys.Remove(supply);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplyExists(Guid id)
        {
            return _context.Supplys.Any(e => e.Id == id);
        }
    }
}
