using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static CommuniLend.DataConstraints.ValidationConstants;
using static CommuniLend.Data.Models.Configurations.StaticSeedData.ReferenceSeedDate;

namespace CommuniLend.Data.Models.Configurations
{
    public class OpenLoanConfiguration : IEntityTypeConfiguration<OpenLoan>
    {
        public void Configure(EntityTypeBuilder<OpenLoan> builder)
        {
            builder.HasIndex(l => l.ToolId)
                .IsUnique();


            builder.HasOne(l => l.Owner)
                .WithMany(m => m.OpenLoans)
                .HasForeignKey(l => l.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasData(GetSeedData());
        }

        private static List<OpenLoan> GetSeedData()
        {
            return new List<OpenLoan>
            {
                new OpenLoan { Id = 1, ToolId = 3, OwnerId = 2, BorrowerId = 1, LoanDate = ReferenceDate.AddDays(-14), DueDate = ReferenceDate.AddDays(7),  Notes = "Borrowed for home renovation project" },
                new OpenLoan { Id = 2, ToolId = 7,  OwnerId = 6, BorrowerId = 2, LoanDate = ReferenceDate.AddDays(-10), DueDate = ReferenceDate.AddDays(11), Notes = "Needed for measuring deck" },
                new OpenLoan { Id = 3, ToolId = 11, OwnerId = 3, BorrowerId = 7, LoanDate = ReferenceDate.AddDays(-8),  DueDate = ReferenceDate.AddDays(13), Notes = "Cabinet installation" },
                new OpenLoan { Id = 4, ToolId = 14, OwnerId = 6, BorrowerId = 4, LoanDate = ReferenceDate.AddDays(-5),  DueDate = ReferenceDate.AddDays(16), Notes = "Basement repair work" },
                new OpenLoan { Id = 5, ToolId = 19, OwnerId = 4, BorrowerId = 5, LoanDate = ReferenceDate.AddDays(-3),  DueDate = ReferenceDate.AddDays(18), Notes = "Caulking bathroom tiles" },
                new OpenLoan { Id = 6, ToolId = 8,  OwnerId = 7, BorrowerId = 6, LoanDate = ReferenceDate.AddDays(-2),  DueDate = ReferenceDate.AddDays(19), Notes = "Engine repair project" },
                new OpenLoan { Id = 7, ToolId = 15, OwnerId = 7, BorrowerId = 3, LoanDate = ReferenceDate.AddDays(-1),  DueDate = ReferenceDate.AddDays(20), Notes = "Measuring for new flooring" },

            };
        }
    }
}
