using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using Extensions.Base;

namespace WebApp.Controllers
{
    [Authorize]
    public class SupplysController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public SupplysController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Supplys
        public async Task<IActionResult> Index()
        {
            var uId = User.GetUserId()!.Value;
            return View(await _uow.Supplys.GetAllAsync(uId));
        }

        // GET: Supplys/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var supply = await _uow.Supplys.FirstOrDefaultAsync(id.Value,User.GetUserId()!.Value, false);

            if (supply == null) return NotFound();

            return View(supply);
        }

        // GET: Supplys/Create
        public async Task<IActionResult> Create()
        {
            var uId = User.GetUserId()!.Value;
            ViewData["ComponentId"] = new SelectList(await _uow.Components.GetAllAsync(uId), "Id", "Name");
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(uId), "Id", "Name");
            ViewData["WarehouseId"] = new SelectList(await _uow.Warehouses.GetAllAsync(uId), "Id", "Address");
            return View();
        }

        // POST: Supplys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Quantity,ItemId,ComponentId,WarehouseId,Id")] Supply supply)
        {
            var uId = User.GetUserId()!.Value;
            if (ModelState.IsValid)
            {
                supply.Id = Guid.NewGuid();
                _uow.Supplys.Add(supply);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(await _uow.Components.GetAllAsync(uId), "Id", "Name", supply.ComponentId);
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(uId), "Id", "Name", supply.ItemId);
            ViewData["WarehouseId"] = new SelectList(await _uow.Warehouses.GetAllAsync(uId), "Id", "Address", supply.WarehouseId);
            return View(supply);
        }

        // GET: Supplys/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var uId = User.GetUserId()!.Value;
            
            var supply = await _uow.Supplys.FirstOrDefaultAsync(id.Value, uId,false);

            if (supply == null) return NotFound();

            ViewData["ComponentId"] =
                new SelectList(await _uow.Components.GetAllAsync(uId), "Id", "Name", supply.ComponentId);
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(uId), "Id", "Name", supply.ItemId);
            ViewData["WarehouseId"] =
                new SelectList(await _uow.Warehouses.GetAllAsync(uId), "Id", "Address", supply.WarehouseId);
            return View(supply);
        }

        // POST: Supplys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Quantity,ItemId,ComponentId,WarehouseId,Id")] Supply supply)
        {
            if (id != supply.Id) return NotFound();
            var uId = User.GetUserId()!.Value;

            if (!ModelState.IsValid || !await _uow.Supplys.ExistsAsync(supply.Id, uId))
            {
                ViewData["ComponentId"] =
                    new SelectList(await _uow.Components.GetAllAsync(uId), "Id", "Name", supply.ComponentId);
                ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(uId), "Id", "Name", supply.ItemId);
                ViewData["WarehouseId"] = new SelectList(await _uow.Warehouses.GetAllAsync(uId), "Id", "Address",
                    supply.WarehouseId);
                return View(supply);
            }

            _uow.Supplys.Update(supply);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Supplys/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var supply = await _uow.Supplys.FirstOrDefaultAsync(id.Value,User.GetUserId()!.Value, false);

            if (supply == null) return NotFound();
            return View(supply);
        }

        // POST: Supplys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var uId = User.GetUserId()!.Value;
            var supply = await _uow.Supplys.FirstOrDefaultAsync(id, uId);
            if (supply == null) return NotFound();
            _uow.Supplys.Remove(supply, uId);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
