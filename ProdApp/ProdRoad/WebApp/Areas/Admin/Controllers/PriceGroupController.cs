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
    public class PriceGroupController : Controller
    {
        private readonly AppDbContext _context;

        public PriceGroupController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/PriceGroup
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PriceGroups.Include(p => p.AppUser).Include(p => p.SubPriceGroup);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/PriceGroup/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceGroup = await _context.PriceGroups
                .Include(p => p.AppUser)
                .Include(p => p.SubPriceGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (priceGroup == null)
            {
                return NotFound();
            }

            return View(priceGroup);
        }

        // GET: Admin/PriceGroup/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["PriceGroupId"] = new SelectList(_context.PriceGroups, "Id", "Name");
            return View();
        }

        // POST: Admin/PriceGroup/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,PriceGroupId,Name,Tax,Margin,Discount,Id")] PriceGroup priceGroup)
        {
            if (ModelState.IsValid)
            {
                priceGroup.Id = Guid.NewGuid();
                _context.Add(priceGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", priceGroup.AppUserId);
            ViewData["PriceGroupId"] = new SelectList(_context.PriceGroups, "Id", "Name", priceGroup.PriceGroupId);
            return View(priceGroup);
        }

        // GET: Admin/PriceGroup/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceGroup = await _context.PriceGroups.FindAsync(id);
            if (priceGroup == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", priceGroup.AppUserId);
            ViewData["PriceGroupId"] = new SelectList(_context.PriceGroups, "Id", "Name", priceGroup.PriceGroupId);
            return View(priceGroup);
        }

        // POST: Admin/PriceGroup/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,PriceGroupId,Name,Tax,Margin,Discount,Id")] PriceGroup priceGroup)
        {
            if (id != priceGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(priceGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriceGroupExists(priceGroup.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", priceGroup.AppUserId);
            ViewData["PriceGroupId"] = new SelectList(_context.PriceGroups, "Id", "Name", priceGroup.PriceGroupId);
            return View(priceGroup);
        }

        // GET: Admin/PriceGroup/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceGroup = await _context.PriceGroups
                .Include(p => p.AppUser)
                .Include(p => p.SubPriceGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (priceGroup == null)
            {
                return NotFound();
            }

            return View(priceGroup);
        }

        // POST: Admin/PriceGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var priceGroup = await _context.PriceGroups.FindAsync(id);
            _context.PriceGroups.Remove(priceGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriceGroupExists(Guid id)
        {
            return _context.PriceGroups.Any(e => e.Id == id);
        }
    }
}
