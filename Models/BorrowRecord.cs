using System;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagement.Models; // Add this line if the Book class is in the same namespace

namespace LibraryManagement.Models
{
    public class BorrowRecord
    {
        public int Id { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; } = null!; // Use null-forgiving operator or initialize as needed

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser ApplicationUser { get; set; } = null!;

        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
