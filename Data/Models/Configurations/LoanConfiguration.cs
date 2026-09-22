using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommuniLend.Data.Models.Configurations
{
    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.HasOne(l => l.Borrower)
                .WithMany(m => m.Loans)
                .HasForeignKey(l => l.BorrowerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.Tool)
                .WithMany(t => t.Loans)
                .HasForeignKey(l => l.ToolId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(GetLoanSeedData());
        }

        private static List<Loan> GetLoanSeedData()
        {
            return new List<Loan>
            {
                // Active loans (no return date)
                new Loan { Id = 1, ToolId = 3, BorrowerId = 1, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-14)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7)), ReturnDate = null, Notes = "Borrowed for home renovation project" },
                new Loan { Id = 2, ToolId = 7, BorrowerId = 2, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-10)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(11)), ReturnDate = null, Notes = "Needed for measuring deck" },
                new Loan { Id = 3, ToolId = 11, BorrowerId = 3, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-8)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(13)), ReturnDate = null, Notes = "Cabinet installation" },
                new Loan { Id = 4, ToolId = 14, BorrowerId = 4, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(16)), ReturnDate = null, Notes = "Basement repair work" },
                new Loan { Id = 5, ToolId = 19, BorrowerId = 5, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-3)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(18)), ReturnDate = null, Notes = "Caulking bathroom tiles" },
                new Loan { Id = 6, ToolId = 8, BorrowerId = 6, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-2)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(19)), ReturnDate = null, Notes = "Engine repair project" },
                new Loan { Id = 7, ToolId = 15, BorrowerId = 7, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(20)), ReturnDate = null, Notes = "Measuring for new flooring" },

                // Returned loans
                new Loan { Id = 8, ToolId = 4, BorrowerId = 2, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-45)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-31)), ReturnDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-30)), Notes = "Shelf installation" },
                new Loan { Id = 9, ToolId = 9, BorrowerId = 3, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-40)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-26)), ReturnDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-25)), Notes = "Picture frame hanging" },
                new Loan { Id = 10, ToolId = 5, BorrowerId = 4, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-35)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-21)), ReturnDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-20)), Notes = "Fence repair" },
                new Loan { Id = 11, ToolId = 13, BorrowerId = 5, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-30)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-16)), ReturnDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-15)), Notes = "Wall stud detection" },
                new Loan { Id = 12, ToolId = 2, BorrowerId = 6, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-25)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-11)), ReturnDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-10)), Notes = "Deck board cutting" },
                new Loan { Id = 13, ToolId = 10, BorrowerId = 7, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-20)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-6)), ReturnDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)), Notes = "Molding and trim work" },
                new Loan { Id = 14, ToolId = 16, BorrowerId = 1, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-35)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-21)), ReturnDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-20)), Notes = "Tree removal branch cutting" },
                new Loan { Id = 15, ToolId = 17, BorrowerId = 2, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-28)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-14)), ReturnDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-13)), Notes = "Floor sanding" },
                new Loan { Id = 16, ToolId = 18, BorrowerId = 3, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-22)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-8)), ReturnDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-7)), Notes = "Finishing hardwood" }
            };
        }
    }
}
