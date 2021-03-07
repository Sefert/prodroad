using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.Controllers
{
    public class OrderDatasController : Controller
    {
        private readonly AppDbContext _context;

        public OrderDatasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrderDatas
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.OrderDatas.Include(o => o.Component).Include(o => o.Item).Include(o => o.Order).Include(o => o.Supply);
            return View(await appDbContext.ToListAsync());
        }

        // GET: OrderDatas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderData = await _context.OrderDatas
                .Include(o => o.Component)
                .Include(o => o.Item)
                .Include(o => o.Order)
                .Include(o => o.Supply)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderData == null)
            {
                return NotFound();
            }

            return View(orderData);
        }

        // GET: OrderDatas/Create
        public IActionResult Create()
        {
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name");
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name");
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress");
            ViewData["SupplyId"] = new SelectList(_context.Supplys, "Id", "Id");
            return View();
        }

        // POST: OrderDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Quantity,Total,OrderId,ComponentId,SupplyId,ItemId,Id")] OrderData orderData)
        {
            if (ModelState.IsValid)
            {
                orderData.Id = Guid.NewGuid();
                _context.Add(orderData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name", orderData.ComponentId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", orderData.ItemId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress", orderData.OrderId);
            ViewData["SupplyId"] = new SelectList(_context.Supplys, "Id", "Id", orderData.SupplyId);
            return View(orderData);
        }

        // GET: OrderDatas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderData = await _context.OrderDatas.FindAsync(id);
            if (orderData == null)
            {
                return NotFound();
            }
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name", orderData.ComponentId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", orderData.ItemId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress", orderData.OrderId);
            ViewData["SupplyId"] = new SelectList(_context.Supplys, "Id", "Id", orderData.SupplyId);
            return View(orderData);
        }

        // POST: OrderDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Quantity,Total,OrderId,ComponentId,SupplyId,ItemId,Id")] OrderData orderData)
        {
            if (id != orderData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDataExists(orderData.Id))
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
            ViewData["ComponentId"] = new SelectList(_context.Components, "Id", "Name", orderData.ComponentId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", orderData.ItemId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress", orderData.OrderId);
            ViewData["SupplyId"] = new SelectList(_context.Supplys, "Id", "Id", orderData.SupplyId);
            return View(orderData);
        }

        // GET: OrderDatas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderData = await _context.OrderDatas
                .Include(o => o.Component)
                .Include(o => o.Item)
                .Include(o => o.Order)
                .Include(o => o.Supply)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderData == null)
            {
                return NotFound();
            }

            return View(orderData);
        }

        // POST: OrderDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var orderData = await _context.OrderDatas.FindAsync(id);
            _context.OrderDatas.Remove(orderData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDataExists(Guid id)
        {
            return _context.OrderDatas.Any(e => e.Id == id);
        }
    }
}
