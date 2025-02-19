using LibraryManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    [Authorize(Roles = "Member")]
    public class BorrowController : Controller
    {
        private readonly IBorrowService _borrowService;
        public BorrowController(IBorrowService borrowService)
        {
            _borrowService = borrowService;
        }
        
        public async Task<IActionResult> MyBorrows()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                TempData["Error"] = "User identifier not found.";
                return RedirectToAction("Index", "Home");
            }
            var userId = userIdClaim.Value;
            var records = await _borrowService.GetUserBorrowRecordsAsync(userId);
            return View(records);
        }
        
        [HttpPost]
        public async Task<IActionResult> Borrow(int bookId, DateTime dueDate)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                TempData["Error"] = "User identifier not found.";
                return RedirectToAction("Index", "Home");
            }
            var userId = userIdClaim.Value;
            var success = await _borrowService.BorrowBookAsync(bookId, userId, dueDate);
            if (!success)
                TempData["Error"] = "Unable to borrow the book. It might not be available.";
            return RedirectToAction("Index", "Book");
        }
        
        [HttpPost]
        public async Task<IActionResult> Return(int bookId)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                TempData["Error"] = "User identifier not found.";
                return RedirectToAction("Index", "Home");
            }
            var userId = userIdClaim.Value;
            var success = await _borrowService.ReturnBookAsync(bookId, userId);
            if (!success)
                TempData["Error"] = "Unable to return the book.";
            return RedirectToAction("MyBorrows");
        }
    }
}
