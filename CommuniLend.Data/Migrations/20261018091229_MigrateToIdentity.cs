using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CommuniLend.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigrateToIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    JoinDate = table.Column<DateOnly>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    DateAdded = table.Column<DateOnly>(type: "date", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tools_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tools_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClosedLoans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToolId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    BorrowerId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    LoanDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReturnDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosedLoans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClosedLoans_AspNetUsers_BorrowerId",
                        column: x => x.BorrowerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClosedLoans_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClosedLoans_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpenLoans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToolId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    BorrowerId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    LoanDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenLoans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenLoans_AspNetUsers_BorrowerId",
                        column: x => x.BorrowerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpenLoans_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpenLoans_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "IsActive", "JoinDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "234a29ff-3432-5780-a6e8-3acd25056d4c", 0, "987 Cedar Lane, Springfield, IL 62706", "364894f5-ab00-5eb4-a4a7-40ac99d7f27f", "ApplicationUser", "jessica.martinez@email.com", true, "Jessica Martinez", true, new DateOnly(2026, 9, 13), false, null, "JESSICA.MARTINEZ@EMAIL.COM", "JESSICA.MARTINEZ@EMAIL.COM", "AQAAAAIAAYagAAAAEF86nB57LUhgocPl9wkStNa2Svp+fHGNi/3hE6or/QJMzteRck+ThqQsgLDjNlHc8A==", "(555) 678-9012", true, "364894f5-ab00-5eb4-a4a7-40ac99d7f27f", false, "jessica.martinez@email.com" },
                    { "5085596c-7975-580a-aa8a-68ad83d435bb", 0, "456 Oak Avenue, Springfield, IL 62702", "364894f5-ab00-5eb4-a4a7-40ac99d7f27f", "ApplicationUser", "sarah.johnson@email.com", true, "Sarah Johnson", true, new DateOnly(2026, 7, 30), false, null, "SARAH.JOHNSON@EMAIL.COM", "SARAH.JOHNSON@EMAIL.COM", "AQAAAAIAAYagAAAAEF86nB57LUhgocPl9wkStNa2Svp+fHGNi/3hE6or/QJMzteRck+ThqQsgLDjNlHc8A==", "(555) 234-5678", true, "364894f5-ab00-5eb4-a4a7-40ac99d7f27f", false, "sarah.johnson@email.com" },
                    { "5babe2ff-dff9-5359-8b04-f77b0028ca98", 0, "147 Birch Court, Springfield, IL 62707", "364894f5-ab00-5eb4-a4a7-40ac99d7f27f", "ApplicationUser", "robert.taylor@email.com", true, "Robert Taylor", true, new DateOnly(2026, 9, 18), false, null, "ROBERT.TAYLOR@EMAIL.COM", "ROBERT.TAYLOR@EMAIL.COM", "AQAAAAIAAYagAAAAEF86nB57LUhgocPl9wkStNa2Svp+fHGNi/3hE6or/QJMzteRck+ThqQsgLDjNlHc8A==", "(555) 789-0123", true, "364894f5-ab00-5eb4-a4a7-40ac99d7f27f", false, "robert.taylor@email.com" },
                    { "93295a13-2d4b-5732-9cba-916402a8edf3", 0, "321 Elm Street, Springfield, IL 62704", "364894f5-ab00-5eb4-a4a7-40ac99d7f27f", "ApplicationUser", "emily.rodriguez@email.com", true, "Emily Rodriguez", true, new DateOnly(2026, 8, 29), false, null, "EMILY.RODRIGUEZ@EMAIL.COM", "EMILY.RODRIGUEZ@EMAIL.COM", "AQAAAAIAAYagAAAAEF86nB57LUhgocPl9wkStNa2Svp+fHGNi/3hE6or/QJMzteRck+ThqQsgLDjNlHc8A==", "(555) 456-7890", true, "364894f5-ab00-5eb4-a4a7-40ac99d7f27f", false, "emily.rodriguez@email.com" },
                    { "b9af376a-21a5-540b-89d2-8c131633400d", 0, "789 Pine Road, Springfield, IL 62703", "364894f5-ab00-5eb4-a4a7-40ac99d7f27f", "ApplicationUser", "michael.chen@email.com", true, "Michael Chen", true, new DateOnly(2026, 8, 14), false, null, "MICHAEL.CHEN@EMAIL.COM", "MICHAEL.CHEN@EMAIL.COM", "AQAAAAIAAYagAAAAEF86nB57LUhgocPl9wkStNa2Svp+fHGNi/3hE6or/QJMzteRck+ThqQsgLDjNlHc8A==", "(555) 345-6789", true, "364894f5-ab00-5eb4-a4a7-40ac99d7f27f", false, "michael.chen@email.com" },
                    { "f04c78b2-6180-5b82-bdb8-0eedb97675a7", 0, "654 Maple Drive, Springfield, IL 62705", "364894f5-ab00-5eb4-a4a7-40ac99d7f27f", "ApplicationUser", "david.williams@email.com", true, "David Williams", true, new DateOnly(2026, 9, 8), false, null, "DAVID.WILLIAMS@EMAIL.COM", "DAVID.WILLIAMS@EMAIL.COM", "AQAAAAIAAYagAAAAEF86nB57LUhgocPl9wkStNa2Svp+fHGNi/3hE6or/QJMzteRck+ThqQsgLDjNlHc8A==", "(555) 567-8901", true, "364894f5-ab00-5eb4-a4a7-40ac99d7f27f", false, "david.williams@email.com" },
                    { "fec24483-730b-58d9-9383-2c44fcf490e8", 0, "123 Main Street, Springfield, IL 62701", "364894f5-ab00-5eb4-a4a7-40ac99d7f27f", "ApplicationUser", "john.smith@email.com", true, "John Smith", true, new DateOnly(2026, 6, 30), false, null, "JOHN.SMITH@EMAIL.COM", "JOHN.SMITH@EMAIL.COM", "AQAAAAIAAYagAAAAEF86nB57LUhgocPl9wkStNa2Svp+fHGNi/3hE6or/QJMzteRck+ThqQsgLDjNlHc8A==", "(555) 123-4567", true, "364894f5-ab00-5eb4-a4a7-40ac99d7f27f", false, "john.smith@email.com" }
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
                table: "Tools",
                columns: new[] { "Id", "CategoryId", "Condition", "DateAdded", "Description", "ImageUrl", "Name", "OwnerId", "ReplacementValue" },
                values: new object[,]
                {
                    { 1, 1, 0, new DateOnly(2026, 8, 29), "18V lithium-ion cordless drill with carry case", "/images/tools/cordless-drill.jpg", "Cordless Drill", "fec24483-730b-58d9-9383-2c44fcf490e8", 89.99m },
                    { 2, 1, 1, new DateOnly(2026, 9, 3), "7.25\" circular saw for wood cutting", "/images/tools/circular-saw.jpg", "Circular Saw", "fec24483-730b-58d9-9383-2c44fcf490e8", 65.50m },
                    { 3, 2, 1, new DateOnly(2026, 9, 8), "Set of 3 adjustable wrenches in various sizes", "/images/tools/wrench-set.jpg", "Adjustable Wrench Set", "5085596c-7975-580a-aa8a-68ad83d435bb", 25.99m },
                    { 4, 3, 2, new DateOnly(2026, 8, 16), "24\" aluminum spirit level", "/images/tools/level.jpg", "Level", "b9af376a-21a5-540b-89d2-8c131633400d", 18.75m },
                    { 5, 2, 1, new DateOnly(2026, 8, 30), "Set of 5 hammers with different head weights", "/images/tools/hammer-set.jpg", "Hammer Set", "93295a13-2d4b-5732-9cba-916402a8edf3", 35.00m },
                    { 6, 1, 0, new DateOnly(2026, 9, 20), "Orbital jigsaw for curved cuts", "/images/tools/jigsaw.jpg", "Jigsaw", "f04c78b2-6180-5b82-bdb8-0eedb97675a7", 120.00m },
                    { 7, 3, 1, new DateOnly(2026, 9, 14), "25ft retractable tape measure", "/images/tools/tape-measure.jpg", "Tape Measure", "234a29ff-3432-5780-a6e8-3acd25056d4c", 12.50m },
                    { 8, 2, 2, new DateOnly(2026, 9, 23), "Complete socket set with ratchets", "/images/tools/socket-set.jpg", "Socket Set", "5babe2ff-dff9-5359-8b04-f77b0028ca98", 45.99m },
                    { 9, 2, 1, new DateOnly(2026, 7, 30), "Set of 4 different pliers", "/images/tools/pliers-set.jpg", "Pliers Set", "fec24483-730b-58d9-9383-2c44fcf490e8", 28.00m },
                    { 10, 1, 0, new DateOnly(2026, 8, 9), "10\" compound miter saw", "/images/tools/miter-saw.jpg", "Miter Saw", "5085596c-7975-580a-aa8a-68ad83d435bb", 199.99m },
                    { 11, 2, 1, new DateOnly(2026, 9, 16), "Set of 12 screwdrivers (Phillips and flathead)", "/images/tools/screwdriver-set.jpg", "Screwdriver Set", "b9af376a-21a5-540b-89d2-8c131633400d", 19.99m },
                    { 12, 1, 2, new DateOnly(2026, 9, 28), "12V compact power drill", "/images/tools/power-drill.jpg", "Power Drill", "93295a13-2d4b-5732-9cba-916402a8edf3", 59.99m },
                    { 13, 3, 0, new DateOnly(2026, 9, 9), "Digital stud finder with deep scan", "/images/tools/stud-finder.jpg", "Stud Finder", "f04c78b2-6180-5b82-bdb8-0eedb97675a7", 42.50m },
                    { 14, 3, 1, new DateOnly(2026, 9, 18), "LED flashlight with adjustable beam", "/images/tools/flashlight.jpg", "Flashlight", "234a29ff-3432-5780-a6e8-3acd25056d4c", 15.00m },
                    { 15, 3, 0, new DateOnly(2026, 9, 22), "Digital laser distance measurer", "/images/tools/laser-measure.jpg", "Laser Measure", "5babe2ff-dff9-5359-8b04-f77b0028ca98", 79.99m },
                    { 16, 1, 1, new DateOnly(2026, 8, 9), "Cordless reciprocating saw for demolition", "/images/tools/reciprocating-saw.jpg", "Reciprocating Saw", "fec24483-730b-58d9-9383-2c44fcf490e8", 145.00m },
                    { 17, 1, 1, new DateOnly(2026, 8, 19), "5\" random orbital sander", "/images/tools/orbital-sander.jpg", "Orbital Sander", "5085596c-7975-580a-aa8a-68ad83d435bb", 99.99m },
                    { 18, 2, 3, new DateOnly(2026, 8, 24), "Set of 4 nail sets", "/images/tools/nail-set.jpg", "Nail Set", "b9af376a-21a5-540b-89d2-8c131633400d", 8.99m },
                    { 19, 2, 1, new DateOnly(2026, 9, 20), "Heavy-duty caulking gun", "/images/tools/caulking-gun.jpg", "Caulking Gun", "93295a13-2d4b-5732-9cba-916402a8edf3", 22.00m },
                    { 20, 2, 2, new DateOnly(2026, 9, 28), "Retractable utility knife with replacement blades", "/images/tools/utility-knife.jpg", "Utility Knife", "f04c78b2-6180-5b82-bdb8-0eedb97675a7", 10.50m },
                    { 21, 1, 0, new DateOnly(2026, 9, 28), "20V impact driver", "/images/tools/impact-driver.jpg", "Impact Driver", "234a29ff-3432-5780-a6e8-3acd25056d4c", 99.99m },
                    { 22, 1, 0, new DateOnly(2026, 9, 28), "18-gauge brad nailer", "/images/tools/brad-nailer.jpg", "Brad Nailer", "5babe2ff-dff9-5359-8b04-f77b0028ca98", 75.00m },
                    { 23, 3, 1, new DateOnly(2026, 9, 28), "Stud finder with level", "/images/tools/stud-kit.jpg", "Stud Finder Kit", "fec24483-730b-58d9-9383-2c44fcf490e8", 35.99m },
                    { 24, 3, 1, new DateOnly(2026, 9, 28), "9\" torpedo level", "/images/tools/torpedo-level.jpg", "Torpedo Level", "5085596c-7975-580a-aa8a-68ad83d435bb", 16.50m },
                    { 25, 2, 1, new DateOnly(2026, 9, 28), "24-piece ratchet socket set", "/images/tools/ratchet-set.jpg", "Ratchet Set", "b9af376a-21a5-540b-89d2-8c131633400d", 55.00m },
                    { 26, 2, 2, new DateOnly(2026, 9, 28), "4\" bench vise with swivel base", "/images/tools/bench-vise.jpg", "Bench Vise", "93295a13-2d4b-5732-9cba-916402a8edf3", 65.00m },
                    { 27, 1, 1, new DateOnly(2026, 9, 28), "4.5\" angle grinder", "/images/tools/angle-grinder.jpg", "Angle Grinder", "f04c78b2-6180-5b82-bdb8-0eedb97675a7", 85.99m },
                    { 28, 1, 2, new DateOnly(2026, 9, 28), "Pneumatic finishing nailer", "/images/tools/pneumatic-nailer.jpg", "Pneumatic Nailer", "234a29ff-3432-5780-a6e8-3acd25056d4c", 60.00m },
                    { 29, 3, 0, new DateOnly(2026, 9, 28), "24\" magnetic level", "/images/tools/magnetic-level.jpg", "Magnetic Level", "5babe2ff-dff9-5359-8b04-f77b0028ca98", 21.99m },
                    { 30, 1, 3, new DateOnly(2026, 9, 28), "Portable circular power saw", "/images/tools/power-saw.jpg", "Power Saw", "fec24483-730b-58d9-9383-2c44fcf490e8", 55.00m },
                    { 31, 2, 1, new DateOnly(2026, 9, 28), "Complete hex key set", "/images/tools/allen-key-set.jpg", "Allen Key Set", "5085596c-7975-580a-aa8a-68ad83d435bb", 14.99m },
                    { 32, 2, 2, new DateOnly(2026, 9, 28), "18\" adjustable pipe wrench", "/images/tools/pipe-wrench.jpg", "Pipe Wrench", "b9af376a-21a5-540b-89d2-8c131633400d", 32.50m },
                    { 33, 2, 1, new DateOnly(2026, 9, 28), "12\" hacksaw with blade", "/images/tools/hacksaw.jpg", "Hacksaw", "93295a13-2d4b-5732-9cba-916402a8edf3", 13.00m },
                    { 34, 2, 1, new DateOnly(2026, 9, 28), "30-piece drill bit set", "/images/tools/drill-bit-set.jpg", "Drill Bit Set", "f04c78b2-6180-5b82-bdb8-0eedb97675a7", 18.50m },
                    { 35, 3, 0, new DateOnly(2026, 9, 28), "12\" combination square", "/images/tools/combination-square.jpg", "Combination Square", "234a29ff-3432-5780-a6e8-3acd25056d4c", 19.99m },
                    { 36, 3, 1, new DateOnly(2026, 9, 28), "Set of 3 LED flashlights", "/images/tools/flashlight-set.jpg", "Flashlight Set", "5babe2ff-dff9-5359-8b04-f77b0028ca98", 35.00m },
                    { 37, 2, 1, new DateOnly(2026, 9, 28), "15\" flat pry bar", "/images/tools/pry-bar.jpg", "Pry Bar", "fec24483-730b-58d9-9383-2c44fcf490e8", 14.50m },
                    { 38, 1, 0, new DateOnly(2026, 9, 28), "Cordless oscillating tool with accessories", "/images/tools/multi-tool.jpg", "Oscillating Multi-Tool", "5085596c-7975-580a-aa8a-68ad83d435bb", 129.99m },
                    { 39, 2, 2, new DateOnly(2026, 9, 28), "Set of 2 bar clamps (12\" and 24\")", "/images/tools/bar-clamp-set.jpg", "Bar Clamp Set", "b9af376a-21a5-540b-89d2-8c131633400d", 42.00m },
                    { 40, 1, 1, new DateOnly(2026, 9, 28), "Electric nail gun", "/images/tools/nail-gun.jpg", "Nail Gun", "93295a13-2d4b-5732-9cba-916402a8edf3", 79.99m },
                    { 41, 3, 3, new DateOnly(2026, 9, 28), "24\" carpenter's speed square", "/images/tools/carpenters-square.jpg", "Carpenter's Square", "f04c78b2-6180-5b82-bdb8-0eedb97675a7", 11.00m },
                    { 42, 1, 1, new DateOnly(2026, 9, 28), "Shop dust extractor", "/images/tools/dust-extractor.jpg", "Dust Extractor", "234a29ff-3432-5780-a6e8-3acd25056d4c", 125.00m },
                    { 43, 3, 0, new DateOnly(2026, 9, 28), "6\" adjustable square", "/images/tools/adjustable-square.jpg", "Adjustable Square", "5babe2ff-dff9-5359-8b04-f77b0028ca98", 17.50m },
                    { 44, 2, 1, new DateOnly(2026, 9, 28), "Heavy-duty cable protector", "/images/tools/cable-protector.jpg", "Cable Protector", "fec24483-730b-58d9-9383-2c44fcf490e8", 28.00m },
                    { 45, 1, 2, new DateOnly(2026, 9, 28), "6\" bench grinder", "/images/tools/bench-grinder.jpg", "Bench Grinder", "5085596c-7975-580a-aa8a-68ad83d435bb", 95.00m },
                    { 46, 2, 1, new DateOnly(2026, 9, 28), "Set of 4 wood chisels", "/images/tools/chisel-set.jpg", "Chisel Set", "b9af376a-21a5-540b-89d2-8c131633400d", 24.50m },
                    { 47, 3, 0, new DateOnly(2026, 9, 28), "30m measuring wheel", "/images/tools/measuring-wheel.jpg", "Retractable Measuring Wheel", "93295a13-2d4b-5732-9cba-916402a8edf3", 35.99m },
                    { 48, 2, 2, new DateOnly(2026, 9, 28), "Sheet metal cutting shears", "/images/tools/sheet-metal-shears.jpg", "Sheet Metal Shears", "f04c78b2-6180-5b82-bdb8-0eedb97675a7", 33.00m },
                    { 49, 3, 0, new DateOnly(2026, 9, 28), "Digital angle finder", "/images/tools/angle-finder.jpg", "Angle Finder", "234a29ff-3432-5780-a6e8-3acd25056d4c", 38.50m },
                    { 50, 1, 1, new DateOnly(2026, 9, 28), "Complete rotary tool kit with accessories", "/images/tools/rotary-tool-kit.jpg", "Rotary Tool Kit", "5babe2ff-dff9-5359-8b04-f77b0028ca98", 89.99m }
                });

            migrationBuilder.InsertData(
                table: "ClosedLoans",
                columns: new[] { "Id", "BorrowerId", "DueDate", "LoanDate", "Notes", "OwnerId", "ReturnDate", "ToolId" },
                values: new object[,]
                {
                    { 8, "5085596c-7975-580a-aa8a-68ad83d435bb", new DateOnly(2026, 9, 2), new DateOnly(2026, 8, 19), "Shelf installation", "b9af376a-21a5-540b-89d2-8c131633400d", new DateOnly(2026, 9, 3), 4 },
                    { 9, "b9af376a-21a5-540b-89d2-8c131633400d", new DateOnly(2026, 9, 2), new DateOnly(2026, 8, 19), "Picture frame hanging", "fec24483-730b-58d9-9383-2c44fcf490e8", new DateOnly(2026, 9, 3), 9 },
                    { 10, "fec24483-730b-58d9-9383-2c44fcf490e8", new DateOnly(2026, 9, 14), new DateOnly(2026, 8, 31), "Fence repair", "93295a13-2d4b-5732-9cba-916402a8edf3", new DateOnly(2026, 9, 15), 5 },
                    { 11, "93295a13-2d4b-5732-9cba-916402a8edf3", new DateOnly(2026, 9, 24), new DateOnly(2026, 9, 10), "Wall stud detection", "f04c78b2-6180-5b82-bdb8-0eedb97675a7", new DateOnly(2026, 9, 25), 13 },
                    { 12, "b9af376a-21a5-540b-89d2-8c131633400d", new DateOnly(2026, 9, 18), new DateOnly(2026, 9, 4), "Deck board cutting", "fec24483-730b-58d9-9383-2c44fcf490e8", new DateOnly(2026, 9, 19), 2 },
                    { 13, "f04c78b2-6180-5b82-bdb8-0eedb97675a7", new DateOnly(2026, 9, 24), new DateOnly(2026, 9, 10), "Molding and trim work", "5085596c-7975-580a-aa8a-68ad83d435bb", new DateOnly(2026, 9, 25), 10 },
                    { 14, "5085596c-7975-580a-aa8a-68ad83d435bb", new DateOnly(2026, 9, 7), new DateOnly(2026, 8, 24), "Tree removal branch cutting", "fec24483-730b-58d9-9383-2c44fcf490e8", new DateOnly(2026, 9, 8), 16 },
                    { 15, "fec24483-730b-58d9-9383-2c44fcf490e8", new DateOnly(2026, 9, 14), new DateOnly(2026, 8, 31), "Floor sanding", "5085596c-7975-580a-aa8a-68ad83d435bb", new DateOnly(2026, 9, 15), 17 },
                    { 16, "93295a13-2d4b-5732-9cba-916402a8edf3", new DateOnly(2026, 9, 20), new DateOnly(2026, 9, 6), "Finishing hardwood", "b9af376a-21a5-540b-89d2-8c131633400d", new DateOnly(2026, 9, 21), 18 }
                });

            migrationBuilder.InsertData(
                table: "OpenLoans",
                columns: new[] { "Id", "BorrowerId", "DueDate", "LoanDate", "Notes", "OwnerId", "ToolId" },
                values: new object[,]
                {
                    { 1, "fec24483-730b-58d9-9383-2c44fcf490e8", new DateOnly(2026, 10, 5), new DateOnly(2026, 9, 14), "Borrowed for home renovation project", "5085596c-7975-580a-aa8a-68ad83d435bb", 3 },
                    { 2, "5085596c-7975-580a-aa8a-68ad83d435bb", new DateOnly(2026, 10, 9), new DateOnly(2026, 9, 18), "Needed for measuring deck", "234a29ff-3432-5780-a6e8-3acd25056d4c", 7 },
                    { 3, "5babe2ff-dff9-5359-8b04-f77b0028ca98", new DateOnly(2026, 10, 11), new DateOnly(2026, 9, 20), "Cabinet installation", "b9af376a-21a5-540b-89d2-8c131633400d", 11 },
                    { 4, "93295a13-2d4b-5732-9cba-916402a8edf3", new DateOnly(2026, 10, 14), new DateOnly(2026, 9, 23), "Basement repair work", "234a29ff-3432-5780-a6e8-3acd25056d4c", 14 },
                    { 5, "f04c78b2-6180-5b82-bdb8-0eedb97675a7", new DateOnly(2026, 10, 16), new DateOnly(2026, 9, 25), "Caulking bathroom tiles", "93295a13-2d4b-5732-9cba-916402a8edf3", 19 },
                    { 6, "234a29ff-3432-5780-a6e8-3acd25056d4c", new DateOnly(2026, 10, 17), new DateOnly(2026, 9, 26), "Engine repair project", "5babe2ff-dff9-5359-8b04-f77b0028ca98", 8 },
                    { 7, "b9af376a-21a5-540b-89d2-8c131633400d", new DateOnly(2026, 10, 18), new DateOnly(2026, 9, 27), "Measuring for new flooring", "5babe2ff-dff9-5359-8b04-f77b0028ca98", 15 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClosedLoans_BorrowerId",
                table: "ClosedLoans",
                column: "BorrowerId");

            migrationBuilder.CreateIndex(
                name: "IX_ClosedLoans_OwnerId",
                table: "ClosedLoans",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ClosedLoans_ToolId",
                table: "ClosedLoans",
                column: "ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenLoans_BorrowerId",
                table: "OpenLoans",
                column: "BorrowerId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenLoans_OwnerId",
                table: "OpenLoans",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenLoans_ToolId",
                table: "OpenLoans",
                column: "ToolId",
                unique: true);

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ClosedLoans");

            migrationBuilder.DropTable(
                name: "OpenLoans");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Tools");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
