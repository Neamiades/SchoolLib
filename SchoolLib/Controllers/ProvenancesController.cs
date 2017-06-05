using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolLib.Data;
using SchoolLib.Models.Books;
using System.Globalization;

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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Provenances.Include(p => p.Book);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Provenances/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id, int? bookId)
        {
            if (id == null)
            {
                if (bookId != null)
                    return RedirectToAction("Create", new { bookId = bookId });
                return NotFound();
            }

            var provenance = await _context.Provenances
                .Include(p => p.Book)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (provenance == null)
                return NotFound();
 
            return View(provenance);
        }

        // GET: Inventories/Create
        [HttpGet]
        public IActionResult Create(int? bookId)
        {
            ViewData["BookId"] = bookId;
            return View();
        }

        // POST: Provenances/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Place,WayBill,ReceiptDate,Note,BookId")] Provenance provenance)
        {
            if (!_context.Books.Any(b => b.Id == provenance.BookId))
                ModelState.AddModelError("BookId", "Книги з даним інвентарним номером не існує");
            else if (_context.Provenances.Any(i => i.BookId == provenance.BookId))
                ModelState.AddModelError("BookId", "Книга з даним інвентарним номером вже має запис про походження");
            
            if (_context.Provenances.Any(i => i.WayBill == provenance.WayBill))
                ModelState.AddModelError("WayBill", "Запис про походження з даним номером накладної вже існує");
            
            if (ModelState.IsValid)
            {
                _context.Add(provenance);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["BookId"] = provenance.BookId;
            ViewData["Fail"] = true;
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
            return View(provenance);
        }

        // POST: Provenances/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit
            (
            int id,
            int curBookId,
            int curWayBill,
            [Bind("Id,Place,WayBill,ReceiptDate,Note,BookId")]
            Provenance provenance
            )
        {
            if (id != provenance.Id)
            {
                return NotFound();
            }
            if (!_context.Books.Any(b => b.Id == provenance.BookId))
                ModelState.AddModelError("BookId", "Книги з даним інвентарним номером не існує");
            else if (_context.Provenances.Any(p => p.BookId == provenance.BookId && p.BookId != curBookId))
                ModelState.AddModelError("BookId", "Книга з даним інвентарним номером вже має запис про походження");
            if (_context.Provenances.Any(p => p.WayBill == provenance.WayBill && p.WayBill != curWayBill))
                ModelState.AddModelError("WayBill", "Запис про походження з даним номером накладної вже існує");
            
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
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction("Index");
            }
            provenance.WayBill = curWayBill;
            provenance.BookId = curBookId;
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

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckDate(string strDate)
        {
            IFormatProvider culture = new CultureInfo("uk-UA");
            DateTime date, low;
            //DateTime date = DateTime.Parse(strDate, culture, DateTimeStyles.AssumeLocal);
            low = DateTime.ParseExact("01.01.1990", "dd.mm.yyyy", culture);
            if (!DateTime.TryParse(strDate, culture, DateTimeStyles.AssumeLocal, out date) ||
                date > DateTime.Now || date < low)
                return Json(false);
            return Json(true);
        }

        private bool ProvenanceExists(int id)
        {
            return _context.Provenances.Any(e => e.Id == id);
        }
    }
}
