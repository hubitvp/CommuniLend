using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommuniLend.Data.Models.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(GetCategorySeedData());
        }

        private static List<Category> GetCategorySeedData()
        {
            return new List<Category>
            {
                new Category { Id = 1, Name = "Power Tools", Description = "Electric and cordless power tools for drilling, cutting, sanding, and more" },
                new Category { Id = 2, Name = "Hand Tools", Description = "Manual hand tools including wrenches, hammers, screwdrivers, pliers, and socket sets" },
                new Category { Id = 3, Name = "Measuring & Finishing Tools", Description = "Tools for measuring, leveling, detecting studs, and finishing work" }
            };
        }
    }
}
