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
    public class ProductionMetasController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ProductionMetasController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: ProductionMetas
        public async Task<IActionResult> Index()
        {
            return View(await _uow.ProductionMetas.GetAllAsync(User.GetUserId()!.Value));
        }

        // GET: ProductionMetas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var productionMeta = await _uow.ProductionMetas.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value, false);

            if (productionMeta == null) return NotFound();

            return View(productionMeta);
        }

        // GET: ProductionMetas/Create
        public async Task<IActionResult> Create()
        {
            ViewData["SupplyId"] = new SelectList(await _uow.Supplys.GetAllAsync(User.GetUserId()!.Value), "Id", "Id");
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
                productionMeta.AppUserId = User.GetUserId()!.Value;
                productionMeta.Id = Guid.NewGuid();
                _uow.ProductionMetas.Add(productionMeta);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplyId"] = new SelectList(await _uow.Supplys.GetAllAsync(User.GetUserId()!.Value), "Id", "Id", productionMeta.SupplyId);
            return View(productionMeta);
        }

        // GET: ProductionMetas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var uId = User.GetUserId()!.Value;

            var productionMeta = await _uow.ProductionMetas.FirstOrDefaultAsync(id.Value, uId, false);

            if (productionMeta == null) return NotFound();

            ViewData["SupplyId"] =
                new SelectList(await _uow.Supplys.GetAllAsync(uId), "Id", "Id", productionMeta.SupplyId);
            return View(productionMeta);
        }

        // POST: ProductionMetas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Line,RealStartDate,RealEndDate,SupplyId,ApplicationUserId,StartDate,EndDate,StartTime,EndTime,Id")] ProductionMeta productionMeta)
        {
            if (id != productionMeta.Id) return NotFound();
            var uId = User.GetUserId()!.Value;

            if (!ModelState.IsValid || !await _uow.ProductionMetas.ExistsAsync(productionMeta.Id, uId))
                return View(productionMeta);

            ViewData["SupplyId"] =
                new SelectList(await _uow.Supplys.GetAllAsync(uId), "Id", "Id", productionMeta.SupplyId);

            _uow.ProductionMetas.Update(productionMeta);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductionMetas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            
            var productionMeta = await _uow.ProductionMetas.FirstOrDefaultAsync(id.Value,User.GetUserId()!.Value, false);

            if (productionMeta == null) return NotFound();
            return View(productionMeta);
        }

        // POST: ProductionMetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var uId = User.GetUserId()!.Value;
            var productionMeta = await _uow.ProductionMetas.FirstOrDefaultAsync(id, uId);
            if (productionMeta == null) return NotFound();
            _uow.ProductionMetas.Remove(productionMeta, uId);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
