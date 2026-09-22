using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommuniLend.Data.Models.Configurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasData(GetMemberSeedData());
        }

        private static List<Member> GetMemberSeedData()
        {
            return new List<Member>
            {
                new Member { Id = 1, FullName = "John Smith", Email = "john.smith@email.com", PhoneNumber = "(555) 123-4567", Address = "123 Main Street, Springfield, IL 62701", JoinDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-90)), IsActive = true },
                new Member { Id = 2, FullName = "Sarah Johnson", Email = "sarah.johnson@email.com", PhoneNumber = "(555) 234-5678", Address = "456 Oak Avenue, Springfield, IL 62702", JoinDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-60)), IsActive = true },
                new Member { Id = 3, FullName = "Michael Chen", Email = "michael.chen@email.com", PhoneNumber = "(555) 345-6789", Address = "789 Pine Road, Springfield, IL 62703", JoinDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-45)), IsActive = true },
                new Member { Id = 4, FullName = "Emily Rodriguez", Email = "emily.rodriguez@email.com", PhoneNumber = "(555) 456-7890", Address = "321 Elm Street, Springfield, IL 62704", JoinDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-30)), IsActive = true },
                new Member { Id = 5, FullName = "David Williams", Email = "david.williams@email.com", PhoneNumber = "(555) 567-8901", Address = "654 Maple Drive, Springfield, IL 62705", JoinDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-20)), IsActive = true },
                new Member { Id = 6, FullName = "Jessica Martinez", Email = "jessica.martinez@email.com", PhoneNumber = "(555) 678-9012", Address = "987 Cedar Lane, Springfield, IL 62706", JoinDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-15)), IsActive = true },
                new Member { Id = 7, FullName = "Robert Taylor", Email = "robert.taylor@email.com", PhoneNumber = "(555) 789-0123", Address = "147 Birch Court, Springfield, IL 62707", JoinDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-10)), IsActive = true }
            };
        }
    }
}
