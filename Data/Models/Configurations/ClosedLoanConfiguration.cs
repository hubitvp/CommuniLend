using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommuniLend.Data.Models.Configurations
{
    public class ClosedLoanConfiguration : IEntityTypeConfiguration<ClosedLoan>
    {
        public void Configure(EntityTypeBuilder<ClosedLoan> builder)
        {

            builder.HasOne(l => l.Borrower)
                .WithMany(m => m.ClosedLoans)
                .HasForeignKey(l => l.BorrowerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.Tool)
                .WithMany(t => t.ClosedLoans)
                .HasForeignKey(l => l.ToolId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(GetSeedData());
        }

        private static List<ClosedLoan> GetSeedData()
        {
            return new List<ClosedLoan>
            {
                new ClosedLoan { Id = 8, ToolId = 4, BorrowerId = 2, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-45)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-31)), ReturnDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-30)), Notes = "Shelf installation" },
                new ClosedLoan { Id = 9, ToolId = 9, BorrowerId = 3, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-40)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-26)), ReturnDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-25)), Notes = "Picture frame hanging" },
                new ClosedLoan { Id = 10, ToolId = 5, BorrowerId = 4, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-35)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-21)), ReturnDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-20)), Notes = "Fence repair" },
                new ClosedLoan { Id = 11, ToolId = 13, BorrowerId = 5, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-30)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-16)), ReturnDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-15)), Notes = "Wall stud detection" },
                new ClosedLoan { Id = 12, ToolId = 2, BorrowerId = 6, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-25)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-11)), ReturnDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-10)), Notes = "Deck board cutting" },
                new ClosedLoan { Id = 13, ToolId = 10, BorrowerId = 7, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-20)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-6)), ReturnDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)), Notes = "Molding and trim work" },
                new ClosedLoan { Id = 14, ToolId = 16, BorrowerId = 1, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-35)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-21)), ReturnDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-20)), Notes = "Tree removal branch cutting" },
                new ClosedLoan { Id = 15, ToolId = 17, BorrowerId = 2, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-28)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-14)), ReturnDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-13)), Notes = "Floor sanding" },
                new ClosedLoan { Id = 16, ToolId = 18, BorrowerId = 3, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-22)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-8)), ReturnDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-7)), Notes = "Finishing hardwood" }
            };
        }
    }
}
