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
    public class PricesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPriceRepo _repo;

        public PricesController(AppDbContext context)
        {
            _context = context;
            _repo = new PriceRepo(context);
        }

        // GET: Prices
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Prices.Include(p => p.Component).Include(p => p.Item);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Prices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            return View(await _repo.FirstOrDefaultAsync((Guid) id));
        }

        // GET: Prices/Create
        public IActionResult Create()
        {
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name");
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name");
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
                _repo.Add(price);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name", price.ComponentId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", price.ItemId);
            return View(price);
        }

        // GET: Prices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _context.Prices.FindAsync(id);
            if (price == null)
            {
                return NotFound();
            }
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name", price.ComponentId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", price.ItemId);
            return View(price);
        }

        // POST: Prices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Amount,ComponentId,ItemId,StartDate,EndDate,Id")] Price price)
        {
            if (id != price.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(price);
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
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name", price.ComponentId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", price.ItemId);
            return View(price);
        }

        // GET: Prices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await _repo.FirstOrDefaultAsync((Guid) id));
        }

        // POST: Prices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var price = await _repo.FirstOrDefaultAsync(id);
            _repo.Remove(price);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
