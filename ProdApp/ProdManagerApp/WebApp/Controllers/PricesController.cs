using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.App;

namespace WebApp.Controllers
{
    public class PricesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PricesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Prices
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Prices.GetAllAsync());
        }

        // GET: Prices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var price = await _uow.Prices.FirstOrDefaultAsync(id.Value, false);

            if (price == null) return NotFound();

            return View(price);
        }

        // GET: Prices/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ComponentId"] = new SelectList(await _uow.Components.GetAllAsync(), "Id", "Name");
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: Prices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,ComponentId,ItemId,StartDate,EndDate,Id")] Price price)
        {
            if (ModelState.IsValid)
            {
                price.Id = Guid.NewGuid();
                _uow.Prices.Add(price);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(await _uow.Components.GetAllAsync(), "Id", "Name", price.ComponentId);
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(), "Id", "Name", price.ItemId);
            return View(price);
        }

        // GET: Prices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var price = await _uow.Prices.FirstOrDefaultAsync(id.Value, false);

            if (price == null) return NotFound();

            ViewData["ComponentId"] =
                new SelectList(await _uow.Components.GetAllAsync(), "Id", "Name", price.ComponentId);
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(), "Id", "Name", price.ItemId);
            return View(price);
        }

        // POST: Prices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Amount,ComponentId,ItemId,StartDate,EndDate,Id")] Price price)
        {
            if (id != price.Id) return NotFound();

            if (!ModelState.IsValid || !await _uow.Prices.ExistsAsync(price.Id))
                return View(price);

            ViewData["ComponentId"] =
                new SelectList(await _uow.Components.GetAllAsync(), "Id", "Name", price.ComponentId);
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(), "Id", "Name", price.ItemId);

            _uow.Prices.Update(price);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Prices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var price = await _uow.Prices.FirstOrDefaultAsync(id.Value, false);

            if (price == null) return NotFound();

            return View(price);
        }

        // POST: Prices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var price = await _uow.Prices.FirstOrDefaultAsync(id);
            if (price == null) return NotFound();
            _uow.Prices.Remove(price);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
