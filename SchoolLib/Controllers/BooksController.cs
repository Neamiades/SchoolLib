using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolLib.Data;
using SchoolLib.Models.Books;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;

namespace SchoolLib.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        List<SelectListItem> bookTypeDropdownList = new List<SelectListItem>();
        List<SelectListItem> bookStatusDropdownList = new List<SelectListItem>();

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
            bookTypeDropdownList.Add(new SelectListItem { Text = "Всі книжки", Value = "Book", Selected = true });
            bookTypeDropdownList.Add(new SelectListItem { Text = "Підручники", Value = "StudyBook", Selected = false });
            bookTypeDropdownList.Add(new SelectListItem { Text = "Додаткова література", Value = "AdditionalBook", Selected = false });

            bookStatusDropdownList.Add(new SelectListItem { Text = "Всі", Value = "All", Selected = true });
            bookStatusDropdownList.Add(new SelectListItem { Text = "В бібліотеці", Value = "InStock", Selected = false });
            bookStatusDropdownList.Add(new SelectListItem { Text = "У читача", Value = "OnHands", Selected = false });
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["bookTypesList"] = bookTypeDropdownList;
            ViewData["bookStatusList"] = bookStatusDropdownList;

            return View(await _context.Books.ToListAsync());
        }

        public async Task<IActionResult> Search(
            string type,
            int? id,
            string name,
            string author,
            string authorCipher,
            short? published,
            string note,
            string price,
            BookStatus status)
        {
            var books = _context.Books.Where(b => status.HasFlag(b.Status));
            if (type != "Book")
                books = books.Where(b => b.Discriminator == type);
            if (id.HasValue)
                books = books.Where(b => b.Id == id);
            if (!string.IsNullOrWhiteSpace(name))
                books = books.Where(b => b.Name == name);
            if (!string.IsNullOrWhiteSpace(author))
                books = books.Where(b => b.Author == author);
            if (!string.IsNullOrWhiteSpace(authorCipher))
                books = books.Where(b => b.AuthorCipher == authorCipher);
            if (!string.IsNullOrWhiteSpace(note))
                books = books.Where(b => b.Note == note);
            if (published.HasValue)
                books = books.Where(b => b.Published == published);
            if (!string.IsNullOrWhiteSpace(price))
                books = books.Where(b => Convert.ToDouble(b.Price.Replace('.', ',')) == Convert.ToDouble(price.Replace('.', ',')));

            ViewData["type"] = type == "Book"      ? typeof(Book)      : 
                               type == "StudyBook" ? typeof(StudyBook) : 
                                                     typeof(AdditionalBook);
            return PartialView("_Books", await books.ToListAsync());
        }
        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            else if (book.Discriminator == "AdditionalBook")
            {
                return RedirectToAction("Details", "AdditionalBooks", new { id = id });
            }

            return RedirectToAction("Details", "StudyBooks", new { id = id });
        }

        // GET: Readers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            else if (book.Discriminator == "AdditionalBook")
            {
                return RedirectToAction("Edit", "AdditionalBooks", new { id = id });
            }

            return RedirectToAction("Edit", "StudyBooks", new { id = id });
        }

        // GET: Readers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            else if (book.Discriminator == "AdditionalBook")
            {
                return RedirectToAction("Delete", "AdditionalBooks", new { id = id });
            }

            return RedirectToAction("Delete", "StudyBooks", new { id = id });
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
