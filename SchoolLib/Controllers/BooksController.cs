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
            ViewData["type"] = "Book";
            ViewData["name"] = null;
            ViewData["author"] = null;
            ViewData["authorCipher"] = null;
            ViewData["published"] = null;
            ViewData["note"] = null;
            ViewData["price"] = null;
            ViewData["bookTypesList"] = bookTypeDropdownList;
            ViewData["bookStatusList"] = bookStatusDropdownList;

            return View(await _context.Books.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(
            string type,
            string name,
            string author, 
            string authorCipher,
            short? published,
            string note,
            string price,
            BookStatus status)
        {
            ViewData["type"] = type;
            ViewData["name"] = name;
            ViewData["author"] = author;
            ViewData["authorCipher"] = authorCipher;
            ViewData["published"] = published;
            ViewData["price"] = price;
            ViewData["note"] = note;
            ViewData["bookTypesList"] = bookTypeDropdownList;
            ViewData["bookStatusList"] = bookStatusDropdownList;

            var books = _context.Books.Where(b => status.HasFlag(b.Status));
            if (type != "Book")
                books = books.Where(b => b.Discriminator == type);
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
                books = books.Where(b => Convert.ToDouble(b.Price.Replace('.',',')) == Convert.ToDouble(price.Replace('.', ',')));
            return View(await books.ToListAsync());
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

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckEmail(string email)
        {
            if (email == "admin@mail.ru" || email == "aaa@gmail.com")
                return Json(false);
            return Json(true);
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
