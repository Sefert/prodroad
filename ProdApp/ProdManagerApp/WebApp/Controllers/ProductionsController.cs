using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.App;

namespace WebApp.Controllers
{
    public class ProductionsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ProductionsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Productions
        public async Task<IActionResult> Index()
        {
            
            return View(await _uow.Productions.GetAllAsync());
        }

        // GET: Productions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var production = await _uow.Productions.FirstOrDefaultAsync(id.Value, false);

            if (production == null) return NotFound();
            return View(production);
        }

        // GET: Productions/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ComponentId"] = new SelectList(await _uow.Components.GetAllAsync(), "Id", "Name");
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(), "Id", "Name");
            ViewData["ProductionMetaId"] = new SelectList(await _uow.ProductionMetas.GetAllAsync(), "Id", "Line");
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
                _uow.Productions.Add(production);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(await _uow.Components.GetAllAsync(), "Id", "Name", production.ComponentId);
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(), "Id", "Name", production.ItemId);
            ViewData["ProductionMetaId"] = new SelectList(await _uow.ProductionMetas.GetAllAsync(), "Id", "Line", production.ProductionMetaId);
            return View(production);
        }

        // GET: Productions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var production = await _uow.Productions.FirstOrDefaultAsync(id.Value, false);

            if (production == null) return NotFound();

            ViewData["ComponentId"] =
                new SelectList(await _uow.Components.GetAllAsync(), "Id", "Name", production.ComponentId);
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(), "Id", "Name", production.ItemId);
            ViewData["ProductionMetaId"] = new SelectList(await _uow.ProductionMetas.GetAllAsync(), "Id", "Line",
                production.ProductionMetaId);
            return View(production);
        }

        // POST: Productions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Quantity,UsedProduced,ProductionMetaId,ComponentId,ItemId,StartDate,EndDate,StartTime,EndTime,Id")] Production production)
        {
            if (id != production.Id) return NotFound();

            if (!ModelState.IsValid || !await _uow.Productions.ExistsAsync(production.Id))
                return View(production);

            ViewData["ComponentId"] =
                new SelectList(await _uow.Components.GetAllAsync(), "Id", "Name", production.ComponentId);
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(), "Id", "Name", production.ItemId);
            ViewData["ProductionMetaId"] = new SelectList(await _uow.ProductionMetas.GetAllAsync(), "Id", "Line",
                production.ProductionMetaId);

            _uow.Productions.Update(production);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Productions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var production = await _uow.Productions.FirstOrDefaultAsync(id.Value, false);

            if (production == null) return NotFound();
            return View(production);
        }

        // POST: Productions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var production = await _uow.Productions.FirstOrDefaultAsync(id);
            if (production == null) return NotFound();
            _uow.Productions.Remove(production);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
