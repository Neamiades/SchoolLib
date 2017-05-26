using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolLib.Data;
using SchoolLib.Models.Books;

namespace SchoolLib.Controllers
{
    public class AdditionalBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdditionalBooksController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: AdditionalBooks
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdditionalBooks.ToListAsync());
        }

        // GET: AdditionalBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalBook = await _context.AdditionalBooks
                .SingleOrDefaultAsync(m => m.Id == id);
            if (additionalBook == null)
            {
                return NotFound();
            }

            return View(additionalBook);
        }

        // GET: AdditionalBooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdditionalBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InventoryNum,Name,Author,AuthorCipher,Language,Cipher,Published,Price,Note")] AdditionalBook additionalBook)
        {
            if (ModelState.IsValid)
            {
                additionalBook.Status = BookStatus.InStock;
                _context.Add(additionalBook);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Books");
            }
            return View(additionalBook);
        }

        // GET: AdditionalBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalBook = await _context.AdditionalBooks.SingleOrDefaultAsync(m => m.Id == id);
            if (additionalBook == null)
            {
                return NotFound();
            }
            return View(additionalBook);
        }

        // POST: AdditionalBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InventoryNum,Name,Author,AuthorCipher,Language,Cipher,Published,Price,Note")] AdditionalBook additionalBook)
        {
            if (id != additionalBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(additionalBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdditionalBookExists(additionalBook.Id))
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
            return View(additionalBook);
        }

        // GET: AdditionalBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalBook = await _context.AdditionalBooks
                .SingleOrDefaultAsync(m => m.Id == id);
            if (additionalBook == null)
            {
                return NotFound();
            }

            return View(additionalBook);
        }

        // POST: AdditionalBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var additionalBook = await _context.AdditionalBooks.SingleOrDefaultAsync(m => m.Id == id);
            _context.AdditionalBooks.Remove(additionalBook);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Books");
        }

        private bool AdditionalBookExists(int id)
        {
            return _context.AdditionalBooks.Any(e => e.Id == id);
        }
    }
}
