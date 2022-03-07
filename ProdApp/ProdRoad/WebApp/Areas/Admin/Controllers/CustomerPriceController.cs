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
    public class CustomerPriceController : Controller
    {
        private readonly AppDbContext _context;

        public CustomerPriceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CustomerPrice
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.CustomerPrices.Include(c => c.Price);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/CustomerPrice/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerPrice = await _context.CustomerPrices
                .Include(c => c.Price)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerPrice == null)
            {
                return NotFound();
            }

            return View(customerPrice);
        }

        // GET: Admin/CustomerPrice/Create
        public IActionResult Create()
        {
            ViewData["PriceId"] = new SelectList(_context.Prices, "Id", "Id");
            return View();
        }

        // POST: Admin/CustomerPrice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerPriceGroupId,PriceId,CreatedAt,CreatedId,UpdatedAt,UpdatedId,Id")] CustomerPrice customerPrice)
        {
            if (ModelState.IsValid)
            {
                customerPrice.Id = Guid.NewGuid();
                _context.Add(customerPrice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PriceId"] = new SelectList(_context.Prices, "Id", "Id", customerPrice.PriceId);
            return View(customerPrice);
        }

        // GET: Admin/CustomerPrice/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerPrice = await _context.CustomerPrices.FindAsync(id);
            if (customerPrice == null)
            {
                return NotFound();
            }
            ViewData["PriceId"] = new SelectList(_context.Prices, "Id", "Id", customerPrice.PriceId);
            return View(customerPrice);
        }

        // POST: Admin/CustomerPrice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CustomerPriceGroupId,PriceId,CreatedAt,CreatedId,UpdatedAt,UpdatedId,Id")] CustomerPrice customerPrice)
        {
            if (id != customerPrice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerPrice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerPriceExists(customerPrice.Id))
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
            ViewData["PriceId"] = new SelectList(_context.Prices, "Id", "Id", customerPrice.PriceId);
            return View(customerPrice);
        }

        // GET: Admin/CustomerPrice/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerPrice = await _context.CustomerPrices
                .Include(c => c.Price)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerPrice == null)
            {
                return NotFound();
            }

            return View(customerPrice);
        }

        // POST: Admin/CustomerPrice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var customerPrice = await _context.CustomerPrices.FindAsync(id);
            _context.CustomerPrices.Remove(customerPrice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerPriceExists(Guid id)
        {
            return _context.CustomerPrices.Any(e => e.Id == id);
        }
    }
}
