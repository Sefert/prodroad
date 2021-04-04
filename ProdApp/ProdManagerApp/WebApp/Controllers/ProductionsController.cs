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
            
            return View(await _uow.Productions.GetAllAsync(User.GetUserId()!.Value));
        }

        // GET: Productions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var production = await _uow.Productions.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value, false);

            if (production == null) return NotFound();
            return View(production);
        }

        // GET: Productions/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ComponentId"] = new SelectList(await _uow.Components.GetAllAsync(User.GetUserId()!.Value), "Id", "Name");
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(User.GetUserId()!.Value), "Id", "Name");
            ViewData["ProductionMetaId"] = new SelectList(await _uow.ProductionMetas.GetAllAsync(User.GetUserId()!.Value), "Id", "Line");
            return View();
        }

        // POST: Productions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Quantity,UsedProduced,ProductionMetaId,ComponentId,ItemId,StartDate,EndDate,StartTime,EndTime,Id")] Production production)
        {
            var uId = User.GetUserId()!.Value;
            if (ModelState.IsValid)
            {
                production.Id = Guid.NewGuid();
                _uow.Productions.Add(production);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(await _uow.Components.GetAllAsync(uId), "Id", "Name", production.ComponentId);
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(uId), "Id", "Name", production.ItemId);
            ViewData["ProductionMetaId"] = new SelectList(await _uow.ProductionMetas.GetAllAsync(uId), "Id", "Line", production.ProductionMetaId);
            return View(production);
        }

        // GET: Productions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var uId = User.GetUserId()!.Value;

            var production = await _uow.Productions.FirstOrDefaultAsync(id.Value, uId, false);

            if (production == null) return NotFound();

            ViewData["ComponentId"] =
                new SelectList(await _uow.Components.GetAllAsync(uId), "Id", "Name", production.ComponentId);
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(uId), "Id", "Name", production.ItemId);
            ViewData["ProductionMetaId"] = new SelectList(await _uow.ProductionMetas.GetAllAsync(uId), "Id", "Line",
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
            var uId = User.GetUserId()!.Value;

            if (!ModelState.IsValid || !await _uow.Productions.ExistsAsync(production.Id, uId))
                return View(production);

            ViewData["ComponentId"] =
                new SelectList(await _uow.Components.GetAllAsync(uId), "Id", "Name", production.ComponentId);
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(uId), "Id", "Name", production.ItemId);
            ViewData["ProductionMetaId"] = new SelectList(await _uow.ProductionMetas.GetAllAsync(uId), "Id", "Line",
                production.ProductionMetaId);

            _uow.Productions.Update(production);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Productions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var production = await _uow.Productions.FirstOrDefaultAsync(id.Value,User.GetUserId()!.Value, false);

            if (production == null) return NotFound();
            return View(production);
        }

        // POST: Productions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var uId = User.GetUserId()!.Value;
            var production = await _uow.Productions.FirstOrDefaultAsync(id, uId);
            if (production == null) return NotFound();
            _uow.Productions.Remove(production, uId);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
