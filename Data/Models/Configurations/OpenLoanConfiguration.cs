using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static CommuniLend.DataConstraints.ValidationConstants;

namespace CommuniLend.Data.Models.Configurations
{
    public class OpenLoanConfiguration : IEntityTypeConfiguration<OpenLoan>
    {
        public void Configure(EntityTypeBuilder<OpenLoan> builder)
        {
            builder.HasOne(l => l.Borrower)
                .WithMany(m => m.OpenLoans)
                .HasForeignKey(l => l.BorrowerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.Tool)
                .WithOne(t => t.OpenLoan)
                .HasForeignKey<OpenLoan>(l => l.ToolId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(l => l.ToolId)
                .IsUnique();

            builder.HasData(GetSeedData());
        }

        private static List<OpenLoan> GetSeedData()
        {
            return new List<OpenLoan>
            {
                new OpenLoan { Id = 1, ToolId = 3, BorrowerId = 1, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-14)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),  Notes = "Borrowed for home renovation project" },
                new OpenLoan { Id = 2, ToolId = 7, BorrowerId = 2, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-10)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(11)),  Notes = "Needed for measuring deck" },
                new OpenLoan { Id = 3, ToolId = 11, BorrowerId = 3, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-8)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(13)),  Notes = "Cabinet installation" },
                new OpenLoan { Id = 4, ToolId = 14, BorrowerId = 4, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(16)),  Notes = "Basement repair work" },
                new OpenLoan { Id = 5, ToolId = 19, BorrowerId = 5, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-3)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(18)),  Notes = "Caulking bathroom tiles" },
                new OpenLoan { Id = 6, ToolId = 8, BorrowerId = 6, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-2)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(19)),  Notes = "Engine repair project" },
                new OpenLoan { Id = 7, ToolId = 15, BorrowerId = 7, LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(20)),  Notes = "Measuring for new flooring" },
            };
        }
    }
}
