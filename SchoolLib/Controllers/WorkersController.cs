using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolLib.Data;
using SchoolLib.Models.People;

namespace SchoolLib.Controllers
{
	public class WorkersController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IFormatProvider _culture = new CultureInfo("uk-UA");

		public WorkersController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Workers
		public IActionResult Index()
		{
			return RedirectToAction("Index", "Readers");
		}

		// GET: Workers/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var worker = await _context.Workers.Include(w => w.Drop)
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
			ViewData["FirstRegistrationDate"] = ViewData["LastRegistrationDate"] = DateTime.Today.ToString("dd.MM.yyyy");
			ViewBag.FreeId = _context.Readers.Max(w => w.Id) + 1;

			return View();
		}

		// POST: Workers/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create
		(
			[Bind(
				"Id,FirstName,SurName,Patronimic,Street,House,Apartment,Position,FirstRegistrationDate,LastRegistrationDate,Note")]
			Worker worker
		)
		{
			if (_context.Readers.Any(w => w.Id == worker.Id))
				ModelState.AddModelError("Id", "Співробітник з даним ідентифікаційним номером все існує");
			if (ModelState.IsValid)
			{
				worker.LastRegistrationDate = worker.FirstRegistrationDate;
				worker.Status = ReaderStatus.Enabled;
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
			int curId,
			[Bind(
				"Id,FirstName,SurName,Patronimic,Street,House,Apartment,Position,FirstRegistrationDate,LastRegistrationDate,Status,Note")]
			Worker worker
		)
		{
			if (curId != worker.Id)
				return BadRequest();

			if (_context.Readers.Any(w => w.Id == worker.Id && w.Id != curId))
				ModelState.AddModelError("Id", "Співробітник з даним ідентифікаційним номером все існує");
			if (ModelState.IsValid &&
			    DateTime.ParseExact(worker.FirstRegistrationDate, "dd.MM.yyyy", _culture) >
			    DateTime.ParseExact(worker.LastRegistrationDate, "dd.MM.yyyy", _culture))
			{
				ModelState.AddModelError("LastRegistrationDate", "Дата перереєстрації не може бути раніше ніж дата реєстрації");
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
						return NotFound();
					throw;
				}
				return RedirectToAction("Index", "Readers");
			}
			worker.Id = curId;
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
		[HttpPost]
		[ActionName("Delete")]
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