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
    public class SupplysController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISupplyRepo _repo;

        public SupplysController(AppDbContext context)
        {
            _context = context;
            _repo = new SupplyRepo(context);
        }

        // GET: Supplys
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAllAsync());
        }

        // GET: Supplys/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await _repo.FirstOrDefaultAsync((Guid) id));
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
                _repo.Add(supply);
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

            var supply = await _repo.FirstOrDefaultAsync((Guid) id);

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
                    _repo.Update(supply);
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

            return View(await  _repo.FirstOrDefaultAsync((Guid) id));
        }

        // POST: Supplys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supply = await _repo.FirstOrDefaultAsync(id);
            _repo.Remove(supply);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
