using LibraryManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Repositories
{
    public interface IBorrowRecordRepository
    {
        Task<IEnumerable<BorrowRecord>> GetBorrowRecordsByUserAsync(string userId);
        Task<BorrowRecord> GetActiveBorrowRecordAsync(int bookId, string userId);
        Task<IEnumerable<BorrowRecord>> GetOverdueRecordsAsync();
        Task AddAsync(BorrowRecord record);
        Task UpdateAsync(BorrowRecord record);
    }
}
