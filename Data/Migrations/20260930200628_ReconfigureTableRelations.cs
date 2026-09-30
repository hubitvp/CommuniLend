using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommuniLend.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReconfigureTableRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClosedLoans_Members_BorrowerId",
                table: "ClosedLoans");

            migrationBuilder.DropForeignKey(
                name: "FK_OpenLoans_Members_BorrowerId",
                table: "OpenLoans");

            migrationBuilder.AddForeignKey(
                name: "FK_ClosedLoans_Members_BorrowerId",
                table: "ClosedLoans",
                column: "BorrowerId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpenLoans_Members_BorrowerId",
                table: "OpenLoans",
                column: "BorrowerId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClosedLoans_Members_BorrowerId",
                table: "ClosedLoans");

            migrationBuilder.DropForeignKey(
                name: "FK_OpenLoans_Members_BorrowerId",
                table: "OpenLoans");

            migrationBuilder.AddForeignKey(
                name: "FK_ClosedLoans_Members_BorrowerId",
                table: "ClosedLoans",
                column: "BorrowerId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OpenLoans_Members_BorrowerId",
                table: "OpenLoans",
                column: "BorrowerId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
