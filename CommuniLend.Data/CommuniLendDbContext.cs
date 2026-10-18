using CommuniLend.Data.Configurations;
using CommuniLend.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CommuniLend.Data
{
    public class CommuniLendDbContext(DbContextOptions<CommuniLendDbContext> options)
        : IdentityDbContext<ApplicationUser>(options)
    {

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Tool> Tools { get; set; } = null!;
        public virtual DbSet<ClosedLoan> ClosedLoans { get; set; } = null!;
        public virtual DbSet<OpenLoan> OpenLoans { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ClosedLoanConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new OpenLoanConfiguration());
            modelBuilder.ApplyConfiguration(new ToolConfiguration());
        }
    }
}
