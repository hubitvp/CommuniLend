using CommuniLend.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static CommuniLend.Data.Models.Configurations.StaticSeedData.ReferenceSeedDate;

namespace CommuniLend.Data.Models.Configurations
{
    public class ToolConfiguration : IEntityTypeConfiguration<Tool>
    {
        public void Configure(EntityTypeBuilder<Tool> builder)
        {
            builder.HasOne(t => t.Category)
                .WithMany(c => c.Tools)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(t => t.Owner)
                .WithMany(m => m.OwnedTools)
                .HasForeignKey(t => t.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(GetToolSeedData());
        }

        private static List<Tool> GetToolSeedData()
        {
            return new List<Tool>
            {
                new Tool { Id = 1, Name = "Cordless Drill", Description = "18V lithium-ion cordless drill with carry case", Condition = ToolCondition.New, ReplacementValue = 89.99m, ImageUrl = "/images/tools/cordless-drill.jpg", DateAdded = ReferenceDate.AddDays(-30), CategoryId = 1, OwnerId = 1 },
                new Tool { Id = 2, Name = "Circular Saw", Description = "7.25\" circular saw for wood cutting", Condition = ToolCondition.Good, ReplacementValue = 65.50m, ImageUrl = "/images/tools/circular-saw.jpg", DateAdded = ReferenceDate.AddDays(-25), CategoryId = 1, OwnerId = 1 },
                new Tool { Id = 3, Name = "Adjustable Wrench Set", Description = "Set of 3 adjustable wrenches in various sizes", Condition = ToolCondition.Good, ReplacementValue = 25.99m, ImageUrl = "/images/tools/wrench-set.jpg", DateAdded = ReferenceDate.AddDays(-20), CategoryId = 2, OwnerId = 2},
                new Tool { Id = 4, Name = "Level", Description = "24\" aluminum spirit level", Condition = ToolCondition.Fair, ReplacementValue = 18.75m, ImageUrl = "/images/tools/level.jpg", DateAdded = ReferenceDate.AddDays(-43), CategoryId = 3, OwnerId = 3 },
                new Tool { Id = 5, Name = "Hammer Set", Description = "Set of 5 hammers with different head weights", Condition = ToolCondition.Good, ReplacementValue = 35.00m, ImageUrl = "/images/tools/hammer-set.jpg", DateAdded = ReferenceDate.AddDays(-29), CategoryId = 2, OwnerId = 4},
                new Tool { Id = 6, Name = "Jigsaw", Description = "Orbital jigsaw for curved cuts", Condition = ToolCondition.New, ReplacementValue = 120.00m, ImageUrl = "/images/tools/jigsaw.jpg", DateAdded = ReferenceDate.AddDays(-8), CategoryId = 1, OwnerId = 5 },
                new Tool { Id = 7, Name = "Tape Measure", Description = "25ft retractable tape measure", Condition = ToolCondition.Good, ReplacementValue = 12.50m, ImageUrl = "/images/tools/tape-measure.jpg", DateAdded = ReferenceDate.AddDays(-14), CategoryId = 3, OwnerId = 6},
                new Tool { Id = 8, Name = "Socket Set", Description = "Complete socket set with ratchets", Condition = ToolCondition.Fair, ReplacementValue = 45.99m, ImageUrl = "/images/tools/socket-set.jpg", DateAdded = ReferenceDate.AddDays(-5), CategoryId = 2, OwnerId = 7},
                new Tool { Id = 9, Name = "Pliers Set", Description = "Set of 4 different pliers", Condition = ToolCondition.Good, ReplacementValue = 28.00m, ImageUrl = "/images/tools/pliers-set.jpg", DateAdded = ReferenceDate.AddDays(-60), CategoryId = 2, OwnerId = 1},
                new Tool { Id = 10, Name = "Miter Saw", Description = "10\" compound miter saw", Condition = ToolCondition.New, ReplacementValue = 199.99m, ImageUrl = "/images/tools/miter-saw.jpg", DateAdded = ReferenceDate.AddDays(-50), CategoryId = 1, OwnerId = 2},
                new Tool { Id = 11, Name = "Screwdriver Set", Description = "Set of 12 screwdrivers (Phillips and flathead)", Condition = ToolCondition.Good, ReplacementValue = 19.99m, ImageUrl = "/images/tools/screwdriver-set.jpg", DateAdded = ReferenceDate.AddDays(-12), CategoryId = 2, OwnerId = 3},
                new Tool { Id = 12, Name = "Power Drill", Description = "12V compact power drill", Condition = ToolCondition.Fair, ReplacementValue = 59.99m, ImageUrl = "/images/tools/power-drill.jpg", DateAdded = ReferenceDate, CategoryId = 1, OwnerId = 4 },
                new Tool { Id = 13, Name = "Stud Finder", Description = "Digital stud finder with deep scan", Condition = ToolCondition.New, ReplacementValue = 42.50m, ImageUrl = "/images/tools/stud-finder.jpg", DateAdded = ReferenceDate.AddDays(-19), CategoryId = 3, OwnerId = 5},
                new Tool { Id = 14, Name = "Flashlight", Description = "LED flashlight with adjustable beam", Condition = ToolCondition.Good, ReplacementValue = 15.00m, ImageUrl = "/images/tools/flashlight.jpg", DateAdded = ReferenceDate.AddDays(-10), CategoryId = 3, OwnerId = 6},
                new Tool { Id = 15, Name = "Laser Measure", Description = "Digital laser distance measurer", Condition = ToolCondition.New, ReplacementValue = 79.99m, ImageUrl = "/images/tools/laser-measure.jpg", DateAdded = ReferenceDate.AddDays(-6), CategoryId = 3, OwnerId = 7},
                new Tool { Id = 16, Name = "Reciprocating Saw", Description = "Cordless reciprocating saw for demolition", Condition = ToolCondition.Good, ReplacementValue = 145.00m, ImageUrl = "/images/tools/reciprocating-saw.jpg", DateAdded = ReferenceDate.AddDays(-50), CategoryId = 1, OwnerId = 1},
                new Tool { Id = 17, Name = "Orbital Sander", Description = "5\" random orbital sander", Condition = ToolCondition.Good, ReplacementValue = 99.99m, ImageUrl = "/images/tools/orbital-sander.jpg", DateAdded = ReferenceDate.AddDays(-40), CategoryId = 1, OwnerId = 2},
                new Tool { Id = 18, Name = "Nail Set", Description = "Set of 4 nail sets", Condition = ToolCondition.Worn, ReplacementValue = 8.99m, ImageUrl = "/images/tools/nail-set.jpg", DateAdded = ReferenceDate.AddDays(-35), CategoryId = 2, OwnerId = 3},
                new Tool { Id = 19, Name = "Caulking Gun", Description = "Heavy-duty caulking gun", Condition = ToolCondition.Good, ReplacementValue = 22.00m, ImageUrl = "/images/tools/caulking-gun.jpg", DateAdded = ReferenceDate.AddDays(-8), CategoryId = 2, OwnerId = 4},
                new Tool { Id = 20, Name = "Utility Knife", Description = "Retractable utility knife with replacement blades", Condition = ToolCondition.Fair, ReplacementValue = 10.50m, ImageUrl = "/images/tools/utility-knife.jpg", DateAdded = ReferenceDate, CategoryId = 2, OwnerId = 5},
                new Tool { Id = 21, Name = "Impact Driver", Description = "20V impact driver", Condition = ToolCondition.New, ReplacementValue = 99.99m, ImageUrl = "/images/tools/impact-driver.jpg", DateAdded = ReferenceDate, CategoryId = 1, OwnerId = 6},
                new Tool { Id = 22, Name = "Brad Nailer", Description = "18-gauge brad nailer", Condition = ToolCondition.New, ReplacementValue = 75.00m, ImageUrl = "/images/tools/brad-nailer.jpg", DateAdded = ReferenceDate, CategoryId = 1, OwnerId = 7},
                new Tool { Id = 23, Name = "Stud Finder Kit", Description = "Stud finder with level", Condition = ToolCondition.Good, ReplacementValue = 35.99m, ImageUrl = "/images/tools/stud-kit.jpg", DateAdded = ReferenceDate, CategoryId = 3, OwnerId = 1},
                new Tool { Id = 24, Name = "Torpedo Level", Description = "9\" torpedo level", Condition = ToolCondition.Good, ReplacementValue = 16.50m, ImageUrl = "/images/tools/torpedo-level.jpg", DateAdded = ReferenceDate, CategoryId = 3, OwnerId = 2},
                new Tool { Id = 25, Name = "Ratchet Set", Description = "24-piece ratchet socket set", Condition = ToolCondition.Good, ReplacementValue = 55.00m, ImageUrl = "/images/tools/ratchet-set.jpg", DateAdded = ReferenceDate, CategoryId = 2, OwnerId = 3},
                new Tool { Id = 26, Name = "Bench Vise", Description = "4\" bench vise with swivel base", Condition = ToolCondition.Fair, ReplacementValue = 65.00m, ImageUrl = "/images/tools/bench-vise.jpg", DateAdded = ReferenceDate, CategoryId = 2, OwnerId = 4},
                new Tool { Id = 27, Name = "Angle Grinder", Description = "4.5\" angle grinder", Condition = ToolCondition.Good, ReplacementValue = 85.99m, ImageUrl = "/images/tools/angle-grinder.jpg", DateAdded = ReferenceDate, CategoryId = 1, OwnerId = 5},
                new Tool { Id = 28, Name = "Pneumatic Nailer", Description = "Pneumatic finishing nailer", Condition = ToolCondition.Fair, ReplacementValue = 60.00m, ImageUrl = "/images/tools/pneumatic-nailer.jpg", DateAdded = ReferenceDate, CategoryId = 1, OwnerId = 6},
                new Tool { Id = 29, Name = "Magnetic Level", Description = "24\" magnetic level", Condition = ToolCondition.New, ReplacementValue = 21.99m, ImageUrl = "/images/tools/magnetic-level.jpg", DateAdded = ReferenceDate, CategoryId = 3, OwnerId = 7},
                new Tool { Id = 30, Name = "Power Saw", Description = "Portable circular power saw", Condition = ToolCondition.Worn, ReplacementValue = 55.00m, ImageUrl = "/images/tools/power-saw.jpg", DateAdded = ReferenceDate, CategoryId = 1, OwnerId = 1},
                new Tool { Id = 31, Name = "Allen Key Set", Description = "Complete hex key set", Condition = ToolCondition.Good, ReplacementValue = 14.99m, ImageUrl = "/images/tools/allen-key-set.jpg", DateAdded = ReferenceDate, CategoryId = 2, OwnerId = 2},
                new Tool { Id = 32, Name = "Pipe Wrench", Description = "18\" adjustable pipe wrench", Condition = ToolCondition.Fair, ReplacementValue = 32.50m, ImageUrl = "/images/tools/pipe-wrench.jpg", DateAdded = ReferenceDate, CategoryId = 2, OwnerId = 3},
                new Tool { Id = 33, Name = "Hacksaw", Description = "12\" hacksaw with blade", Condition = ToolCondition.Good, ReplacementValue = 13.00m, ImageUrl = "/images/tools/hacksaw.jpg", DateAdded = ReferenceDate, CategoryId = 2, OwnerId = 4},
                new Tool { Id = 34, Name = "Drill Bit Set", Description = "30-piece drill bit set", Condition = ToolCondition.Good, ReplacementValue = 18.50m, ImageUrl = "/images/tools/drill-bit-set.jpg", DateAdded = ReferenceDate, CategoryId = 2, OwnerId = 5},
                new Tool { Id = 35, Name = "Combination Square", Description = "12\" combination square", Condition = ToolCondition.New, ReplacementValue = 19.99m, ImageUrl = "/images/tools/combination-square.jpg", DateAdded = ReferenceDate, CategoryId = 3, OwnerId = 6},
                new Tool { Id = 36, Name = "Flashlight Set", Description = "Set of 3 LED flashlights", Condition = ToolCondition.Good, ReplacementValue = 35.00m, ImageUrl = "/images/tools/flashlight-set.jpg", DateAdded = ReferenceDate, CategoryId = 3, OwnerId = 7},
                new Tool { Id = 37, Name = "Pry Bar", Description = "15\" flat pry bar", Condition = ToolCondition.Good, ReplacementValue = 14.50m, ImageUrl = "/images/tools/pry-bar.jpg", DateAdded = ReferenceDate, CategoryId = 2, OwnerId = 1},
                new Tool { Id = 38, Name = "Oscillating Multi-Tool", Description = "Cordless oscillating tool with accessories", Condition = ToolCondition.New, ReplacementValue = 129.99m, ImageUrl = "/images/tools/multi-tool.jpg", DateAdded = ReferenceDate, CategoryId = 1, OwnerId = 2},
                new Tool { Id = 39, Name = "Bar Clamp Set", Description = "Set of 2 bar clamps (12\" and 24\")", Condition = ToolCondition.Fair, ReplacementValue = 42.00m, ImageUrl = "/images/tools/bar-clamp-set.jpg", DateAdded = ReferenceDate, CategoryId = 2, OwnerId = 3},
                new Tool { Id = 40, Name = "Nail Gun", Description = "Electric nail gun", Condition = ToolCondition.Good, ReplacementValue = 79.99m, ImageUrl = "/images/tools/nail-gun.jpg", DateAdded = ReferenceDate, CategoryId = 1, OwnerId = 4},
                new Tool { Id = 41, Name = "Carpenter's Square", Description = "24\" carpenter's speed square", Condition = ToolCondition.Worn, ReplacementValue = 11.00m, ImageUrl = "/images/tools/carpenters-square.jpg", DateAdded = ReferenceDate, CategoryId = 3, OwnerId = 5},
                new Tool { Id = 42, Name = "Dust Extractor", Description = "Shop dust extractor", Condition = ToolCondition.Good, ReplacementValue = 125.00m, ImageUrl = "/images/tools/dust-extractor.jpg", DateAdded = ReferenceDate, CategoryId = 1, OwnerId = 6},
                new Tool { Id = 43, Name = "Adjustable Square", Description = "6\" adjustable square", Condition = ToolCondition.New, ReplacementValue = 17.50m, ImageUrl = "/images/tools/adjustable-square.jpg", DateAdded = ReferenceDate, CategoryId = 3, OwnerId = 7},
                new Tool { Id = 44, Name = "Cable Protector", Description = "Heavy-duty cable protector", Condition = ToolCondition.Good, ReplacementValue = 28.00m, ImageUrl = "/images/tools/cable-protector.jpg", DateAdded = ReferenceDate, CategoryId = 2, OwnerId = 1},
                new Tool { Id = 45, Name = "Bench Grinder", Description = "6\" bench grinder", Condition = ToolCondition.Fair, ReplacementValue = 95.00m, ImageUrl = "/images/tools/bench-grinder.jpg", DateAdded = ReferenceDate, CategoryId = 1, OwnerId = 2},
                new Tool { Id = 46, Name = "Chisel Set", Description = "Set of 4 wood chisels", Condition = ToolCondition.Good, ReplacementValue = 24.50m, ImageUrl = "/images/tools/chisel-set.jpg", DateAdded = ReferenceDate, CategoryId = 2, OwnerId = 3},
                new Tool { Id = 47, Name = "Retractable Measuring Wheel", Description = "30m measuring wheel", Condition = ToolCondition.New, ReplacementValue = 35.99m, ImageUrl = "/images/tools/measuring-wheel.jpg", DateAdded = ReferenceDate, CategoryId = 3, OwnerId = 4},
                new Tool { Id = 48, Name = "Sheet Metal Shears", Description = "Sheet metal cutting shears", Condition = ToolCondition.Fair, ReplacementValue = 33.00m, ImageUrl = "/images/tools/sheet-metal-shears.jpg", DateAdded = ReferenceDate, CategoryId = 2, OwnerId = 5},
                new Tool { Id = 49, Name = "Angle Finder", Description = "Digital angle finder", Condition = ToolCondition.New, ReplacementValue = 38.50m, ImageUrl = "/images/tools/angle-finder.jpg", DateAdded = ReferenceDate, CategoryId = 3, OwnerId = 6},
                new Tool { Id = 50, Name = "Rotary Tool Kit", Description = "Complete rotary tool kit with accessories", Condition = ToolCondition.Good, ReplacementValue = 89.99m, ImageUrl = "/images/tools/rotary-tool-kit.jpg", DateAdded = ReferenceDate, CategoryId = 1, OwnerId = 7}
            };
        }
    }
}
