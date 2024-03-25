using App.Contracts.DAL;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class PriceController : Controller
    {
        private readonly IAppUOW _uow;

        public PriceController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: Price
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Prices.GetAllAsync());
        }

        // GET: Price/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _uow.Prices
                .FirstOrDefaultAsync(id.Value);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // GET: Price/Create
        public IActionResult Create()
        {
            ViewData["ItemId"] = new SelectList(_uow.Items.GetAll(), "Id", "Id");
            return View();
        }

        // POST: Price/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,From,Until,Cost,Region,Id")] Price price)
        {
            if (ModelState.IsValid)
            {
                price.Id = Guid.NewGuid();
                _uow.Prices.Add(price);
                await _uow.Prices.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(_uow.Items.GetAll(), "Id", "Id", price.ItemId);
            return View(price);
        }

        // GET: Price/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _uow.Prices.FirstOrDefaultAsync(id.Value);
            if (price == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(_uow.Items.GetAll(), "Id", "Id", price.ItemId);
            return View(price);
        }

        // POST: Price/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Price price)
        {
            if (id != price.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Prices.Update(price);
                    await _uow.Prices.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriceExists(price.Id))
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
            ViewData["ItemId"] = new SelectList(_uow.Items.GetAll(), "Id", "Id", price.ItemId);
            return View(price);
        }

        // GET: Price/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _uow.Prices
                .FirstOrDefaultAsync(id.Value);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // POST: Price/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var price = await _uow.Prices.FirstOrDefaultAsync(id);
            if (price != null)
            {
                _uow.Prices.Remove(price);
            }

            await _uow.Prices.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriceExists(Guid id)
        {
            return _uow.Prices.Exists(id);
        }
    }
}
