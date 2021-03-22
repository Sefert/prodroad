using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Domain.App;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    public class WarehouseController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public WarehouseController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Warehouse
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Warehouses.GetAllAsync());
        }

        // GET: Warehouse/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var warehouse = await _uow.Warehouses.FirstOrDefaultAsync(id.Value, false);

            if (warehouse == null) return NotFound();

            return View(warehouse);
        }

        // GET: Warehouse/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Warehouse/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address,ApplicationUserId,Id")] Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                warehouse.Id = Guid.NewGuid();
                _uow.Warehouses.Add(warehouse);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(warehouse);
        }

        // GET: Warehouse/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var warehouse = await _uow.Warehouses.FirstOrDefaultAsync(id.Value, false);

            if (warehouse == null) return NotFound();
            return View(warehouse);
        }

        // POST: Warehouse/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Address,ApplicationUserId,Id")] Warehouse warehouse)
        {
            if (id != warehouse.Id) return NotFound();

            if (!ModelState.IsValid || !await _uow.Warehouses.ExistsAsync(warehouse.Id))
                return View(warehouse);

            _uow.Warehouses.Update(warehouse);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Warehouse/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var warehouse = await _uow.Warehouses.FirstOrDefaultAsync(id.Value, false);

            if (warehouse == null) return NotFound();
            return View(warehouse);
        }

        // POST: Warehouse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var warehouse = await _uow.Warehouses.FirstOrDefaultAsync(id);
            if (warehouse == null) return NotFound();
            _uow.Warehouses.Remove(warehouse);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
