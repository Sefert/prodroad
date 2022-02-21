#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App;
using Domain.App;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerPriceGroupController : Controller
    {
        private readonly AppDbContext _context;

        public CustomerPriceGroupController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CustomerPriceGroup
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.CustomerPriceGroups.Include(c => c.Customer).Include(c => c.PriceGroup);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/CustomerPriceGroup/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerPriceGroup = await _context.CustomerPriceGroups
                .Include(c => c.Customer)
                .Include(c => c.PriceGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerPriceGroup == null)
            {
                return NotFound();
            }

            return View(customerPriceGroup);
        }

        // GET: Admin/CustomerPriceGroup/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name");
            ViewData["PriceGroupId"] = new SelectList(_context.PriceGroups, "Id", "Name");
            return View();
        }

        // POST: Admin/CustomerPriceGroup/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PriceGroupId,CustomerId,CreatedAt,CreatedId,UpdatedAt,UpdatedId,Id")] CustomerPriceGroup customerPriceGroup)
        {
            if (ModelState.IsValid)
            {
                customerPriceGroup.Id = Guid.NewGuid();
                _context.Add(customerPriceGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", customerPriceGroup.CustomerId);
            ViewData["PriceGroupId"] = new SelectList(_context.PriceGroups, "Id", "Name", customerPriceGroup.PriceGroupId);
            return View(customerPriceGroup);
        }

        // GET: Admin/CustomerPriceGroup/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerPriceGroup = await _context.CustomerPriceGroups.FindAsync(id);
            if (customerPriceGroup == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", customerPriceGroup.CustomerId);
            ViewData["PriceGroupId"] = new SelectList(_context.PriceGroups, "Id", "Name", customerPriceGroup.PriceGroupId);
            return View(customerPriceGroup);
        }

        // POST: Admin/CustomerPriceGroup/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PriceGroupId,CustomerId,CreatedAt,CreatedId,UpdatedAt,UpdatedId,Id")] CustomerPriceGroup customerPriceGroup)
        {
            if (id != customerPriceGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerPriceGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerPriceGroupExists(customerPriceGroup.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", customerPriceGroup.CustomerId);
            ViewData["PriceGroupId"] = new SelectList(_context.PriceGroups, "Id", "Name", customerPriceGroup.PriceGroupId);
            return View(customerPriceGroup);
        }

        // GET: Admin/CustomerPriceGroup/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerPriceGroup = await _context.CustomerPriceGroups
                .Include(c => c.Customer)
                .Include(c => c.PriceGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerPriceGroup == null)
            {
                return NotFound();
            }

            return View(customerPriceGroup);
        }

        // POST: Admin/CustomerPriceGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var customerPriceGroup = await _context.CustomerPriceGroups.FindAsync(id);
            _context.CustomerPriceGroups.Remove(customerPriceGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerPriceGroupExists(Guid id)
        {
            return _context.CustomerPriceGroups.Any(e => e.Id == id);
        }
    }
}
