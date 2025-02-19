using LibraryManagement.Models;
using LibraryManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    [Authorize(Roles = "Admin,Librarian")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();
            return View(books);
        }
        
        [HttpGet]
        public IActionResult Create() => View();
        
        [HttpPost]
        public async Task<IActionResult> Create(Book model)
        {
            if(ModelState.IsValid)
            {
                await _bookService.AddBookAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if(book == null)
                return NotFound();
            return View(book);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(Book model)
        {
            if(ModelState.IsValid)
            {
                await _bookService.UpdateBookAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if(book == null)
                return NotFound();
            return View(book);
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var books = await _bookService.SearchBooksAsync(searchTerm);
            return View("Index", books);
        }
    }
}
