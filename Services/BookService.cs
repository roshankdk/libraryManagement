using LibraryManagement.Models;
using LibraryManagement.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;
        public BookService(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }
        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepo.GetAllAsync();
        }
        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _bookRepo.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm)
        {
            return await _bookRepo.SearchAsync(searchTerm);
        }
        public async Task AddBookAsync(Book book)
        {
            await _bookRepo.AddAsync(book);
        }
        public async Task UpdateBookAsync(Book book)
        {
            await _bookRepo.UpdateAsync(book);
        }
        public async Task DeleteBookAsync(int id)
        {
            await _bookRepo.DeleteAsync(id);
        }
    }
}
