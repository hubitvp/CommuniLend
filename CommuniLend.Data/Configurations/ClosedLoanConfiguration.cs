using CommuniLend.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static CommuniLend.Data.Configurations.StaticSeedData.ReferenceSeedDate;

namespace CommuniLend.Data.Configurations
{
    public class ClosedLoanConfiguration : IEntityTypeConfiguration<ClosedLoan>
    {
        public void Configure(EntityTypeBuilder<ClosedLoan> builder)
        {

            builder.HasOne(l => l.Tool)
                .WithMany(t => t.ClosedLoans)
                .HasForeignKey(l => l.ToolId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(l => l.Owner)
                .WithMany(m => m.ReturnedProvidedTools)
                .HasForeignKey(l => l.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(l => l.Borrower)
                .WithMany(m => m.ReturnedBorrowedTools)
                .HasForeignKey(l => l.BorrowerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasData(GetSeedData());
        }

        private static List<ClosedLoan> GetSeedData()
        {
            return new List<ClosedLoan>
            {
                new ClosedLoan { Id = 8,  ToolId = 4,  OwnerId = 3, BorrowerId = 2, LoanDate = ReferenceDate.AddDays(-40), DueDate = ReferenceDate.AddDays(-26), ReturnDate = ReferenceDate.AddDays(-25), Notes = "Shelf installation" },
                new ClosedLoan { Id = 9,  ToolId = 9,  OwnerId = 1, BorrowerId = 3, LoanDate = ReferenceDate.AddDays(-40), DueDate = ReferenceDate.AddDays(-26), ReturnDate = ReferenceDate.AddDays(-25), Notes = "Picture frame hanging" },
                new ClosedLoan { Id = 10, ToolId = 5,  OwnerId = 4, BorrowerId = 1, LoanDate = ReferenceDate.AddDays(-28), DueDate = ReferenceDate.AddDays(-14), ReturnDate = ReferenceDate.AddDays(-13), Notes = "Fence repair" },
                new ClosedLoan { Id = 11, ToolId = 13, OwnerId = 5, BorrowerId = 4, LoanDate = ReferenceDate.AddDays(-18), DueDate = ReferenceDate.AddDays(-4),  ReturnDate = ReferenceDate.AddDays(-3),  Notes = "Wall stud detection" },
                new ClosedLoan { Id = 12, ToolId = 2,  OwnerId = 1, BorrowerId = 3, LoanDate = ReferenceDate.AddDays(-24), DueDate = ReferenceDate.AddDays(-10), ReturnDate = ReferenceDate.AddDays(-9),  Notes = "Deck board cutting" },
                new ClosedLoan { Id = 13, ToolId = 10, OwnerId = 2, BorrowerId = 5, LoanDate = ReferenceDate.AddDays(-18), DueDate = ReferenceDate.AddDays(-4),  ReturnDate = ReferenceDate.AddDays(-3),  Notes = "Molding and trim work" },
                new ClosedLoan { Id = 14, ToolId = 16, OwnerId = 1, BorrowerId = 2, LoanDate = ReferenceDate.AddDays(-35), DueDate = ReferenceDate.AddDays(-21), ReturnDate = ReferenceDate.AddDays(-20), Notes = "Tree removal branch cutting" },
                new ClosedLoan { Id = 15, ToolId = 17, OwnerId = 2, BorrowerId = 1, LoanDate = ReferenceDate.AddDays(-28), DueDate = ReferenceDate.AddDays(-14), ReturnDate = ReferenceDate.AddDays(-13), Notes = "Floor sanding" },
                new ClosedLoan { Id = 16, ToolId = 18, OwnerId = 3, BorrowerId = 4, LoanDate = ReferenceDate.AddDays(-22), DueDate = ReferenceDate.AddDays(-8),  ReturnDate = ReferenceDate.AddDays(-7),  Notes = "Finishing hardwood" }
            };
        }
    }
}
