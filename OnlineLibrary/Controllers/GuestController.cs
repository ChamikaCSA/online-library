using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Context;
using OnlineLibrary.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Controllers
{
    public class GuestController : Controller
    {
        private readonly LibraryContext _context;

        public GuestController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Guest/SearchBooks
        [HttpGet]
        public async Task<IActionResult> SearchBooks()
        {
            return View(await _context.Book.ToListAsync());
        }

        // POST: Guest/SearchBooks
        [HttpPost]
        public async Task<IActionResult> SearchBooks(string searchString)
        {
            var books = from b in _context.Book
                        select b;

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString) || s.Author.Contains(searchString));
            }

            return View(await books.ToListAsync());
        }

        // GET: Guest/ReserveBook
        public async Task<IActionResult> ReserveBook(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Guest/ReserveBook
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReserveBook(int id, string guestName, string guestEmail)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var guest = await _context.Guest.FirstOrDefaultAsync(g => g.Email == guestEmail);
            if (guest == null)
            {
                guest = new Guest
                {
                    Name = guestName,
                    Email = guestEmail
                };
                _context.Guest.Add(guest);
            }
            await _context.SaveChangesAsync();

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Now,
                Status = "Reserved",
                Book_BookID = book.BookID,
                Guest_GuestID = guest.GuestID
            };

            _context.Reservation.Add(reservation);

            book.Status = "Reserved";
            _context.Update(book);

            await _context.SaveChangesAsync();

            TempData["ReservationSuccessMessage"] = "Book reserved successfully!";
            return RedirectToAction(nameof(SearchBooks));
        }

        // GET: Guest/LeaveMessage
        [HttpGet]
        public IActionResult LeaveMessage()
        {
            return View();
        }

        // POST: Guest/LeaveMessage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LeaveMessage(string guestName, string guestEmail, [Bind("MessageID,MessageContent,Date")] Message message)
        {
            if (ModelState.IsValid)
            {
                var guest = await _context.Guest.FirstOrDefaultAsync(g => g.Email == guestEmail);
                if (guest == null)
                {
                    guest = new Guest
                    {
                        Name = guestName,
                        Email = guestEmail
                    };
                    _context.Guest.Add(guest);
                }
                await _context.SaveChangesAsync();

                message.Guest_GuestID = guest.GuestID;

                message.Date = DateTime.Now;
                _context.Add(message);

                await _context.SaveChangesAsync();

                TempData["MessageSuccessMessage"] = "Message sent successfully!";
                return RedirectToAction(nameof(LeaveMessage));
            }
            return View(message);
        }
    }
}
