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
            return View(await _context.StudyBook.ToListAsync());
        }

        // GET: StudyBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyBook = await _context.StudyBook
                .SingleOrDefaultAsync(m => m.Id == id);
            if (studyBook == null)
            {
                return NotFound();
            }

            return View(studyBook);
        }

        // GET: StudyBooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudyBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InventoryNum,Name,Author,AuthorCipher,Grade,Subject,Published,Price,Note")] StudyBook studyBook)
        {
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

            var studyBook = await _context.StudyBook.SingleOrDefaultAsync(m => m.Id == id);
            if (studyBook == null)
            {
                return NotFound();
            }
            return View(studyBook);
        }

        // POST: StudyBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InventoryNum,Name,Author,AuthorCipher,Grade,Subject,Published,Price,Note,Status")] StudyBook studyBook)
        {
            if (id != studyBook.Id)
            {
                return NotFound();
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
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Books");
            }
            return View(studyBook);
        }

        // GET: StudyBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyBook = await _context.StudyBook
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
            var studyBook = await _context.StudyBook.SingleOrDefaultAsync(m => m.Id == id);
            _context.StudyBook.Remove(studyBook);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Books");
        }

        private bool StudyBookExists(int id)
        {
            return _context.StudyBook.Any(e => e.Id == id);
        }
    }
}
