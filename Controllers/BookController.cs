using LibraryManagement.Models;
using LibraryManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    [Authorize(Roles = "Admin,Librarian,Member")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IBorrowService _borrowService;

        public BookController(IBookService bookService, IBorrowService borrowService)
        {
            _bookService = bookService;
            _borrowService = borrowService;
        }
        
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();
            if (User.IsInRole("Admin") || User.IsInRole("Librarian"))
            {
                return View("AdminIndex", books);
            }
            else if (User.IsInRole("Member"))
            {
                return View("MemberIndex", books);
            }
            return Forbid();
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Librarian")]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookService.AddBookAsync(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookService.UpdateBookAsync(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Borrow(int bookId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dueDate = DateTime.Now.AddDays(14); // Example due date set to 14 days from now
            if (userId == null)
            {
                return BadRequest("User ID cannot be null.");
            }

            var result = await _borrowService.BorrowBookAsync(bookId, userId, dueDate);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Unable to borrow the book.");
        }
    }
}