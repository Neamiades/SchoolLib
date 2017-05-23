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
            ViewData["name"] = string.Empty;
            ViewData["author"] = string.Empty;
            ViewData["authorCipher"] = string.Empty;
            ViewData["published"] = string.Empty;
            ViewData["note"] = string.Empty;
            ViewData["price"] = string.Empty;
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
            short published,
            string note,
            decimal price,
            BookStatus status)
        {
            //switch (type)
            //{
            //    case "StudyBook":
            //        bookTypeDropdownList[1].Selected = true;
            //        break;
            //    case "AdditionalBook":
            //        bookTypeDropdownList[2].Selected = true;
            //        break;
            //    default:
            //        bookTypeDropdownList[0].Selected = true;
            //        break;
            //}

            ViewData["type"] = type;
            ViewData["name"] = name;
            ViewData["author"] = author;
            ViewData["authorCipher"] = authorCipher;
            ViewData["published"] = published;
            ViewData["note"] = note;
            ViewData["price"] = price;
            ViewData["bookTypesList"] = bookTypeDropdownList;
            ViewData["bookStatusList"] = bookStatusDropdownList;

            var books = _context.Books.Where(b => b.Discriminator == type && b.Status == status);
            if (!string.IsNullOrWhiteSpace(name))
                books = books.Where(b => b.Name == name);
            if (!string.IsNullOrWhiteSpace(author))
                books = books.Where(b => b.Author == author);
            if (!string.IsNullOrWhiteSpace(authorCipher))
                books = books.Where(b => b.AuthorCipher == authorCipher);
            if (!string.IsNullOrWhiteSpace(note))
                books = books.Where(b => b.Name == name);
            if (published != 0)
                books = books.Where(b => b.Published == published);
            if (price != 0)
                books = books.Where(b => b.Price == price);

            return View(await books.ToListAsync());
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
