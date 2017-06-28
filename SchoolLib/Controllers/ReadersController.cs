using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolLib.Data;
using SchoolLib.Models.People;
using System;

namespace SchoolLib.Controllers
{
    public class ReadersController : Controller
    {
        private readonly ApplicationDbContext _context;
        List<SelectListItem> readerTypeDropdownList = new List<SelectListItem>();
        List<SelectListItem> readerStatusDropdownList = new List<SelectListItem>();

        public ReadersController(ApplicationDbContext context)
        {
            _context = context;
            readerTypeDropdownList.Add(new SelectListItem { Text = "Неважливо", Value = "Reader", Selected = true });
            readerTypeDropdownList.Add(new SelectListItem { Text = "Учень", Value = "Student", Selected = false });
            readerTypeDropdownList.Add(new SelectListItem { Text = "Співробітник", Value = "Worker", Selected = false });

            readerStatusDropdownList.Add(new SelectListItem { Text = "Неважливо", Value = "Any", Selected = true });
            readerStatusDropdownList.Add(new SelectListItem { Text = "Активний", Value = "Enabled", Selected = false });
            readerStatusDropdownList.Add(new SelectListItem { Text = "Деактивований", Value = "Disabled", Selected = false });
            readerStatusDropdownList.Add(new SelectListItem { Text = "Вибув", Value = "Removed", Selected = false });
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["readerTypeList"] = readerTypeDropdownList;
            ViewData["readerStatusList"] = readerStatusDropdownList;

            return View();
        }
        
        public async Task<IActionResult> SearchActions(
            string type,
            int? id,
            string firstName,
            string surName,
            string patronimic,
            string street,
            string house,
            short? apartment,
            string note,
            ReaderStatus status,
            string actn)
        {
            List<Reader> readersList;
            var readers = _context.Readers.Where(r => status.HasFlag(r.Status));
            if (type != "Reader")
                readers = readers.Where(r => r.Discriminator == type);
            if (id.HasValue)
                readers = readers.Where(r => r.Id == id);
            if (!string.IsNullOrWhiteSpace(firstName))
                readers = readers.Where(r => r.FirstName == firstName);
            if (!string.IsNullOrWhiteSpace(surName))
                readers = readers.Where(r => r.SurName == surName);
            if (!string.IsNullOrWhiteSpace(patronimic))
                readers = readers.Where(r => r.Patronimic == patronimic);
            if (!string.IsNullOrWhiteSpace(street))
                readers = readers.Where(r => r.Street == street);
            if (!string.IsNullOrWhiteSpace(house))
                readers = readers.Where(r => r.House == house);
            if (apartment.HasValue)
                readers = readers.Where(r => r.Apartment == apartment);
            if (!string.IsNullOrWhiteSpace(note))
                readers = readers.Where(r => r.Note == note);

            ViewData["type"] = type == "Reader"  ? typeof(Reader)  :
                               type == "Student" ? typeof(Student) :
                                                   typeof(Worker);
            if (actn == "activate")
            {
                readersList = await readers.Where(r => r.Status == ReaderStatus.Disabled).ToListAsync();
                foreach (var r in readersList)
                {
                    r.Status = ReaderStatus.Enabled;
                    r.LastRegistrationDate = DateTime.Today.ToString("dd.MM.yyyy");
                }
                _context.UpdateRange(readersList);
                await _context.SaveChangesAsync();
            }
            else if (actn == "restore")
            {
                readersList = await readers.Where(r => r.Status == ReaderStatus.Removed).ToListAsync();
                foreach (var r in readersList)
                {
                    r.Drop.Note = $"Відновлений {DateTime.Today.ToString("dd.MM.yyyy")}";
                    r.Status = ReaderStatus.Disabled;
                    r.LastRegistrationDate = DateTime.Today.ToString("dd.MM.yyyy");
                }
                _context.UpdateRange(readersList);
                await _context.SaveChangesAsync();
            }
            readersList = await readers.ToListAsync();
            return PartialView("_Readers", readersList);
        }

        public async Task<IActionResult> Activate(
            string type,
            int? id,
            string firstName,
            string surName,
            string patronimic,
            string street,
            string house,
            short? apartment,
            string lastRegDate,
            string firstRegDate,
            string note,
            ReaderStatus status)
        {
            var readers = _context.Readers.Where(r => status.HasFlag(r.Status));
            if (type != "Reader")
                readers = readers.Where(r => r.Discriminator == type);
            if (id.HasValue)
                readers = readers.Where(r => r.Id == id);
            if (!string.IsNullOrWhiteSpace(firstName))
                readers = readers.Where(r => r.FirstName == firstName);
            if (!string.IsNullOrWhiteSpace(surName))
                readers = readers.Where(r => r.SurName == surName);
            if (!string.IsNullOrWhiteSpace(patronimic))
                readers = readers.Where(r => r.Patronimic == patronimic);
            if (!string.IsNullOrWhiteSpace(street))
                readers = readers.Where(r => r.Street == street);
            if (!string.IsNullOrWhiteSpace(house))
                readers = readers.Where(r => r.House == house);
            if (apartment.HasValue)
                readers = readers.Where(r => r.Apartment == apartment);
            /* !todo:Исправить сравнение строк на сравнение дат */
            if (!string.IsNullOrWhiteSpace(lastRegDate))
                readers = readers.Where(r => r.LastRegistrationDate == lastRegDate);
            if (!string.IsNullOrWhiteSpace(firstRegDate))
                readers = readers.Where(r => r.FirstRegistrationDate == firstRegDate);
            /* !todo */
            if (!string.IsNullOrWhiteSpace(note))
                readers = readers.Where(r => r.Note == note);

            string view = "";

            return PartialView(view, await readers.ToListAsync());
        }

        // GET: Readers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reader = await _context.Readers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (reader == null)
            {
                return NotFound();
            }
            else if (reader.Discriminator == "Student")
            {
                return RedirectToAction("Details", "Students", new { id = id });
            }

            return RedirectToAction("Details", "Workers", new { id = id });
        }

        // GET: Readers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reader = await _context.Readers.SingleOrDefaultAsync(m => m.Id == id);
            if (reader == null)
            {
                return NotFound();
            }
            else if (reader.Discriminator == "Student")
            {
                return RedirectToAction("Edit", "Students", new { id = id });
            }

            return RedirectToAction("Edit", "Workers", new { id = id });
        }

        // GET: Readers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reader = await _context.Readers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (reader == null)
            {
                return NotFound();
            }
            else if (reader.Discriminator == "Student")
            {
                return RedirectToAction("Delete", "Students", new { id = id });
            }

            return RedirectToAction("Delete", "Workers", new { id = id });
        }

        private bool ReaderExists(int id)
        {
            return _context.Readers.Any(e => e.Id == id);
        }
    }
}
