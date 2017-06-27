using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolLib.Data;
using SchoolLib.Models.Books;

namespace SchoolLib.Controllers
{
    public class StudyBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudyBooksController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: StudyBooks
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudyBooks.ToListAsync());
        }

        // GET: StudyBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyBook = await _context.StudyBooks
                .Include(sb => sb.Inventory)
                .Include(sb => sb.Provenance)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (studyBook == null)
                return NotFound();
            
            if (studyBook.Status == BookStatus.OnHands)
            {
                ViewData["readerId"] =
                    _context.
                    Issuances.
                    Where(i => i.BookId == studyBook.Id && i.AcceptanceDate == null).
                    SingleOrDefaultAsync()?.Result.ReaderId;
            }
            return View(studyBook);
        }

        // GET: StudyBooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudyBooks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,Author,AuthorCipher,Grade,Subject,Published,Price,Note")]
            StudyBook studyBook
            )
        {
            if (_context.Books.Any(b => b.Id == studyBook.Id))
            {
                ModelState.AddModelError("Id", "Підручник з даним інвентарним номером все існує");
            }
            if (ModelState.IsValid)
            {
                studyBook.Status = BookStatus.InStock;
                _context.Add(studyBook);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Books");
            }
            return View(studyBook);
        }

        // GET: StudyBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyBook = await _context.StudyBooks.SingleOrDefaultAsync(m => m.Id == id);
            if (studyBook == null)
            {
                return NotFound();
            }
            return View(studyBook);
        }

        // POST: StudyBooks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int curId,
            [Bind("Id,Name,Author,AuthorCipher,Grade,Subject,Published,Price,Note,Status")]
            StudyBook studyBook
            )
        {
            if (_context.Books.Any(b => b.Id == studyBook.Id && b.Id != curId))
            {
                ModelState.AddModelError("Id", "Підручник з даним інвентарним номером все існує");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studyBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudyBookExists(studyBook.Id))
                    {
                        //!todo: Сделать возможным изменение ид книги
                        //await DeleteConfirmed(curId);
                        //return await Create(studyBook);
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Books");
            }
            studyBook.Id = curId;
            return View(studyBook);
        }

        // GET: StudyBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyBook = await _context.StudyBooks
                .SingleOrDefaultAsync(m => m.Id == id);
            if (studyBook == null)
            {
                return NotFound();
            }

            return View(studyBook);
        }

        // POST: StudyBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studyBook = await _context.StudyBooks.SingleOrDefaultAsync(m => m.Id == id);
            _context.StudyBooks.Remove(studyBook);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Books");
        }

        private bool StudyBookExists(int id)
        {
            return _context.StudyBooks.Any(e => e.Id == id);
        }
    }
}
