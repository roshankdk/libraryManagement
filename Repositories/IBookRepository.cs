using LibraryManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task<IEnumerable<Book>> SearchAsync(string searchTerm);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);
    }
}
