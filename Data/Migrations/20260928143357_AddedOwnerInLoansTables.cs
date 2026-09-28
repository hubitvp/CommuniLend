using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommuniLend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedOwnerInLoansTables : Migration
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

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "OpenLoans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "ClosedLoans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ClosedLoans",
                keyColumn: "Id",
                keyValue: 8,
                column: "OwnerId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ClosedLoans",
                keyColumn: "Id",
                keyValue: 9,
                column: "OwnerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ClosedLoans",
                keyColumn: "Id",
                keyValue: 10,
                column: "OwnerId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ClosedLoans",
                keyColumn: "Id",
                keyValue: 11,
                column: "OwnerId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "ClosedLoans",
                keyColumn: "Id",
                keyValue: 12,
                column: "OwnerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ClosedLoans",
                keyColumn: "Id",
                keyValue: 13,
                column: "OwnerId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ClosedLoans",
                keyColumn: "Id",
                keyValue: 14,
                column: "OwnerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ClosedLoans",
                keyColumn: "Id",
                keyValue: 15,
                column: "OwnerId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ClosedLoans",
                keyColumn: "Id",
                keyValue: 16,
                column: "OwnerId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "OpenLoans",
                keyColumn: "Id",
                keyValue: 1,
                column: "OwnerId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "OpenLoans",
                keyColumn: "Id",
                keyValue: 2,
                column: "OwnerId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "OpenLoans",
                keyColumn: "Id",
                keyValue: 3,
                column: "OwnerId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "OpenLoans",
                keyColumn: "Id",
                keyValue: 4,
                column: "OwnerId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "OpenLoans",
                keyColumn: "Id",
                keyValue: 5,
                column: "OwnerId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "OpenLoans",
                keyColumn: "Id",
                keyValue: 6,
                column: "OwnerId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "OpenLoans",
                keyColumn: "Id",
                keyValue: 7,
                column: "OwnerId",
                value: 7);

            migrationBuilder.CreateIndex(
                name: "IX_OpenLoans_OwnerId",
                table: "OpenLoans",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ClosedLoans_OwnerId",
                table: "ClosedLoans",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClosedLoans_Members_BorrowerId",
                table: "ClosedLoans",
                column: "BorrowerId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClosedLoans_Members_OwnerId",
                table: "ClosedLoans",
                column: "OwnerId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpenLoans_Members_BorrowerId",
                table: "OpenLoans",
                column: "BorrowerId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OpenLoans_Members_OwnerId",
                table: "OpenLoans",
                column: "OwnerId",
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
                name: "FK_ClosedLoans_Members_OwnerId",
                table: "ClosedLoans");

            migrationBuilder.DropForeignKey(
                name: "FK_OpenLoans_Members_BorrowerId",
                table: "OpenLoans");

            migrationBuilder.DropForeignKey(
                name: "FK_OpenLoans_Members_OwnerId",
                table: "OpenLoans");

            migrationBuilder.DropIndex(
                name: "IX_OpenLoans_OwnerId",
                table: "OpenLoans");

            migrationBuilder.DropIndex(
                name: "IX_ClosedLoans_OwnerId",
                table: "ClosedLoans");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "OpenLoans");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ClosedLoans");

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
    }
}
