using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolLib.Data;
using SchoolLib.Models.People;

namespace SchoolLib.Controllers
{
    public class DropsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DropsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Drops
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Drops.Include(d => d.Reader);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Drops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drop = await _context.Drops
                .Include(d => d.Reader)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (drop == null)
            {
                return NotFound();
            }

            return View(drop);
        }

        // GET: Drops/Create
        [HttpGet]
        public IActionResult Create(int? readerId)
        {
            ViewData["ReaderId"] = readerId;
            
            return View();
        }

        // POST: Drops/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Couse,Note,ReaderId")] Drop drop)
        {
            if (_context.Drops.Any(d => d.ReaderId == drop.ReaderId))
            {
                ModelState.AddModelError("ReaderId", "Читач з даним ідентифікаційним номером вже має запис про вибуття");
            }
            if (ModelState.IsValid)
            {
                _context.Add(drop);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(drop);
        }

        // GET: Drops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drop = await _context.Drops.SingleOrDefaultAsync(m => m.Id == id);
            if (drop == null)
            {
                return NotFound();
            }
            ViewData["ReaderId"] = drop.ReaderId;
            return View(drop);
        }

        // POST: Drops/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int curReaderId, [Bind("Id,Date,Couse,Note,ReaderId")] Drop drop)
        {
            if (id != drop.Id)
                return NotFound();

            if (_context.Drops.Any(d => d.ReaderId == drop.ReaderId && d.ReaderId != curReaderId))
                ModelState.AddModelError("ReaderId", "Читач з даним ідентифікаційним номером вже має запис про вибуття");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DropExists(drop.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction("Index");
            }
            return View(drop);
        }

        // GET: Drops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drop = await _context.Drops
                .Include(d => d.Reader)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (drop == null)
            {
                return NotFound();
            }

            return View(drop);
        }

        // POST: Drops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drop = await _context.Drops.SingleOrDefaultAsync(m => m.Id == id);
            _context.Drops.Remove(drop);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DropExists(int id)
        {
            return _context.Drops.Any(e => e.Id == id);
        }
    }
}
