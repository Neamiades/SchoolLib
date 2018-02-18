using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolLib.Data;
using SchoolLib.Models.Books;
using SchoolLib.Models.People;
using SchoolLib.Models.StatisticViewModels;

namespace SchoolLib.Controllers
{
	public class StatisticController : Controller
	{
		private static readonly IFormatProvider Culture = new CultureInfo("uk-UA");
		private readonly ApplicationDbContext _context;

		public StatisticController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View("Error");
		}

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

			if (Tpe(startDate, out var start) && Tpe(endDate, out var end))
			{
				query = actn == "iss_books"
						? query.Where(i => Pe(i.IssueDate) >= start
						                   && Pe(i.IssueDate) <= end)
						: query.Where(i => i.AcceptanceDate != null
						                   && Pe(i.AcceptanceDate) >= start
						                   && Pe(i.AcceptanceDate) <= end)
					;
			}

			PriceCheck(startPrice, endPrice, ref query);
			YearCheck(startPubDate, endPubDate, ref query);

			bookData.AdBooks = new List<AdditionalBook>();
			bookData.StBooks = new List<StudyBook>();

			foreach (var iss in await query.ToListAsync())
			{
				if (iss.Book.Discriminator == "AdditionalBook")
					bookData.AdBooks.Add((AdditionalBook) iss.Book);
				else if (((StudyBook) iss.Book).Grade >= startGrade
				         && ((StudyBook) iss.Book).Grade <= endGrade)
					bookData.StBooks.Add((StudyBook) iss.Book);
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

			if (Tpe(startDate, out var start) && Tpe(endDate, out var end))
			{
				queryIss = queryIss.Where(i => Pe(i.IssueDate) >= start
				                               && Pe(i.IssueDate) <= end)
					.GroupBy(i => i.ReaderId)
					.Select(grp => grp.First())
					.Concat(_context
						.Issuances
						.Include(i => i.Reader)
						.Where(i => i.AcceptanceDate != null
						            && Pe(i.AcceptanceDate) >= start
						            && Pe(i.AcceptanceDate) <= end)
						.GroupBy(i => i.ReaderId)
						.Select(grp => grp.First()));
			}

			readerData.Students = new List<Student>();
			readerData.Workers = new List<Worker>();

			foreach (var iss in await queryIss.ToListAsync())
			{
				if (iss.Reader.Discriminator == "Student")
					readerData.Students.Add((Student) iss.Reader);
				else
					readerData.Workers.Add((Worker) iss.Reader);
			}

			return PartialView("_ReadersAcceptance", readerData);
		}

		public async Task<IActionResult> ReaderDrops
		(
			string startDate,
			string endDate
		)
		{
			var readerData = new ReadersStatisticViewModel();
			IQueryable<Drop> queryDrop = _context.Drops.Include(i => i.Reader);

			if (Tpe(startDate, out var start) && Tpe(endDate, out var end))
			{
				queryDrop = queryDrop.Where(i => Pe(i.Date) >= start
				                                 && Pe(i.Date) <= end);
			}

			readerData.Students = new List<Student>();
			readerData.Workers = new List<Worker>();

			foreach (var iss in await queryDrop.ToListAsync())
			{
				if (iss.Reader.Discriminator == "Student")
					readerData.Students.Add((Student) iss.Reader);
				else
					readerData.Workers.Add((Worker) iss.Reader);
			}

			return PartialView("_ReadersDrop", readerData);
		}

		public static bool Tpe(string strDate, out DateTime date)
		{
			return DateTime.TryParseExact(strDate, "dd.MM.yyyy", Culture,
				DateTimeStyles.AssumeLocal, out date);
		}

		public static DateTime Pe(string strDate)
		{
			return DateTime.ParseExact(strDate, "dd.MM.yyyy", Culture,
				DateTimeStyles.AssumeLocal);
		}

		public static void PriceCheck(double? start, double? end, ref IQueryable<Issuance> query)
		{
			if (start.HasValue && end.HasValue)
			{
				query = query.Where(i => double.Parse(i.Book.Price) >= start && double.Parse(i.Book.Price) <= end);
			}
		}

		public static void YearCheck(short? start, short? end, ref IQueryable<Issuance> query)
		{
			if (start.HasValue && end.HasValue)
			{
				query = query.Where(i => i.Book.Published >= start && i.Book.Published <= end);
			}
		}
	}
}