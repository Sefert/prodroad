using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class ActiveNotificationsController : Controller
    {
        private readonly AppDbContext _context;

        public ActiveNotificationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ActiveNotifications
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ActiveNotifications.Include(a => a.MasterNotification).Include(a => a.Order).Include(a => a.Supply);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ActiveNotifications/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activeNotification = await _context.ActiveNotifications
                .Include(a => a.MasterNotification)
                .Include(a => a.Order)
                .Include(a => a.Supply)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activeNotification == null)
            {
                return NotFound();
            }

            return View(activeNotification);
        }

        // GET: ActiveNotifications/Create
        public IActionResult Create()
        {
            ViewData["MasterNotificationId"] = new SelectList(_context.ActiveNotifications, "Id", "Head");
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress");
            ViewData["SupplyId"] = new SelectList(_context.Supplys, "Id", "Id");
            return View();
        }

        // POST: ActiveNotifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Head,Info,OrderId,SupplyId,ApplicationUserId,MasterNotificationId,StartDate,EndDate,StartTime,EndTime,Id")] ActiveNotification activeNotification)
        {
            if (ModelState.IsValid)
            {
                activeNotification.Id = Guid.NewGuid();
                _context.Add(activeNotification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MasterNotificationId"] = new SelectList(_context.ActiveNotifications, "Id", "Head", activeNotification.MasterNotificationId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress", activeNotification.OrderId);
            ViewData["SupplyId"] = new SelectList(_context.Supplys, "Id", "Id", activeNotification.SupplyId);
            return View(activeNotification);
        }

        // GET: ActiveNotifications/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activeNotification = await _context.ActiveNotifications.FindAsync(id);
            if (activeNotification == null)
            {
                return NotFound();
            }
            ViewData["MasterNotificationId"] = new SelectList(_context.ActiveNotifications, "Id", "Head", activeNotification.MasterNotificationId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress", activeNotification.OrderId);
            ViewData["SupplyId"] = new SelectList(_context.Supplys, "Id", "Id", activeNotification.SupplyId);
            return View(activeNotification);
        }

        // POST: ActiveNotifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Head,Info,OrderId,SupplyId,ApplicationUserId,MasterNotificationId,StartDate,EndDate,StartTime,EndTime,Id")] ActiveNotification activeNotification)
        {
            if (id != activeNotification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activeNotification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActiveNotificationExists(activeNotification.Id))
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
            ViewData["MasterNotificationId"] = new SelectList(_context.ActiveNotifications, "Id", "Head", activeNotification.MasterNotificationId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress", activeNotification.OrderId);
            ViewData["SupplyId"] = new SelectList(_context.Supplys, "Id", "Id", activeNotification.SupplyId);
            return View(activeNotification);
        }

        // GET: ActiveNotifications/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activeNotification = await _context.ActiveNotifications
                .Include(a => a.MasterNotification)
                .Include(a => a.Order)
                .Include(a => a.Supply)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activeNotification == null)
            {
                return NotFound();
            }

            return View(activeNotification);
        }

        // POST: ActiveNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var activeNotification = await _context.ActiveNotifications.FindAsync(id);
            _context.ActiveNotifications.Remove(activeNotification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActiveNotificationExists(Guid id)
        {
            return _context.ActiveNotifications.Any(e => e.Id == id);
        }
    }
}
