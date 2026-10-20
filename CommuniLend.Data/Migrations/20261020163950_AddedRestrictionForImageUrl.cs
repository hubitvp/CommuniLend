using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CommuniLend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedRestrictionForImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Tools",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "JoinDate",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "234a29ff-3432-5780-a6e8-3acd25056d4c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5085596c-7975-580a-aa8a-68ad83d435bb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5babe2ff-dff9-5359-8b04-f77b0028ca98");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "93295a13-2d4b-5732-9cba-916402a8edf3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b9af376a-21a5-540b-89d2-8c131633400d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f04c78b2-6180-5b82-bdb8-0eedb97675a7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fec24483-730b-58d9-9383-2c44fcf490e8");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Tools",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "JoinDate",
                table: "AspNetUsers",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

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
        }
    }
}
