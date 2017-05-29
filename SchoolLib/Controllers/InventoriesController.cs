using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolLib.Data;
using SchoolLib.Models.Books;

namespace SchoolLib.Controllers
{
    public class InventoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoriesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Inventories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Inventories.Include(i => i.Book);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Inventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                .Include(i => i.Book)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (inventory == null)
            {
                return RedirectToAction("Create", new { id = id } );
            }

            return View(inventory);
        }

        // GET: Inventories/Create
        public IActionResult Create(int? id)
        {
            var books = _context.Books.Where(b => b.Inventory == null);
            if (id.HasValue)
                books = books.Where(b => b.Id == id);
            ViewData["BookId"] = new SelectList(books.ToList(), "Id", "InventoryNum");
            
            return View();
        }

        // POST: Inventories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActNumber,Year,Couse,Note,BookId")] Inventory inventory)
        {
            if (_context.Inventories.Any
                    (i => i.ActNumber == inventory.ActNumber || i.Id == inventory.Id))
            {
                ModelState.AddModelError("ActNumber", "Інвентаризаційний запис с даним номером акту вже існує");
            }
            if (ModelState.IsValid)
            {
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "InventoryNum", inventory.BookId);
            return View(inventory);
        }

        // GET: Inventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories.SingleOrDefaultAsync(m => m.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "InventoryNum", inventory.BookId);
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ActNumber,Year,Couse,Note,BookId")] Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return NotFound();
            }
            if (_context.Inventories.Any
                    (i => i.ActNumber == inventory.ActNumber && i.Id != inventory.Id))
            {
                ModelState.AddModelError("ActNumber", "Інвентаризаційний запис с даним номером акту вже існує");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "InventoryNum", inventory.BookId);
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                .Include(i => i.Book)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventory = await _context.Inventories.SingleOrDefaultAsync(m => m.Id == id);
            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventories.Any(e => e.Id == id);
        }
    }
}
