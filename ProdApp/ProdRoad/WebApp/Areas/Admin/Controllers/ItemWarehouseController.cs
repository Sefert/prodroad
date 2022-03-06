#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ItemWarehouseController : Controller
    {
        private readonly AppDbContext _context;

        public ItemWarehouseController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ItemWarehouse
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ItemWarehouses.Include(i => i.Item).Include(i => i.Warehouse);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/ItemWarehouse/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemWarehouse = await _context.ItemWarehouses
                .Include(i => i.Item)
                .Include(i => i.Warehouse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemWarehouse == null)
            {
                return NotFound();
            }

            return View(itemWarehouse);
        }

        // GET: Admin/ItemWarehouse/Create
        public IActionResult Create()
        {
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name");
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "Address");
            return View();
        }

        // POST: Admin/ItemWarehouse/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,WarehouseId,Quantity,UpdatedAt,UpdatedId,Id")] ItemWarehouse itemWarehouse)
        {
            if (ModelState.IsValid)
            {
                itemWarehouse.Id = Guid.NewGuid();
                _context.Add(itemWarehouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", itemWarehouse.ItemId);
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "Address", itemWarehouse.WarehouseId);
            return View(itemWarehouse);
        }

        // GET: Admin/ItemWarehouse/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemWarehouse = await _context.ItemWarehouses.FindAsync(id);
            if (itemWarehouse == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", itemWarehouse.ItemId);
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "Address", itemWarehouse.WarehouseId);
            return View(itemWarehouse);
        }

        // POST: Admin/ItemWarehouse/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ItemId,WarehouseId,Quantity,UpdatedAt,UpdatedId,Id")] ItemWarehouse itemWarehouse)
        {
            if (id != itemWarehouse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemWarehouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemWarehouseExists(itemWarehouse.Id))
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
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", itemWarehouse.ItemId);
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "Address", itemWarehouse.WarehouseId);
            return View(itemWarehouse);
        }

        // GET: Admin/ItemWarehouse/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemWarehouse = await _context.ItemWarehouses
                .Include(i => i.Item)
                .Include(i => i.Warehouse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemWarehouse == null)
            {
                return NotFound();
            }

            return View(itemWarehouse);
        }

        // POST: Admin/ItemWarehouse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var itemWarehouse = await _context.ItemWarehouses.FindAsync(id);
            _context.ItemWarehouses.Remove(itemWarehouse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemWarehouseExists(Guid id)
        {
            return _context.ItemWarehouses.Any(e => e.Id == id);
        }
    }
}
