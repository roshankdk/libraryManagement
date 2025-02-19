using LibraryManagement.Models;
using LibraryManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IBorrowRecordRepository _borrowRepo;
        private readonly IBookRepository _bookRepo;
        public BorrowService(IBorrowRecordRepository borrowRepo, IBookRepository bookRepo)
        {
            _borrowRepo = borrowRepo;
            _bookRepo = bookRepo;
        }
        public async Task<bool> BorrowBookAsync(int bookId, string userId, DateTime dueDate)
        {
            var book = await _bookRepo.GetByIdAsync(bookId);
            if (book == null || !book.IsAvailable)
                return false;

            book.IsAvailable = false;
            await _bookRepo.UpdateAsync(book);

            BorrowRecord record = new BorrowRecord
            {
                BookId = bookId,
                UserId = userId,
                BorrowDate = DateTime.Now,
                DueDate = dueDate
            };

            await _borrowRepo.AddAsync(record);
            return true;
        }
        public async Task<bool> ReturnBookAsync(int bookId, string userId)
        {
            var record = await _borrowRepo.GetActiveBorrowRecordAsync(bookId, userId);
            if (record == null)
                return false;

            record.ReturnDate = DateTime.Now;
            await _borrowRepo.UpdateAsync(record);

            var book = await _bookRepo.GetByIdAsync(bookId);
            book.IsAvailable = true;
            await _bookRepo.UpdateAsync(book);

            return true;
        }
        public async Task<IEnumerable<BorrowRecord>> GetUserBorrowRecordsAsync(string userId)
        {
            return await _borrowRepo.GetBorrowRecordsByUserAsync(userId);
        }
        public async Task<IEnumerable<BorrowRecord>> GetOverdueRecordsAsync()
        {
            return await _borrowRepo.GetOverdueRecordsAsync();
        }
    }
}
