using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolLib.Data;
using SchoolLib.Models.People;

namespace SchoolLib.Controllers
{
    public class WorkersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Workers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Workers.ToListAsync());
        }

        // GET: Workers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await _context.Workers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        // GET: Workers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Workers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create
            (
            [Bind("Id,FirstName,SurName,Patronimic,Street,House,Apartment,Postition,LastRegistrationDate,FirstRegistrationDate,Status,Note")]
            Worker worker
            )
        {
            if (_context.Workers.Any(w => w.Id == worker.Id))
            {
                ModelState.AddModelError("Id", "Співробітник з даним ідентифікаційним номером все існує");
            }
            if (ModelState.IsValid)
            {
                worker.Status = ReaderStatus.Disabled;
                _context.Add(worker);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Readers");
            }
            return View(worker);
        }

        // GET: Workers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await _context.Workers.SingleOrDefaultAsync(m => m.Id == id);
            if (worker == null)
            {
                return NotFound();
            }
            return View(worker);
        }

        // POST: Workers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit
            (
            int id,
            [Bind("Id,FirstName,SurName,Patronimic,Street,House,Apartment,Position,LastRegistrationDate,FirstRegistrationDate,Status,Note")]
            Worker worker
            )
        {
            if (id != worker.Id)
            {
                return NotFound();
            }
            if (_context.Workers.Any(w => w.Id == worker.Id && w.Id != id))
            {
                ModelState.AddModelError("Id", "Співробітник з даним ідентифікаційним номером все існує");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(worker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkerExists(worker.Id))
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
            return View(worker);
        }

        // GET: Workers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await _context.Workers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        // POST: Workers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var worker = await _context.Workers.SingleOrDefaultAsync(m => m.Id == id);
            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool WorkerExists(int id)
        {
            return _context.Workers.Any(e => e.Id == id);
        }
    }
}
