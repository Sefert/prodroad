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
    public class ProductionMetasController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductionMetaRepo _repo;

        public ProductionMetasController(AppDbContext context)
        {
            _context = context;
            _repo = new ProductionMetaRepo(_context);
        }

        // GET: ProductionMetas
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAllAsync());
        }

        // GET: ProductionMetas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await _repo.FirstOrDefaultAsync((Guid) id));
        }

        // GET: ProductionMetas/Create
        public IActionResult Create()
        {
            ViewData["SupplyId"] = new SelectList(_context.Supplys, "Id", "Id");
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
                productionMeta.Id = Guid.NewGuid();
                _repo.Add(productionMeta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplyId"] = new SelectList(_context.Supplys, "Id", "Id", productionMeta.SupplyId);
            return View(productionMeta);
        }

        // GET: ProductionMetas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionMeta = await _repo.FirstOrDefaultAsync((Guid) id);

            ViewData["SupplyId"] = new SelectList(_context.Supplys, "Id", "Id", productionMeta.SupplyId);
            return View(productionMeta);
        }

        // POST: ProductionMetas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Line,RealStartDate,RealEndDate,SupplyId,ApplicationUserId,StartDate,EndDate,StartTime,EndTime,Id")] ProductionMeta productionMeta)
        {
            if (id != productionMeta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(productionMeta);
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
            ViewData["SupplyId"] = new SelectList(_context.Supplys, "Id", "Id", productionMeta.SupplyId);
            return View(productionMeta);
        }

        // GET: ProductionMetas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await _repo.FirstOrDefaultAsync((Guid) id));
        }

        // POST: ProductionMetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var productionMeta = await _repo.FirstOrDefaultAsync(id);
            _repo.Remove(productionMeta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
