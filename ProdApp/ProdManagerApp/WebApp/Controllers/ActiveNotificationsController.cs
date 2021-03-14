using System;
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
    public class ActiveNotificationsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IActiveNotificationRepo _repo;

        public ActiveNotificationsController(AppDbContext context)
        {
            _context = context;
            _repo= new ActiveNotificationRepo(context);
        }

        // GET: ActiveNotifications
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAllAsync());
        }

        // GET: ActiveNotifications/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            return View(await _repo.FirstOrDefaultAsync((Guid) id, false));
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
                _repo.Add(activeNotification);
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

            var activeNotification = await _repo.FirstOrDefaultAsync((Guid) id);

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
                    _repo.Update(activeNotification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repo.ExistsAsync(activeNotification.Id).Result)
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

            var activeNotification = await _repo.FirstOrDefaultAsync((Guid) id);

            return View(activeNotification);
        }

        // POST: ActiveNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var activeNotification = await _repo.Remove(id);
            _context.ActiveNotifications.Remove(activeNotification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
