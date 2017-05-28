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
    public class ProvenancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProvenancesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Provenances
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Provenances.Include(p => p.Book);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Provenances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provenance = await _context.Provenances
                .Include(p => p.Book)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (provenance == null)
            {
                return RedirectToAction("Create", new { id = id });
            }

            return View(provenance);
        }

        // GET: Inventories/Create
        public IActionResult Create(int? id)
        {
            //var books = _context.Books.AsQueryable();
            var books = _context.Books.Where(b => b.Provenance == null);
            if (id.HasValue)
                books = books.Where(b => b.Id == id);
            ViewData["BookId"] = new SelectList(books.ToList(), "Id", "InventoryNum");
            return View();
        }

        // POST: Provenances/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Place,WayBill,ReceiptDate,Note,BookId")] Provenance provenance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(provenance);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "InventoryNum", provenance.BookId);
            return View(provenance);
        }

        // GET: Provenances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provenance = await _context.Provenances.SingleOrDefaultAsync(m => m.Id == id);
            if (provenance == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "InventoryNum", provenance.BookId);
            return View(provenance);
        }

        // POST: Provenances/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Place,WayBill,ReceiptDate,Note,BookId")] Provenance provenance)
        {
            if (id != provenance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(provenance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvenanceExists(provenance.Id))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "InventoryNum", provenance.BookId);
            return View(provenance);
        }

        // GET: Provenances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provenance = await _context.Provenances
                .Include(p => p.Book)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (provenance == null)
            {
                return NotFound();
            }

            return View(provenance);
        }

        // POST: Provenances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var provenance = await _context.Provenances.SingleOrDefaultAsync(m => m.Id == id);
            _context.Provenances.Remove(provenance);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProvenanceExists(int id)
        {
            return _context.Provenances.Any(e => e.Id == id);
        }
    }
}
