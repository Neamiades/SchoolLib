using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolLib.Data;
using SchoolLib.Models.Books;
using SchoolLib.Models.People;
using SchoolLib.Models.StatisticViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolLib.Controllers
{
    public class StatisticController : Controller
    {
        readonly ApplicationDbContext _context;
        static IFormatProvider culture = new CultureInfo("uk-UA");

        public StatisticController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Index() => View();
        public async Task<IActionResult> BookSearch(
            string startDate,
            string endDate,
            double? startPrice,
            double? endPrice,
            short? startPubDate,
            short? endPubDate,
            byte? startGrade,
            byte? endGrade,
            string actn
            )
        {
            var bookData = new BooksStatisticViewModel();
            IQueryable<Issuance> query = _context.Issuances.Include(i => i.Book);
            DateTime _start, _end;
            if (tpe(startDate, out _start) && tpe(endDate, out _end))
            {
                query = actn == "iss_books"
                    ? query.Where(i => pe(i.IssueDate) >= _start
                            && pe(i.IssueDate) <= _end)
                    : query.Where(i => i.AcceptanceDate != null
                            && pe(i.AcceptanceDate) >= _start
                            && pe(i.AcceptanceDate) <= _end)
                    ;
            }
            priceCheck(startPrice, endPrice, ref query);
            yearCheck(startPubDate, endPubDate, ref query);
            bookData.AdBooks = new List<AdditionalBook>();
            bookData.StBooks = new List<StudyBook>();
            foreach (var iss in (await query.ToListAsync()))
            {
                if(iss.Book.Discriminator == "AdditionalBook")
                    bookData.AdBooks.Add((AdditionalBook)iss.Book);
                else if (((StudyBook)iss.Book).Grade >= startGrade
                        && ((StudyBook)iss.Book).Grade <= endGrade)
                    bookData.StBooks.Add((StudyBook)iss.Book);
            }
            return PartialView("_Books", bookData);
        }

        public async Task<IActionResult> ReaderAttemptance(
            string startDate,
            string endDate
            )
        {
            var readerData = new ReadersStatisticViewModel();
            IQueryable<Issuance> queryIss = _context.Issuances.Include(i => i.Reader);
            DateTime _start, _end;
            if (tpe(startDate, out _start) && tpe(endDate, out _end))
            {
                queryIss = queryIss.Where(i => pe(i.IssueDate) >= _start
                            && pe(i.IssueDate) <= _end)
                        .GroupBy(i => i.ReaderId)
                        .Select(grp => grp.First())
                        .Concat(_context
                            .Issuances
                            .Include(i => i.Reader)
                            .Where(i => i.AcceptanceDate != null
                                && pe(i.AcceptanceDate) >= _start
                                && pe(i.AcceptanceDate) <= _end)
                            .GroupBy(i => i.ReaderId)
                            .Select(grp => grp.First()));
            }
            readerData.Students = new List<Student>();
            readerData.Workers = new List<Worker>();
            foreach (var iss in (await queryIss.ToListAsync()))
            {
                if (iss.Reader.Discriminator == "Student")
                    readerData.Students.Add((Student)iss.Reader);
                else
                    readerData.Workers.Add((Worker)iss.Reader);
            }
            return PartialView("_ReadersAcceptance", readerData);
        }
        public async Task<IActionResult> ReaderDrops(
            string startDate,
            string endDate
            )
        {
            var readerData = new ReadersStatisticViewModel();
            IQueryable<Drop> queryDrop = _context.Drops.Include(i => i.Reader);
            DateTime _start, _end;
            if (tpe(startDate, out _start) && tpe(endDate, out _end))
            {
                queryDrop = queryDrop.Where(i => pe(i.Date) >= _start
                            && pe(i.Date) <= _end);
            }
            readerData.Students = new List<Student>();
            readerData.Workers = new List<Worker>();
            foreach (var iss in (await queryDrop.ToListAsync()))
            {
                if (iss.Reader.Discriminator == "Student")
                    readerData.Students.Add((Student)iss.Reader);
                else
                    readerData.Workers.Add((Worker)iss.Reader);
            }
            return PartialView("_ReadersDrop", readerData);
        }
        public static bool tpe(string str_date, out DateTime date)
        {
            return DateTime.TryParseExact(str_date, "dd.MM.yyyy", culture,
                DateTimeStyles.AssumeLocal, out date);
        }
        public static DateTime pe(string str_date)
        {
            return DateTime.ParseExact(str_date, "dd.MM.yyyy", culture,
                DateTimeStyles.AssumeLocal);
        }

        public static void priceCheck(double? start, double? end, ref IQueryable<Issuance> query)
        {
            if (start.HasValue && end.HasValue)
                query = query
                    .Where
                    (i => Double.Parse(i.Book.Price) >= start
                        && Double.Parse(i.Book.Price) <= end);
        }
        public static void yearCheck(short? start, short? end, ref IQueryable<Issuance> query)
        {
            if (start.HasValue && end.HasValue)
                query = query
                    .Where
                    (i => i.Book.Published >= start
                        && i.Book.Published <= end);
        }
    }
}
