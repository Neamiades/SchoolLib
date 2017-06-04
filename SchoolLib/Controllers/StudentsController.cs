using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolLib.Data;
using SchoolLib.Models.People;
using System;
using System.Globalization;

namespace SchoolLib.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        IFormatProvider culture = new CultureInfo("uk-UA");

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Index", "Readers");
            //return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(s => s.Drop)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create
            (
            [Bind("Id,FirstName,SurName,Patronimic,Street,House,Apartment,Grade,FirstRegistrationDate,LastRegistrationDate,Note")]
            Student student
            )
        {
            if (_context.Readers.Any(s => s.Id == student.Id))
                ModelState.AddModelError("Id", "Учень з даним ідентифікаційним номером все існує");
            if (ModelState.IsValid && DateTime.ParseExact(student.FirstRegistrationDate, "dd.MM.yyyy", culture) >
                DateTime.ParseExact(student.LastRegistrationDate, "dd.MM.yyyy", culture))
            {
                ModelState.AddModelError("LastRegistrationDate", "Дата перереєстрації не може бути раніше ніж дата реєстрації");
            }
            if (ModelState.IsValid)
            {
                //student.LastRegistrationDate = student.FirstRegistrationDate;
                student.Status = ReaderStatus.Enabled;
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Readers");
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.SingleOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit
            (
            int curId,
            [Bind("Id,FirstName,SurName,Patronimic,Street,House,Apartment,Grade,FirstRegistrationDate,LastRegistrationDate,Status,Note")]
            Student student
            )
        {
            if (curId != student.Id)
                return NotFound();
            
            if (_context.Readers.Any(s => s.Id == student.Id && s.Id != curId))
                ModelState.AddModelError("Id", "Учень з даним ідентифікаційним номером все існує");
            if (ModelState.IsValid && DateTime.ParseExact(student.FirstRegistrationDate, "dd.MM.yyyy", culture) >
                DateTime.ParseExact(student.LastRegistrationDate, "dd.MM.yyyy", culture))
            {
                ModelState.AddModelError("LastRegistrationDate", "Дата перереєстрації не може бути раніше ніж дата реєстрації");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Readers");
            }
            student.Id = curId;
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .SingleOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(m => m.Id == id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
