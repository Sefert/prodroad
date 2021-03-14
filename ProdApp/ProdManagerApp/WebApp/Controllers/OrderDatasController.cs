using System;
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
    public class OrderDatasController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IOrderDataRepo _repo;

        public OrderDatasController(AppDbContext context)
        {
            _context = context;
            _repo = new OrderDataRepo(context);
        }

        // GET: OrderDatas
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAllAsync());
        }

        // GET: OrderDatas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await _repo.FirstOrDefaultAsync((Guid) id));
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
                _repo.Add(orderData);
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

            var orderData = await _repo.FirstOrDefaultAsync((Guid) id);

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
                    _repo.Update(orderData);
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

            return View(await _repo.FirstOrDefaultAsync((Guid) id));
        }

        // POST: OrderDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var orderData = await _repo.FirstOrDefaultAsync(id);
            _repo.Remove(orderData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
