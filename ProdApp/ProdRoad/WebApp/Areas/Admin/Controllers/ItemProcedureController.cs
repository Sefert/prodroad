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
    public class ItemProcedureController : Controller
    {
        private readonly AppDbContext _context;

        public ItemProcedureController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ItemProcedure
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ItemProcedures.Include(i => i.Item).Include(i => i.Procedure);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/ItemProcedure/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemProcedure = await _context.ItemProcedures
                .Include(i => i.Item)
                .Include(i => i.Procedure)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemProcedure == null)
            {
                return NotFound();
            }

            return View(itemProcedure);
        }

        // GET: Admin/ItemProcedure/Create
        public IActionResult Create()
        {
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name");
            ViewData["ProcedureId"] = new SelectList(_context.Procedures, "Id", "Code");
            return View();
        }

        // POST: Admin/ItemProcedure/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProcedureId,ItemId,CreatedUsed,Quantity,Id")] ItemProcedure itemProcedure)
        {
            if (ModelState.IsValid)
            {
                itemProcedure.Id = Guid.NewGuid();
                _context.Add(itemProcedure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", itemProcedure.ItemId);
            ViewData["ProcedureId"] = new SelectList(_context.Procedures, "Id", "Code", itemProcedure.ProcedureId);
            return View(itemProcedure);
        }

        // GET: Admin/ItemProcedure/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemProcedure = await _context.ItemProcedures.FindAsync(id);
            if (itemProcedure == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", itemProcedure.ItemId);
            ViewData["ProcedureId"] = new SelectList(_context.Procedures, "Id", "Code", itemProcedure.ProcedureId);
            return View(itemProcedure);
        }

        // POST: Admin/ItemProcedure/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProcedureId,ItemId,CreatedUsed,Quantity,Id")] ItemProcedure itemProcedure)
        {
            if (id != itemProcedure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemProcedure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemProcedureExists(itemProcedure.Id))
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
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", itemProcedure.ItemId);
            ViewData["ProcedureId"] = new SelectList(_context.Procedures, "Id", "Code", itemProcedure.ProcedureId);
            return View(itemProcedure);
        }

        // GET: Admin/ItemProcedure/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemProcedure = await _context.ItemProcedures
                .Include(i => i.Item)
                .Include(i => i.Procedure)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemProcedure == null)
            {
                return NotFound();
            }

            return View(itemProcedure);
        }

        // POST: Admin/ItemProcedure/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var itemProcedure = await _context.ItemProcedures.FindAsync(id);
            _context.ItemProcedures.Remove(itemProcedure);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemProcedureExists(Guid id)
        {
            return _context.ItemProcedures.Any(e => e.Id == id);
        }
    }
}
