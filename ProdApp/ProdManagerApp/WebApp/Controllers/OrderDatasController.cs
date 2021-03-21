using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.App;

namespace WebApp.Controllers
{
    public class OrderDatasController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public OrderDatasController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: OrderDatas
        public async Task<IActionResult> Index()
        {
            return View(await _uow.OrderDatas.GetAllAsync());
        }

        // GET: OrderDatas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var orderData = await _uow.OrderDatas.FirstOrDefaultAsync(id.Value, false);

            if (orderData == null) return NotFound();

            return View(orderData);
        }

        // GET: OrderDatas/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ComponentId"] = new SelectList(await _uow.Components.GetAllAsync(), "Id", "Name");
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(), "Id", "Name");
            ViewData["OrderId"] = new SelectList(await _uow.Orders.GetAllAsync(), "Id", "DeliveryAddress");
            ViewData["SupplyId"] = new SelectList(await _uow.Supplys.GetAllAsync(), "Id", "Id");
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
                _uow.OrderDatas.Add(orderData);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(await _uow.Components.GetAllAsync(), "Id", "Name", orderData.ComponentId);
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(), "Id", "Name", orderData.ItemId);
            ViewData["OrderId"] = new SelectList(await _uow.Orders.GetAllAsync(), "Id", "DeliveryAddress", orderData.OrderId);
            ViewData["SupplyId"] = new SelectList(await _uow.Supplys.GetAllAsync(), "Id", "Id", orderData.SupplyId);
            return View(orderData);
        }

        // GET: OrderDatas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var orderData = await _uow.OrderDatas.FirstOrDefaultAsync(id.Value, false);

            if (orderData == null) return NotFound();

            ViewData["ComponentId"] =
                new SelectList(await _uow.Components.GetAllAsync(), "Id", "Name", orderData.ComponentId);
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(), "Id", "Name", orderData.ItemId);
            ViewData["OrderId"] =
                new SelectList(await _uow.Orders.GetAllAsync(), "Id", "DeliveryAddress", orderData.OrderId);
            ViewData["SupplyId"] = new SelectList(await _uow.Supplys.GetAllAsync(), "Id", "Id", orderData.SupplyId);
            return View(orderData);
        }

        // POST: OrderDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Quantity,Total,OrderId,ComponentId,SupplyId,ItemId,Id")] OrderData orderData)
        {
            if (id != orderData.Id) return NotFound();

            if (!ModelState.IsValid || !await _uow.OrderDatas.ExistsAsync(orderData.Id))
                return View(orderData);

            ViewData["ComponentId"] =
                new SelectList(await _uow.Components.GetAllAsync(), "Id", "Name", orderData.ComponentId);
            ViewData["ItemId"] = new SelectList(await _uow.Items.GetAllAsync(), "Id", "Name", orderData.ItemId);
            ViewData["OrderId"] =
                new SelectList(await _uow.Orders.GetAllAsync(), "Id", "DeliveryAddress", orderData.OrderId);
            ViewData["SupplyId"] = new SelectList(await _uow.Supplys.GetAllAsync(), "Id", "Id", orderData.SupplyId);

            _uow.OrderDatas.Update(orderData);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: OrderDatas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var orderData = await _uow.OrderDatas.FirstOrDefaultAsync(id.Value, false);

            if (orderData == null) return NotFound();
            return View(orderData);
        }

        // POST: OrderDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var orderData = await _uow.OrderDatas.FirstOrDefaultAsync(id);
            if (orderData == null) return NotFound();
            _uow.OrderDatas.Remove(orderData);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
