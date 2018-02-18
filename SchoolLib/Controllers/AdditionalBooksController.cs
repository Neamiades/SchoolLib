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

		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
				return NotFound();

			var additionalBook = await _context.AdditionalBooks
				.Include(ab => ab.Inventory)
				.Include(ab => ab.Provenance)
				.SingleOrDefaultAsync(ab => ab.Id == id);
			if (additionalBook == null)
				return NotFound();
			if (additionalBook.Status == BookStatus.OnHands)
			{
				ViewData["readerId"] =
					_context.Issuances.Where(i => i.BookId == additionalBook.Id && i.AcceptanceDate == null).SingleOrDefaultAsync()
						?.Result.ReaderId;
			}
			return View(additionalBook);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(
			[Bind("Id,Name,Author,AuthorCipher,Language,Cipher,Published,Price,Note")] AdditionalBook additionalBook
		)
		{
			if (_context.Books.Any(b => b.Id == additionalBook.Id))
			{
				ModelState.AddModelError("Id", "Книга з даним інвентарним номером все існує");
			}
			if (ModelState.IsValid)
			{
				additionalBook.Status = BookStatus.InStock;
				_context.Add(additionalBook);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Books");
			}
			return View(additionalBook);
		}

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

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(
			int curId,
			[Bind("Id,Name,Author,AuthorCipher,Language,Cipher,Published,Price,Note,Status")] AdditionalBook additionalBook
		)
		{
			if (_context.Books.Any(b => b.Id == additionalBook.Id && b.Id != curId))
			{
				ModelState.AddModelError("Id", "Книга з даним інвентарним номером все існує");
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
					throw;
				}
				return RedirectToAction("Index", "Books");
			}
			additionalBook.Id = curId;
			return View(additionalBook);
		}

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

		[HttpPost]
		[ActionName("Delete")]
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