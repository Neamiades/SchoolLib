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
	public class InventoriesController : Controller
	{
		private readonly List<SelectListItem> _bookTypeDropdownList;
		private readonly ApplicationDbContext _context;

		public InventoriesController(ApplicationDbContext context)
		{
			_context = context;

			_bookTypeDropdownList = new List<SelectListItem>
			{
				new SelectListItem {Text = "Всі", Value = "Book", Selected = true},
				new SelectListItem {Text = "Підручники", Value = "StudyBook", Selected = false},
				new SelectListItem {Text = "Додаткова література", Value = "AdditionalBook", Selected = false}
			};
		}

		// GET: Inventories
		public async Task<IActionResult> Index()
		{
			var applicationDbContext = _context.Inventories.Include(i => i.Book);

			ViewData["bookTypesList"] = _bookTypeDropdownList;

			ViewBag.InvCount = await _context.Inventories.CountAsync();
			ViewBag.StudyBooksInvCount = await _context.Inventories.CountAsync(i => i.Book.Discriminator == "StudyBook");
			ViewBag.AddBooksInvCount = await _context.Inventories.CountAsync(i => i.Book.Discriminator == "AdditionalBook");

			return View(await applicationDbContext.ToListAsync());
		}

		public async Task<IActionResult> Search
		(
			string type,
			int? bookId,
			short? year,
			int? actNumber,
			string couse,
			string note
		)
		{
			var inventories = _context.Inventories.Include(i => i.Book).AsQueryable();

			if (type != "Book")
				inventories = inventories.Where(i => i.Book.Discriminator == type);

			if (bookId.HasValue)
				inventories = inventories.Where(i => i.BookId == bookId);

			if (year.HasValue)
				inventories = inventories.Where(i => i.Year == year);

			if (actNumber.HasValue)
				inventories = inventories.Where(i => i.ActNumber == actNumber);

			if (!string.IsNullOrWhiteSpace(couse))
				inventories = inventories.Where(i => i.Couse == couse);

			if (!string.IsNullOrWhiteSpace(note))
				inventories = inventories.Where(i => i.Note == note);

			return PartialView("_Inventories", await inventories.ToListAsync());
		}

		// GET: Inventories/Details/5
		public async Task<IActionResult> Details(int? id, int? bookId)
		{
			if (id == null)
			{
				if (bookId != null)
					return RedirectToAction("Create", new {bookId});
				return NotFound();
			}

			var inventory = await _context.Inventories
				.Include(i => i.Book)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (inventory == null)
				return NotFound();

			return View(inventory);
		}

		// GET: Inventories/Create
		[HttpGet]
		public IActionResult Create(int? bookId)
		{
			ViewData["BookId"] = bookId;
			return View();
		}

		// POST: Inventories/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,ActNumber,Year,Couse,Note,BookId")] Inventory inventory)
		{
			if (!_context.Books.Any(b => b.Id == inventory.BookId))
			{
				ModelState.AddModelError("BookId", "Книги з даним інвентарним номером не існує");
			}
			else if (_context.Inventories.Any(i => i.BookId == inventory.BookId))
			{
				ModelState.AddModelError("BookId", "Книга з даним інвентарним номером вже має інвентарний запис");
			}

			if (ModelState.IsValid)
			{
				inventory.Book = _context.Books.SingleOrDefault(b => b.Id == inventory.BookId);
				_context.Add(inventory);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			ViewData["BookId"] = inventory.BookId;
			ViewData["Fail"] = true;

			return View(inventory);
		}

		// GET: Inventories/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var inventory = await _context.Inventories.SingleOrDefaultAsync(m => m.Id == id);
			if (inventory == null)
			{
				return NotFound();
			}
			return View(inventory);
		}

		// POST: Inventories/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, int curBookId,
			[Bind("Id,ActNumber,Year,Couse,Note,BookId")] Inventory inventory)
		{
			if (id != inventory.Id)
				return NotFound();

			if (!_context.Books.Any(b => b.Id == inventory.BookId))
				ModelState.AddModelError("BookId", "Книги з даним інвентарним номером не існує");

			if (_context.Inventories.Any(i => i.BookId == inventory.BookId && i.BookId != curBookId))
				ModelState.AddModelError("BookId", "Книга з даним інвентарним номером вже має інвентарний запис");

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(inventory);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!InventoryExists(inventory.Id))
						return NotFound();

					throw;
				}

				return RedirectToAction("Index");
			}

			inventory.BookId = curBookId;

			return View(inventory);
		}

		// GET: Inventories/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
				return NotFound();

			var inventory = await _context.Inventories
				.Include(i => i.Book)
				.SingleOrDefaultAsync(m => m.Id == id);

			if (inventory == null)
				return NotFound();

			return View(inventory);
		}

		// POST: Inventories/Delete/5
		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var inventory = await _context.Inventories.SingleOrDefaultAsync(m => m.Id == id);
			_context.Inventories.Remove(inventory);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		private bool InventoryExists(int id)
		{
			return _context.Inventories.Any(e => e.Id == id);
		}
	}
}