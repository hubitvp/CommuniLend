using CommuniLend.Data.Configurations.StaticSeedData;
using CommuniLend.Data.Models;
using CommuniLend.Data.Models.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static CommuniLend.Data.Configurations.StaticSeedData.ReferenceSeedDate;

namespace CommuniLend.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public const string SeedPassword = "Password123!";

        private const string SeedPasswordHash =
            "AQAAAAIAAYagAAAAEF86nB57LUhgocPl9wkStNa2Svp+fHGNi/3hE6or/QJMzteRck+ThqQsgLDjNlHc8A==";

        private const string SeedSecurityStamp = "364894f5-ab00-5eb4-a4a7-40ac99d7f27f";

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(ValidationConstants.User.PhoneNumberMaxLength);

            builder.HasData(GetUserSeedData());
        }

        private static List<ApplicationUser> GetUserSeedData()
        {
            return new List<ApplicationUser>
            {
                CreateUser(1, "John Smith",       "john.smith@email.com",       "(555) 123-4567", "123 Main Street, Springfield, IL 62701", -90),
                CreateUser(2, "Sarah Johnson",    "sarah.johnson@email.com",    "(555) 234-5678", "456 Oak Avenue, Springfield, IL 62702",  -60),
                CreateUser(3, "Michael Chen",     "michael.chen@email.com",     "(555) 345-6789", "789 Pine Road, Springfield, IL 62703",   -45),
                CreateUser(4, "Emily Rodriguez",  "emily.rodriguez@email.com",  "(555) 456-7890", "321 Elm Street, Springfield, IL 62704",  -30),
                CreateUser(5, "David Williams",   "david.williams@email.com",   "(555) 567-8901", "654 Maple Drive, Springfield, IL 62705", -20),
                CreateUser(6, "Jessica Martinez", "jessica.martinez@email.com", "(555) 678-9012", "987 Cedar Lane, Springfield, IL 62706",  -15),
                CreateUser(7, "Robert Taylor",    "robert.taylor@email.com",    "(555) 789-0123", "147 Birch Court, Springfield, IL 62707", -10),
            };
        }

        private static ApplicationUser CreateUser(
            int number, string fullName, string email, string phone, string address, int joinedDaysFromReference)
        {
            return new ApplicationUser
            {
                Id = SeedUserIds.Get(number),
                UserName = email,
                NormalizedUserName = email.ToUpperInvariant(),
                Email = email,
                NormalizedEmail = email.ToUpperInvariant(),
                EmailConfirmed = true,
                PasswordHash = SeedPasswordHash,
                SecurityStamp = SeedSecurityStamp,
                ConcurrencyStamp = SeedSecurityStamp,
                PhoneNumber = phone,
                PhoneNumberConfirmed = true,
                FullName = fullName,
                Address = address,
                JoinDate = ReferenceDate.AddDays(joinedDaysFromReference),
                IsActive = true,
            };
        }
    }
}
