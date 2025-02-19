using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public interface IBorrowService
    {
        Task<bool> BorrowBookAsync(int bookId, string userId, DateTime dueDate);
        Task<bool> ReturnBookAsync(int bookId, string userId);
        Task<IEnumerable<BorrowRecord>> GetUserBorrowRecordsAsync(string userId);
        Task<IEnumerable<BorrowRecord>> GetOverdueRecordsAsync();
    }
}
