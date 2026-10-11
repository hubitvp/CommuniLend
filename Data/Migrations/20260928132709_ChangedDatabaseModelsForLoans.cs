using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CommuniLend.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedDatabaseModelsForLoans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Tools");

            migrationBuilder.CreateTable(
                name: "ClosedLoans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToolId = table.Column<int>(type: "int", nullable: false),
                    BorrowerId = table.Column<int>(type: "int", nullable: false),
                    LoanDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReturnDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosedLoans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClosedLoans_Members_BorrowerId",
                        column: x => x.BorrowerId,
                        principalTable: "Members",
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
                    BorrowerId = table.Column<int>(type: "int", nullable: false),
                    LoanDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenLoans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenLoans_Members_BorrowerId",
                        column: x => x.BorrowerId,
                        principalTable: "Members",
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
                table: "ClosedLoans",
                columns: new[] { "Id", "BorrowerId", "DueDate", "LoanDate", "Notes", "ReturnDate", "ToolId" },
                values: new object[,]
                {
                    { 8, 2, new DateOnly(2026, 9, 2), new DateOnly(2026, 8, 19), "Shelf installation", new DateOnly(2026, 9, 3), 4 },
                    { 9, 3, new DateOnly(2026, 9, 2), new DateOnly(2026, 8, 19), "Picture frame hanging", new DateOnly(2026, 9, 3), 9 },
                    { 10, 1, new DateOnly(2026, 9, 14), new DateOnly(2026, 8, 31), "Fence repair", new DateOnly(2026, 9, 15), 5 },
                    { 11, 4, new DateOnly(2026, 9, 24), new DateOnly(2026, 9, 10), "Wall stud detection", new DateOnly(2026, 9, 25), 13 },
                    { 12, 3, new DateOnly(2026, 9, 18), new DateOnly(2026, 9, 4), "Deck board cutting", new DateOnly(2026, 9, 19), 2 },
                    { 13, 5, new DateOnly(2026, 9, 24), new DateOnly(2026, 9, 10), "Molding and trim work", new DateOnly(2026, 9, 25), 10 },
                    { 14, 2, new DateOnly(2026, 9, 7), new DateOnly(2026, 8, 24), "Tree removal branch cutting", new DateOnly(2026, 9, 8), 16 },
                    { 15, 1, new DateOnly(2026, 9, 14), new DateOnly(2026, 8, 31), "Floor sanding", new DateOnly(2026, 9, 15), 17 },
                    { 16, 4, new DateOnly(2026, 9, 20), new DateOnly(2026, 9, 6), "Finishing hardwood", new DateOnly(2026, 9, 21), 18 }
                });

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 1,
                column: "JoinDate",
                value: new DateOnly(2026, 6, 30));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 2,
                column: "JoinDate",
                value: new DateOnly(2026, 7, 30));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 3,
                column: "JoinDate",
                value: new DateOnly(2026, 8, 14));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 4,
                column: "JoinDate",
                value: new DateOnly(2026, 8, 29));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 5,
                column: "JoinDate",
                value: new DateOnly(2026, 9, 8));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 6,
                column: "JoinDate",
                value: new DateOnly(2026, 9, 13));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 7,
                column: "JoinDate",
                value: new DateOnly(2026, 9, 18));

            migrationBuilder.InsertData(
                table: "OpenLoans",
                columns: new[] { "Id", "BorrowerId", "DueDate", "LoanDate", "Notes", "ToolId" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(2026, 10, 5), new DateOnly(2026, 9, 14), "Borrowed for home renovation project", 3 },
                    { 2, 2, new DateOnly(2026, 10, 9), new DateOnly(2026, 9, 18), "Needed for measuring deck", 7 },
                    { 3, 7, new DateOnly(2026, 10, 11), new DateOnly(2026, 9, 20), "Cabinet installation", 11 },
                    { 4, 4, new DateOnly(2026, 10, 14), new DateOnly(2026, 9, 23), "Basement repair work", 14 },
                    { 5, 5, new DateOnly(2026, 10, 16), new DateOnly(2026, 9, 25), "Caulking bathroom tiles", 19 },
                    { 6, 6, new DateOnly(2026, 10, 17), new DateOnly(2026, 9, 26), "Engine repair project", 8 },
                    { 7, 3, new DateOnly(2026, 10, 18), new DateOnly(2026, 9, 27), "Measuring for new flooring", 15 }
                });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateOnly(2026, 8, 29));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 3));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 8));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateAdded",
                value: new DateOnly(2026, 8, 16));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateAdded",
                value: new DateOnly(2026, 8, 30));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 20));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 14));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 23));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateAdded",
                value: new DateOnly(2026, 7, 30));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateAdded",
                value: new DateOnly(2026, 8, 9));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 16));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 9));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 18));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateAdded",
                value: new DateOnly(2026, 8, 9));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateAdded",
                value: new DateOnly(2026, 8, 19));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateAdded",
                value: new DateOnly(2026, 8, 24));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 19,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 20));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 20,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 21,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 22,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 23,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 24,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 25,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 26,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 27,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 28,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 29,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 30,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 31,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 32,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 33,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 34,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 35,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 36,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 37,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 38,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 39,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 40,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 41,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 42,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 43,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 44,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 45,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 46,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 47,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 48,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 49,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 50,
                column: "DateAdded",
                value: new DateOnly(2026, 9, 28));

            migrationBuilder.CreateIndex(
                name: "IX_ClosedLoans_BorrowerId",
                table: "ClosedLoans",
                column: "BorrowerId");

            migrationBuilder.CreateIndex(
                name: "IX_ClosedLoans_ToolId",
                table: "ClosedLoans",
                column: "ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenLoans_BorrowerId",
                table: "OpenLoans",
                column: "BorrowerId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenLoans_ToolId",
                table: "OpenLoans",
                column: "ToolId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClosedLoans");

            migrationBuilder.DropTable(
                name: "OpenLoans");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Tools",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorrowerId = table.Column<int>(type: "int", nullable: false),
                    ToolId = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    LoanDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ReturnDate = table.Column<DateOnly>(type: "date", nullable: true)
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

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 1,
                column: "JoinDate",
                value: new DateOnly(2026, 6, 24));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 2,
                column: "JoinDate",
                value: new DateOnly(2026, 7, 24));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 3,
                column: "JoinDate",
                value: new DateOnly(2026, 8, 8));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 4,
                column: "JoinDate",
                value: new DateOnly(2026, 8, 23));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 5,
                column: "JoinDate",
                value: new DateOnly(2026, 9, 2));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 6,
                column: "JoinDate",
                value: new DateOnly(2026, 9, 7));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 7,
                column: "JoinDate",
                value: new DateOnly(2026, 9, 12));

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 8, 23), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 8, 28), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 2), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 7), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 12), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 14), false });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 15), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 17), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 19), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 20), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 21), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), false });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 15,
                column: "IsAvailable",
                value: true);

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), false });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), false });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateAdded", "IsAvailable" },
                values: new object[] { new DateOnly(2026, 9, 22), true });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BorrowerId",
                table: "Loans",
                column: "BorrowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_ToolId",
                table: "Loans",
                column: "ToolId");
        }
    }
}
