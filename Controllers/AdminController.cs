using LibraryManagement.Models;
using LibraryManagement.Repositories;
using LibraryManagement.Services;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IMemberRepository _memberRepository;
        private readonly IBorrowService _borrowService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(IBookService bookService, IMemberRepository memberRepository,
            IBorrowService borrowService, UserManager<ApplicationUser> userManager)
        {
            _bookService = bookService;
            _memberRepository = memberRepository;
            _borrowService = borrowService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            var totalBooks = (await _bookService.GetAllBooksAsync()).Count();
            var members = await _memberRepository.GetAllMembersAsync();
            var overdueRecords = await _borrowService.GetOverdueRecordsAsync();
            var model = new AdminDashboardViewModel
            {
                TotalBooks = totalBooks,
                TotalMembers = members.Count(),
                OverdueCount = overdueRecords.Count()
            };
            return View(model);
        }

        public async Task<IActionResult> Members()
        {
            var members = await _memberRepository.GetAllMembersAsync();
            return View(members);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            return RedirectToAction("Members");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }
            return RedirectToAction("Members");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    // Log errors or handle the failure as needed.
                    // Optionally, add an error message to ModelState or TempData.
                }
            }
            return RedirectToAction("Members");
        }

    }
}
