using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Context;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers
{
    public class AdminController : Controller
    {
        private readonly LibraryContext _context;

        public AdminController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Admin/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Admin/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Admin model)
        {
            if (ModelState.IsValid)
            {
                // Check admin credentials
                if (IsValidAdmin(model.Username, model.Password))
                {
                    // Redirect to admin dashboard upon successful login
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }

            // If login fails, return to login page with error message
            return View(model);
        }

        // Method to validate admin credentials
        private bool IsValidAdmin(string username, string password)
        {
            // Retrieve the admin with the provided username from the database
            var admin = _context.Admin.SingleOrDefault(a => a.Username == username);

            // If admin is found and password matches, return true
            if (admin != null && admin.Password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // GET: Admin/Dashboard
        public IActionResult Dashboard()
        {
            return View();
        }

        // GET: Admin/Book
        public async Task<IActionResult> Books()
        {
            return View(await _context.Book.ToListAsync());
        }

        // GET: Admin/AddBook
        public IActionResult AddBook()
        {
            return View();
        }

        // POST: Admin/AddBook
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBook([Bind("BookID,Title,Author,ISBN,Genre,Status")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Books));
            }
            return View(book);
        }

        // GET: Admin/EditBook
        public async Task<IActionResult> EditBook(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Admin/EditBook
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBook(int id, [Bind("BookID,Title,Author,ISBN,Genre,Status")] Book book)
        {
            if (id != book.BookID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Books));
            }
            return View(book);
        }

        // GET: Admin/DeleteBook
        public async Task<IActionResult> DeleteBook(int? id)
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

        // POST: Admin/DeleteBook
        [HttpPost, ActionName("DeleteBook")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedBook(int id)
        {
            var book = await _context.Book.FindAsync(id);

            if (book == null) 
            {
                return NotFound();
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Books));
        }

        // GET: Admin/CD
        public async Task<IActionResult> CDs()
        {
            return View(await _context.CD.ToListAsync());
        }

        // GET: Admin/AddCD
        public IActionResult AddCD()
        {
            return View();
        }

        // POST: Admin/AddCD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCD([Bind("CDID,Title,Artist,Genre")] CD cd)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CDs));
            }
            return View(cd);
        }

        // GET: Admin/EditCD
        public async Task<IActionResult> EditCD(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cd = await _context.CD.FindAsync(id);
            if (cd == null)
            {
                return NotFound();
            }
            return View(cd);
        }

        // POST: Admin/EditCD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCD(int id, [Bind("CDID,Title,Artist,Genre")] CD cd)
        {
            if (id != cd.CDID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CDExists(cd.CDID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(CDs));
            }
            return View(cd);
        }

        // GET: Admin/DeleteCD
        public async Task<IActionResult> DeleteCD(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cd = await _context.CD
                .FirstOrDefaultAsync(m => m.CDID == id);
            if (cd == null)
            {
                return NotFound();
            }

            return View(cd);
        }

        // POST: Admin/DeleteCD
        [HttpPost, ActionName("DeleteCD")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedCD(int id)
        {
            var cd = await _context.CD.FindAsync(id);

            if (cd == null)
            {
                return NotFound();
            }

            _context.CD.Remove(cd);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(CDs));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.BookID == id);
        }

        private bool CDExists(int id)
        {
            return _context.CD.Any(e => e.CDID == id);
        }

        // GET: Admin/Issues
        public async Task<IActionResult> Issues()
        {
            return View(await _context.Issue.ToListAsync());
        }

        // GET: Admin/UpdateReturn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateReturn(int issueId)
        {
            var issue = await _context.Issue.FindAsync(issueId);

            if (issue == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(issue.Book_BookID);

            if (book == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FirstOrDefaultAsync(r => r.Book_BookID == book.BookID);

            issue.ReturnDate = DateTime.Today;
            issue.Status = "Returned";
            _context.Update(issue);

            if (reservation != null)
            {
                reservation.Status = "Available";
                _context.Update(reservation);
            }

            book.Status = "Available";
            _context.Update(book);

            await _context.SaveChangesAsync();

            TempData["UpdateReturnSuccessMessage"] = "Return status updated successfully!";
            return RedirectToAction(nameof(Issues));
        }

        // GET: Admin/IssueBook
        public async Task<IActionResult> IssueBook()
        {
            ViewBag.Books = await _context.Book.ToListAsync();
            ViewBag.Guests = await _context.Guest.ToListAsync();
            ViewBag.Admins = await _context.Admin.ToListAsync();
            return View();
        }

        // POST: Admin/IssueBook
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IssueBook(int bookId, int adminId, int? guestId, string guestName, string guestEmail)
        {
            var book = await _context.Book.FindAsync(bookId);
            var admin = await _context.Admin.FindAsync(adminId);

            if (book == null || admin == null )
            {
                return NotFound();
            }

            Guest? guest = null;

            if (guestId.HasValue)
            {
                guest = await _context.Guest.FindAsync(guestId.Value);
            }
            else
            {
                if (!string.IsNullOrEmpty(guestName) && !string.IsNullOrEmpty(guestEmail))
                {
                    guest = new Guest
                    {
                        Name = guestName,
                        Email = guestEmail
                    };
                    _context.Guest.Add(guest);
                    await _context.SaveChangesAsync();
                }
            }

            if (guest == null)
            {
                return NotFound();
            }

            var issue = new Issue
            {
                IssueDate = DateTime.Now,
                Status = "Issued",
                Admin_AdminID = adminId,
                Guest_GuestID = guest.GuestID,
                Book_BookID = bookId
            };
            _context.Issue.Add(issue);

            var reservation = await _context.Reservation.FirstOrDefaultAsync(r => r.Book_BookID == book.BookID);

            if (reservation != null)
            {
                reservation.Status = "Issued";
                _context.Update(reservation);
            }

            book.Status = "Issued";
            _context.Update(book);

            await _context.SaveChangesAsync();

            TempData["IssueBookSuccessMessage"] = "Book issued successfully!";
            return RedirectToAction(nameof(Issues));
        }
    }
}
