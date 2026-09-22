using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CommuniLend.Data.Migrations
{
    /// <inheritdoc />
    public partial class TablesCreationWithDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    JoinDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Condition = table.Column<int>(type: "int", nullable: false),
                    ReplacementValue = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    DateAdded = table.Column<DateOnly>(type: "date", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tools_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tools_Members_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToolId = table.Column<int>(type: "int", nullable: false),
                    BorrowerId = table.Column<int>(type: "int", nullable: false),
                    LoanDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReturnDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loans_Members_BorrowerId",
                        column: x => x.BorrowerId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Loans_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Electric and cordless power tools for drilling, cutting, sanding, and more", "Power Tools" },
                    { 2, "Manual hand tools including wrenches, hammers, screwdrivers, pliers, and socket sets", "Hand Tools" },
                    { 3, "Tools for measuring, leveling, detecting studs, and finishing work", "Measuring & Finishing Tools" }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Address", "Email", "FullName", "IsActive", "JoinDate", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "123 Main Street, Springfield, IL 62701", "john.smith@email.com", "John Smith", true, new DateOnly(2026, 6, 24), "(555) 123-4567" },
                    { 2, "456 Oak Avenue, Springfield, IL 62702", "sarah.johnson@email.com", "Sarah Johnson", true, new DateOnly(2026, 7, 24), "(555) 234-5678" },
                    { 3, "789 Pine Road, Springfield, IL 62703", "michael.chen@email.com", "Michael Chen", true, new DateOnly(2026, 8, 8), "(555) 345-6789" },
                    { 4, "321 Elm Street, Springfield, IL 62704", "emily.rodriguez@email.com", "Emily Rodriguez", true, new DateOnly(2026, 8, 23), "(555) 456-7890" },
                    { 5, "654 Maple Drive, Springfield, IL 62705", "david.williams@email.com", "David Williams", true, new DateOnly(2026, 9, 2), "(555) 567-8901" },
                    { 6, "987 Cedar Lane, Springfield, IL 62706", "jessica.martinez@email.com", "Jessica Martinez", true, new DateOnly(2026, 9, 7), "(555) 678-9012" },
                    { 7, "147 Birch Court, Springfield, IL 62707", "robert.taylor@email.com", "Robert Taylor", true, new DateOnly(2026, 9, 12), "(555) 789-0123" }
                });

            migrationBuilder.InsertData(
                table: "Tools",
                columns: new[] { "Id", "CategoryId", "Condition", "DateAdded", "Description", "ImageUrl", "IsAvailable", "Name", "OwnerId", "ReplacementValue" },
                values: new object[,]
                {
                    { 1, 1, 0, new DateOnly(2026, 8, 23), "18V lithium-ion cordless drill with carry case", "/images/tools/cordless-drill.jpg", true, "Cordless Drill", 1, 89.99m },
                    { 2, 1, 1, new DateOnly(2026, 8, 28), "7.25\" circular saw for wood cutting", "/images/tools/circular-saw.jpg", true, "Circular Saw", 1, 65.50m },
                    { 3, 2, 1, new DateOnly(2026, 9, 2), "Set of 3 adjustable wrenches in various sizes", "/images/tools/wrench-set.jpg", true, "Adjustable Wrench Set", 2, 25.99m },
                    { 4, 3, 2, new DateOnly(2026, 9, 7), "24\" aluminum spirit level", "/images/tools/level.jpg", true, "Level", 3, 18.75m },
                    { 5, 2, 1, new DateOnly(2026, 9, 12), "Set of 5 hammers with different head weights", "/images/tools/hammer-set.jpg", true, "Hammer Set", 4, 35.00m },
                    { 6, 1, 0, new DateOnly(2026, 9, 14), "Orbital jigsaw for curved cuts", "/images/tools/jigsaw.jpg", false, "Jigsaw", 5, 120.00m },
                    { 7, 3, 1, new DateOnly(2026, 9, 15), "25ft retractable tape measure", "/images/tools/tape-measure.jpg", true, "Tape Measure", 6, 12.50m },
                    { 8, 2, 2, new DateOnly(2026, 9, 17), "Complete socket set with ratchets", "/images/tools/socket-set.jpg", true, "Socket Set", 7, 45.99m },
                    { 9, 2, 1, new DateOnly(2026, 9, 19), "Set of 4 different pliers", "/images/tools/pliers-set.jpg", true, "Pliers Set", 1, 28.00m },
                    { 10, 1, 0, new DateOnly(2026, 9, 20), "10\" compound miter saw", "/images/tools/miter-saw.jpg", true, "Miter Saw", 2, 199.99m },
                    { 11, 2, 1, new DateOnly(2026, 9, 21), "Set of 12 screwdrivers (Phillips and flathead)", "/images/tools/screwdriver-set.jpg", true, "Screwdriver Set", 3, 19.99m },
                    { 12, 1, 2, new DateOnly(2026, 9, 22), "12V compact power drill", "/images/tools/power-drill.jpg", false, "Power Drill", 4, 59.99m },
                    { 13, 3, 0, new DateOnly(2026, 9, 22), "Digital stud finder with deep scan", "/images/tools/stud-finder.jpg", true, "Stud Finder", 5, 42.50m },
                    { 14, 3, 1, new DateOnly(2026, 9, 22), "LED flashlight with adjustable beam", "/images/tools/flashlight.jpg", true, "Flashlight", 6, 15.00m },
                    { 15, 3, 0, new DateOnly(2026, 9, 22), "Digital laser distance measurer", "/images/tools/laser-measure.jpg", true, "Laser Measure", 7, 79.99m },
                    { 16, 1, 1, new DateOnly(2026, 9, 22), "Cordless reciprocating saw for demolition", "/images/tools/reciprocating-saw.jpg", true, "Reciprocating Saw", 1, 145.00m },
                    { 17, 1, 1, new DateOnly(2026, 9, 22), "5\" random orbital sander", "/images/tools/orbital-sander.jpg", true, "Orbital Sander", 2, 99.99m },
                    { 18, 2, 3, new DateOnly(2026, 9, 22), "Set of 4 nail sets", "/images/tools/nail-set.jpg", true, "Nail Set", 3, 8.99m },
                    { 19, 2, 1, new DateOnly(2026, 9, 22), "Heavy-duty caulking gun", "/images/tools/caulking-gun.jpg", true, "Caulking Gun", 4, 22.00m },
                    { 20, 2, 2, new DateOnly(2026, 9, 22), "Retractable utility knife with replacement blades", "/images/tools/utility-knife.jpg", true, "Utility Knife", 5, 10.50m },
                    { 21, 1, 0, new DateOnly(2026, 9, 22), "20V impact driver", "/images/tools/impact-driver.jpg", true, "Impact Driver", 6, 99.99m },
                    { 22, 1, 0, new DateOnly(2026, 9, 22), "18-gauge brad nailer", "/images/tools/brad-nailer.jpg", false, "Brad Nailer", 7, 75.00m },
                    { 23, 3, 1, new DateOnly(2026, 9, 22), "Stud finder with level", "/images/tools/stud-kit.jpg", true, "Stud Finder Kit", 1, 35.99m },
                    { 24, 3, 1, new DateOnly(2026, 9, 22), "9\" torpedo level", "/images/tools/torpedo-level.jpg", true, "Torpedo Level", 2, 16.50m },
                    { 25, 2, 1, new DateOnly(2026, 9, 22), "24-piece ratchet socket set", "/images/tools/ratchet-set.jpg", true, "Ratchet Set", 3, 55.00m },
                    { 26, 2, 2, new DateOnly(2026, 9, 22), "4\" bench vise with swivel base", "/images/tools/bench-vise.jpg", true, "Bench Vise", 4, 65.00m },
                    { 27, 1, 1, new DateOnly(2026, 9, 22), "4.5\" angle grinder", "/images/tools/angle-grinder.jpg", true, "Angle Grinder", 5, 85.99m },
                    { 28, 1, 2, new DateOnly(2026, 9, 22), "Pneumatic finishing nailer", "/images/tools/pneumatic-nailer.jpg", true, "Pneumatic Nailer", 6, 60.00m },
                    { 29, 3, 0, new DateOnly(2026, 9, 22), "24\" magnetic level", "/images/tools/magnetic-level.jpg", true, "Magnetic Level", 7, 21.99m },
                    { 30, 1, 3, new DateOnly(2026, 9, 22), "Portable circular power saw", "/images/tools/power-saw.jpg", true, "Power Saw", 1, 55.00m },
                    { 31, 2, 1, new DateOnly(2026, 9, 22), "Complete hex key set", "/images/tools/allen-key-set.jpg", true, "Allen Key Set", 2, 14.99m },
                    { 32, 2, 2, new DateOnly(2026, 9, 22), "18\" adjustable pipe wrench", "/images/tools/pipe-wrench.jpg", true, "Pipe Wrench", 3, 32.50m },
                    { 33, 2, 1, new DateOnly(2026, 9, 22), "12\" hacksaw with blade", "/images/tools/hacksaw.jpg", true, "Hacksaw", 4, 13.00m },
                    { 34, 2, 1, new DateOnly(2026, 9, 22), "30-piece drill bit set", "/images/tools/drill-bit-set.jpg", true, "Drill Bit Set", 5, 18.50m },
                    { 35, 3, 0, new DateOnly(2026, 9, 22), "12\" combination square", "/images/tools/combination-square.jpg", true, "Combination Square", 6, 19.99m },
                    { 36, 3, 1, new DateOnly(2026, 9, 22), "Set of 3 LED flashlights", "/images/tools/flashlight-set.jpg", true, "Flashlight Set", 7, 35.00m },
                    { 37, 2, 1, new DateOnly(2026, 9, 22), "15\" flat pry bar", "/images/tools/pry-bar.jpg", true, "Pry Bar", 1, 14.50m },
                    { 38, 1, 0, new DateOnly(2026, 9, 22), "Cordless oscillating tool with accessories", "/images/tools/multi-tool.jpg", true, "Oscillating Multi-Tool", 2, 129.99m },
                    { 39, 2, 2, new DateOnly(2026, 9, 22), "Set of 2 bar clamps (12\" and 24\")", "/images/tools/bar-clamp-set.jpg", true, "Bar Clamp Set", 3, 42.00m },
                    { 40, 1, 1, new DateOnly(2026, 9, 22), "Electric nail gun", "/images/tools/nail-gun.jpg", false, "Nail Gun", 4, 79.99m },
                    { 41, 3, 3, new DateOnly(2026, 9, 22), "24\" carpenter's speed square", "/images/tools/carpenters-square.jpg", true, "Carpenter's Square", 5, 11.00m },
                    { 42, 1, 1, new DateOnly(2026, 9, 22), "Shop dust extractor", "/images/tools/dust-extractor.jpg", true, "Dust Extractor", 6, 125.00m },
                    { 43, 3, 0, new DateOnly(2026, 9, 22), "6\" adjustable square", "/images/tools/adjustable-square.jpg", true, "Adjustable Square", 7, 17.50m },
                    { 44, 2, 1, new DateOnly(2026, 9, 22), "Heavy-duty cable protector", "/images/tools/cable-protector.jpg", true, "Cable Protector", 1, 28.00m },
                    { 45, 1, 2, new DateOnly(2026, 9, 22), "6\" bench grinder", "/images/tools/bench-grinder.jpg", true, "Bench Grinder", 2, 95.00m },
                    { 46, 2, 1, new DateOnly(2026, 9, 22), "Set of 4 wood chisels", "/images/tools/chisel-set.jpg", true, "Chisel Set", 3, 24.50m },
                    { 47, 3, 0, new DateOnly(2026, 9, 22), "30m measuring wheel", "/images/tools/measuring-wheel.jpg", true, "Retractable Measuring Wheel", 4, 35.99m },
                    { 48, 2, 2, new DateOnly(2026, 9, 22), "Sheet metal cutting shears", "/images/tools/sheet-metal-shears.jpg", true, "Sheet Metal Shears", 5, 33.00m },
                    { 49, 3, 0, new DateOnly(2026, 9, 22), "Digital angle finder", "/images/tools/angle-finder.jpg", true, "Angle Finder", 6, 38.50m },
                    { 50, 1, 1, new DateOnly(2026, 9, 22), "Complete rotary tool kit with accessories", "/images/tools/rotary-tool-kit.jpg", true, "Rotary Tool Kit", 7, 89.99m }
                });

            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "Id", "BorrowerId", "DueDate", "LoanDate", "Notes", "ReturnDate", "ToolId" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(2026, 9, 29), new DateOnly(2026, 9, 8), "Borrowed for home renovation project", null, 3 },
                    { 2, 2, new DateOnly(2026, 10, 3), new DateOnly(2026, 9, 12), "Needed for measuring deck", null, 7 },
                    { 3, 3, new DateOnly(2026, 10, 5), new DateOnly(2026, 9, 14), "Cabinet installation", null, 11 },
                    { 4, 4, new DateOnly(2026, 10, 8), new DateOnly(2026, 9, 17), "Basement repair work", null, 14 },
                    { 5, 5, new DateOnly(2026, 10, 10), new DateOnly(2026, 9, 19), "Caulking bathroom tiles", null, 19 },
                    { 6, 6, new DateOnly(2026, 10, 11), new DateOnly(2026, 9, 20), "Engine repair project", null, 8 },
                    { 7, 7, new DateOnly(2026, 10, 12), new DateOnly(2026, 9, 21), "Measuring for new flooring", null, 15 },
                    { 8, 2, new DateOnly(2026, 8, 22), new DateOnly(2026, 8, 8), "Shelf installation", new DateOnly(2026, 8, 23), 4 },
                    { 9, 3, new DateOnly(2026, 8, 27), new DateOnly(2026, 8, 13), "Picture frame hanging", new DateOnly(2026, 8, 28), 9 },
                    { 10, 4, new DateOnly(2026, 9, 1), new DateOnly(2026, 8, 18), "Fence repair", new DateOnly(2026, 9, 2), 5 },
                    { 11, 5, new DateOnly(2026, 9, 6), new DateOnly(2026, 8, 23), "Wall stud detection", new DateOnly(2026, 9, 7), 13 },
                    { 12, 6, new DateOnly(2026, 9, 11), new DateOnly(2026, 8, 28), "Deck board cutting", new DateOnly(2026, 9, 12), 2 },
                    { 13, 7, new DateOnly(2026, 9, 16), new DateOnly(2026, 9, 2), "Molding and trim work", new DateOnly(2026, 9, 17), 10 },
                    { 14, 1, new DateOnly(2026, 9, 1), new DateOnly(2026, 8, 18), "Tree removal branch cutting", new DateOnly(2026, 9, 2), 16 },
                    { 15, 2, new DateOnly(2026, 9, 8), new DateOnly(2026, 8, 25), "Floor sanding", new DateOnly(2026, 9, 9), 17 },
                    { 16, 3, new DateOnly(2026, 9, 14), new DateOnly(2026, 8, 31), "Finishing hardwood", new DateOnly(2026, 9, 15), 18 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BorrowerId",
                table: "Loans",
                column: "BorrowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_ToolId",
                table: "Loans",
                column: "ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_Tools_CategoryId",
                table: "Tools",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tools_OwnerId",
                table: "Tools",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Tools");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
