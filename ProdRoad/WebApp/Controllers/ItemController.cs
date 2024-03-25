using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.DAL;
using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class ItemController : Controller
    {
        private readonly IAppUOW _uow;

        public ItemController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: Item
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Items.GetAllAsync());
        }

        // GET: Item/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _uow.Items
                .FirstOrDefaultAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Item/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_uow.Users.GetAll(), "Id", "Id");
            return View();
        }

        // POST: Item/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Item item)
        {
            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid();
                _uow.Items.Add(item);
                await _uow.Items.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_uow.Users.GetAll(), "Id", "Id", item.AppUserId);
            return View(item);
        }

        // GET: Item/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _uow.Items.FirstOrDefaultAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_uow.Users.GetAll(), "Id", "Id", item.AppUserId);
            return View(item);
        }

        // POST: Item/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,Name,Type,Unit,Quantity,Id")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Items.Update(item);
                    await _uow.Items.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
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
            ViewData["AppUserId"] = new SelectList(_uow.Users.GetAll(), "Id", "Id", item.AppUserId);
            return View(item);
        }

        // GET: Item/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _uow.Items
                .FirstOrDefaultAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var item = await _uow.Items.FirstOrDefaultAsync(id);
            if (item != null)
            {
                _uow.Items.Remove(item);
            }

            await _uow.Items.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(Guid id)
        {
            return _uow.Items.Exists(id);
        }
    }
}
