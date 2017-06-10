using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolLib.Data;
using SchoolLib.Models.People;

namespace SchoolLib.Controllers
{
    public class DropsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DropsController(ApplicationDbContext context) => _context = context;
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Drops.Include(d => d.Reader);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id, int? readerId)
        {
            if (id == null)
            {
                if (readerId != null)
                    return RedirectToAction("Create", new { readerId = readerId });
                return NotFound();
            }
            var drop = await _context.Drops
                .Include(d => d.Reader)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (drop == null)
                return NotFound();
            return View(drop);
        }
        [HttpGet]
        public IActionResult Create(int? readerId)
        {
            ViewData["ReaderId"] = readerId;   
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Couse,Note,ReaderId")] Drop drop)
        {
            if (!_context.Readers.Any(b => b.Id == drop.ReaderId))
                ModelState.AddModelError("ReaderId", "Читача з даним ідентифікаційним номером не існує");
            else if (_context.Drops.Any(d => d.ReaderId == drop.ReaderId))
                ModelState.AddModelError("ReaderId", "Читач з даним ідентифікаційним номером вже має запис про вибуття");
            if (ModelState.IsValid)
            {
                _context.Add(drop);
                await _context.SaveChangesAsync();
                await DropReader(drop.ReaderId);
                return RedirectToAction("Index");
            }
            ViewData["ReaderId"] = drop.ReaderId;
            ViewData["Fail"] = true;
            return View(drop);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var drop = await _context.Drops.SingleOrDefaultAsync(m => m.Id == id);
            if (drop == null)
                return NotFound();
            ViewData["ReaderId"] = drop.ReaderId;
            return View(drop);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit
            (
            int id,
            int curReaderId,
            [Bind("Id,Date,Couse,Note,ReaderId")]
            Drop drop
            )
        {
            if (id != drop.Id)
                return NotFound();
            if (!_context.Readers.Any(b => b.Id == drop.ReaderId))
                ModelState.AddModelError("ReaderId", "Читача з даним ідентифікаційним номером не існує");
            else if (_context.Drops.Any(d => d.ReaderId == drop.ReaderId && d.ReaderId != curReaderId))
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
            drop.ReaderId = curReaderId;
            return View(drop);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var drop = await _context.Drops
                .Include(d => d.Reader)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (drop == null)
                return NotFound();
            return View(drop);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drop = await _context.Drops.SingleOrDefaultAsync(m => m.Id == id);
            _context.Drops.Remove(drop);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        private bool DropExists(int id) => _context.Drops.Any(e => e.Id == id);
        private async Task<int> DropReader(int id)
        {
            var reader = await _context.Readers.Include(d => d.Drop)
                .SingleOrDefaultAsync(m => m.Id == id);
            reader.Status = ReaderStatus.Removed;
            _context.Update(reader);
            return await _context.SaveChangesAsync();
        }
    }
}
