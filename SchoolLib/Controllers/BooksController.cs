using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolLib.Data;
using SchoolLib.Models.Books;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SchoolLib.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        readonly List<SelectListItem> _bookTypeDropdownList = new List<SelectListItem>();
        readonly List<SelectListItem> _bookStatusDropdownList = new List<SelectListItem>();

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
            _bookTypeDropdownList.Add(new SelectListItem { Text = "Всі", Value = "Book", Selected = true });
            _bookTypeDropdownList.Add(new SelectListItem { Text = "Підручники", Value = "StudyBook", Selected = false });
            _bookTypeDropdownList.Add(new SelectListItem { Text = "Додаткова література", Value = "AdditionalBook", Selected = false });

            _bookStatusDropdownList.Add(new SelectListItem { Text = "Неважливо", Value = "Any", Selected = true });
            _bookStatusDropdownList.Add(new SelectListItem { Text = "В бібліотеці", Value = "InStock", Selected = false });
            _bookStatusDropdownList.Add(new SelectListItem { Text = "У читача", Value = "OnHands", Selected = false });
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["bookTypesList"] = _bookTypeDropdownList;
            ViewData["bookStatusList"] = _bookStatusDropdownList;

            return View();
        }

        public async Task<IActionResult> Search(
            string type,
            int? id,
            string name,
            string author,
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
                return RedirectToAction("Details", "AdditionalBooks", new { id });
            }

            return RedirectToAction("Details", "StudyBooks", new { id });
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
                return RedirectToAction("Edit", "AdditionalBooks", new { id });
            }

            return RedirectToAction("Edit", "StudyBooks", new { id });
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
                return RedirectToAction("Delete", "AdditionalBooks", new { id });
            }

            return RedirectToAction("Delete", "StudyBooks", new { id });
        }
    }
}
