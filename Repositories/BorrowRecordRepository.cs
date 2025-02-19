using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Repositories
{
    public class BorrowRecordRepository : IBorrowRecordRepository
    {
        private readonly ApplicationDbContext _context;
        public BorrowRecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BorrowRecord>> GetBorrowRecordsByUserAsync(string userId)
        {
            return await _context.BorrowRecords
                .Include(b => b.Book)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }
        public async Task<BorrowRecord> GetActiveBorrowRecordAsync(int bookId, string userId)
        {
        var record = await _context.BorrowRecords
            .FirstOrDefaultAsync(b => b.BookId == bookId && b.UserId == userId && b.ReturnDate == null);

        if (record == null)
        {
            // Handle the absence of a record appropriately.
            // This could involve throwing an exception or returning a default value.
            throw new InvalidOperationException("No active borrow record found.");
        }

        return record;
        }

        public async Task<IEnumerable<BorrowRecord>> GetOverdueRecordsAsync()
        {
            return await _context.BorrowRecords
                .Include(b => b.Book)
                .Where(b => b.DueDate < DateTime.Now && b.ReturnDate == null)
                .ToListAsync();
        }
        public async Task AddAsync(BorrowRecord record)
        {
            _context.BorrowRecords.Add(record);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(BorrowRecord record)
        {
            _context.BorrowRecords.Update(record);
            await _context.SaveChangesAsync();
        }
    }
}
