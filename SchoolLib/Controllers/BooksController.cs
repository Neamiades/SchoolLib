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

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
            bookTypeDropdownList.Add(new SelectListItem { Text = "Всі книжки", Value = "book", Selected = true });
            bookTypeDropdownList.Add(new SelectListItem { Text = "Підручники", Value = "stbook", Selected = false });
            bookTypeDropdownList.Add(new SelectListItem { Text = "Додаткова література", Value = "adbook", Selected = false });
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["type"] = "book";
            ViewData["name"] = string.Empty;
            ViewData["author"] = string.Empty;
            ViewData["price"] = string.Empty;
            ViewData["status"] = string.Empty;
            ViewData["books"] = await _context.Books.ToListAsync();//new List<Book>();
            ViewData["bookTypesList"] = bookTypeDropdownList;

            return View(await _context.Books.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string type, string name, string author, string price, string status)
        {
            ViewData["type"] = type;
            ViewData["name"] = name;
            ViewData["author"] = author;
            ViewData["price"] = price;
            ViewData["status"] = status;
            ViewData["books"] = await _context.Books.ToListAsync();

            switch (type)
            {
                case "sbook":
                    bookTypeDropdownList[1].Selected = true;
                    break;
                case "adbook":
                    bookTypeDropdownList[2].Selected = true;
                    break;
                default:
                    bookTypeDropdownList[0].Selected = true;
                    break;
            }
            ViewData["bookTypesList"] = bookTypeDropdownList;
            return View();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
