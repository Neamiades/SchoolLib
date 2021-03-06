﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolLib.Data;
using SchoolLib.Models.Books;
using SchoolLib.Models.People;
using System.Globalization;
using System.Collections.Generic;

namespace SchoolLib.Controllers
{
    public class IssuancesController : Controller
    {
        private readonly ApplicationDbContext _context;
        IFormatProvider culture = new CultureInfo("uk-UA");

        public IssuancesController(ApplicationDbContext context) => _context = context;

        // GET: Issuances
        public async Task<IActionResult> Index(int? readerId)
        {
            var issuances = readerId.HasValue ? _context.Issuances
                .Include(i => i.Book)
                .Include(i => i.Reader)
                .Where(i => i.ReaderId == readerId)
                                              : _context.Issuances
                                              .Include(i => i.Book)
                                              .Include(i => i.Reader);
            issuances = issuances
                .OrderByDescending(i => DateTime.ParseExact(i.IssueDate, "dd.MM.yyyy", culture))
                .ThenByDescending(i => i.AcceptanceDate == null ? 
                                                 DateTime.Today :
                                                 DateTime.ParseExact(i.AcceptanceDate, "dd.MM.yyyy", culture));

            if (readerId.HasValue)
                ViewData["Reader"] = await _context.Readers
                    .SingleOrDefaultAsync(r => r.Id == readerId);
            
            return View(await issuances.ToListAsync());
        }

        // GET: Issuances/Details/5
        public async Task<IActionResult> Details(int? id, int? bookId)
        {
            if (id == null)
            {
                if (bookId != null)
                {
                    var bookIssuance = await _context.Issuances
                        .Include(i => i.Book)
                        .Include(i => i.Reader)
                        .SingleOrDefaultAsync(i => i.BookId == bookId && i.AcceptanceDate == null);
                    if (bookIssuance == null)
                        return RedirectToAction("Create", new { bookId = bookId });
                    return View(bookIssuance);
                }
                return NotFound();
            }

            var issuance = await _context.Issuances
                .Include(i => i.Book)
                .Include(i => i.Reader)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (issuance == null)
                return NotFound();

            return View(issuance);
        }

        // GET: Issuances/Create
        [HttpGet]
        public IActionResult Create(int? bookId, int? readerId)
        {
            ViewData["BookId"] = bookId;
            ViewData["ReaderId"] = readerId;
            ViewData["IssueDate"] = DateTime.Today.ToString("dd.MM.yyyy");
            return View();
        }

        // POST: Issuances/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create
            (
            [Bind("Id,IssueDate,AcceptanceDate,Couse,Note,ReaderSign,UserSign,ReaderId,BookId")]
            Issuance issuance
            )
        {
            if (!_context.Books.Any(b => b.Id == issuance.BookId))
            {
                ModelState.AddModelError("BookId", "Книги з даним інвентарним номером не існує");
                ViewData["BookError"] = true;
            }
            else if (_context.Issuances.Any(i => i.BookId == issuance.BookId && 
                    i.Book.Status.HasFlag(BookStatus.OnHands)))
            {
                ModelState.AddModelError("BookId", "Книга з даним інвентарним номером вже видана");
                ViewData["BookError"] = true;
            }
            if (!_context.Readers.Any(r => r.Id == issuance.ReaderId))
            {
                ModelState.AddModelError("ReaderId", "Читача з даним ідентифікаційним номером не існує");
                ViewData["ReaderError"] = true;
            }
            else if (_context.Readers.SingleOrDefault(r => r.Id == issuance.ReaderId).Status == ReaderStatus.Removed)
            {
                ModelState.AddModelError("ReaderId", "Книга не може бути видана читачу, що вибув");
                ViewData["ReaderError"] = true;
            }

            if (ModelState.IsValid)
            {
                _context.Add(issuance);
                await _context.SaveChangesAsync();
                await SetBook(issuance.BookId, BookStatus.OnHands);
                return RedirectToAction("Index", new { readerId = issuance.ReaderId});
            }
            ViewData["ReaderId"] = issuance.ReaderId;
            ViewData["BookId"] = issuance.BookId;
            return View(issuance);
        }

        // GET: Issuances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            
            var issuance = await _context.Issuances.SingleOrDefaultAsync(m => m.Id == id);
            if (issuance == null)
                return NotFound();
            return View(issuance);
        }

        // POST: Issuances/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit
            (
            int id,
            [Bind("Id,IssueDate,AcceptanceDate,Couse,Note,ReaderSign,UserSign,ReaderId,BookId")]
            Issuance issuance
            )
        {
            if (id != issuance.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issuance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssuanceExists(issuance.Id))
                    {
                        return NotFound();
                    }
                    else
                        throw;
                }
                return RedirectToAction("Index", new { readerId = issuance.ReaderId });
            }
            ViewData["BookId"] = issuance.BookId;
            ViewData["ReaderId"] = issuance.ReaderId;
            return View(issuance);
        }

        [HttpGet]
        // GET: Issuances/Return/5
        public async Task<IActionResult> Return(int? id)
        {
            if (id == null)
                return NotFound();

            var issuance = await _context.Issuances.SingleOrDefaultAsync(m => m.Id == id);
            if (issuance == null)
                return NotFound();
            ViewData["AcceptanceDate"] = DateTime.Today.ToString("dd.MM.yyyy");
            ViewData["BookId"] = issuance.BookId;
            ViewData["ReaderId"] = issuance.ReaderId;
            return View(issuance);
        }

        // POST: Issuances/Return/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return
            (
            int id,
            [Bind("Id,IssueDate,AcceptanceDate,Couse,Note,ReaderSign,UserSign,ReaderId,BookId")]
            Issuance issuance
            )
        {
            if (id != issuance.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issuance);
                    await _context.SaveChangesAsync();
                    await SetBook(issuance.BookId, BookStatus.InStock);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssuanceExists(issuance.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction("Index", new { readerId = issuance.ReaderId});
            }
            ViewData["AcceptanceDate"] = DateTime.Today.ToString("dd.MM.yyyy");
            ViewData["BookId"] = issuance.BookId;
            ViewData["ReaderId"] = issuance.ReaderId;
            return View(issuance);
        }

        // GET: Issuances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            
            var issuance = await _context.Issuances
                .Include(i => i.Book)
                .Include(i => i.Reader)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (issuance == null)
                return NotFound();
            
            return View(issuance);
        }

        // POST: Issuances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issuance = await _context.Issuances.SingleOrDefaultAsync(m => m.Id == id);
            _context.Issuances.Remove(issuance);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { readerId = issuance.ReaderId});
        }

        private bool IssuanceExists(int id)
        {
            return _context.Issuances.Any(e => e.Id == id);
        }

        private async Task<int> SetBook(int id, BookStatus status)
        {
            var book = await _context.Books.SingleOrDefaultAsync(b => b.Id == id);
            book.Status = status;
            _context.Update(book);
            return await _context.SaveChangesAsync();
        }
    }
}
