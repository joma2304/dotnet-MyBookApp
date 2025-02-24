using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBookApp.Data;
using MyBookApp.Models;

namespace MyBookApp.Controllers
{
    public class BookController : Controller
    {
        private readonly BookDbContext _context;

        public BookController(BookDbContext context)
        {
            _context = context;
        }

        // GET: Book
        public async Task<IActionResult> Index(string search)
        {
            var books = _context.Books.Include(b => b.User).AsQueryable();

            if (!string.IsNullOrEmpty(search)) //Ifall sök-input är ifylld
            {
                var searchLower = search.ToLower(); // Konvertera söksträngen till små bokstäver

                books = books.Where(b =>
                    b.Title.ToLower().Contains(searchLower) || // Titel (case-insensitive)
                    b.User.FirstName.ToLower().Contains(searchLower) || // Förnamn (case-insensitive)
                    b.User.LastName.ToLower().Contains(searchLower) // Efternamn (case-insensitive)
                );
            }

            return View(await books.ToListAsync());
        }


        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookModel = await _context.Books
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookModel == null)
            {
                return NotFound();
            }

            return View(bookModel);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(
                _context.Users.Select(u => new
                {
                    u.Id,
                    FullName = u.FirstName + " " + u.LastName
                }),
                "Id",
                "FullName"
            );

            return View();
        }


        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,Genre,Pages,LoanDate,UserId")] BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", bookModel.UserId);
            return View(bookModel);
        }

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookModel = await _context.Books.FindAsync(id);
            if (bookModel == null)
            {
                return NotFound();
            }

            // Skapa en SelectList med fullständigt namn för varje användare
            ViewData["UserId"] = new SelectList(
                _context.Users.Select(u => new
                {
                    u.Id,
                    FullName = u.FirstName + " " + u.LastName  // Kombinera FirstName och Lastname
                }),
                "Id",      // Id kommer vara det som skickas vid val
                "FullName", // Visa FullName i dropdownlistan
                bookModel.UserId // Förvalda värdet, alltså den nuvarande användaren som boken är kopplad till
            );

            return View(bookModel);
        }


        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,Genre,Pages,LoanDate,UserId")] BookModel bookModel)
        {
            if (id != bookModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookModelExists(bookModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", bookModel.UserId);
            return View(bookModel);
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookModel = await _context.Books
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookModel == null)
            {
                return NotFound();
            }

            return View(bookModel);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookModel = await _context.Books.FindAsync(id);
            if (bookModel != null)
            {
                _context.Books.Remove(bookModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookModelExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
