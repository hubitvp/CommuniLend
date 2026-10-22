using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommuniLend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Tools",
                comment: "Tool that a member shares with the community");

            migrationBuilder.AlterTable(
                name: "OpenLoans",
                comment: "Active loan of a tool that has not been returned yet");

            migrationBuilder.AlterTable(
                name: "ClosedLoans",
                comment: "Loan history record created when a tool is returned");

            migrationBuilder.AlterTable(
                name: "Categories",
                comment: "Category that groups tools, e.g. Power Tools");

            migrationBuilder.AlterTable(
                name: "AspNetUsers",
                comment: "Registered member of the tool library (extends the Identity user)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ReplacementValue",
                table: "Tools",
                type: "decimal(8,2)",
                nullable: false,
                comment: "Replacement value of the tool in EUR",
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Tools",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                comment: "Foreign key to the member who owns the tool",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tools",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "Name of the tool",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Tools",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true,
                comment: "Optional relative URL of the tool image",
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tools",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "Description of the tool",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateAdded",
                table: "Tools",
                type: "date",
                nullable: false,
                comment: "Date the tool was added to the library",
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "Condition",
                table: "Tools",
                type: "int",
                nullable: false,
                comment: "Physical condition of the tool (ToolCondition enum value)",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Tools",
                type: "int",
                nullable: false,
                comment: "Foreign key to the category of the tool",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Tools",
                type: "int",
                nullable: false,
                comment: "Primary key of the tool",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ToolId",
                table: "OpenLoans",
                type: "int",
                nullable: false,
                comment: "Foreign key to the loaned tool",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "OpenLoans",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                comment: "Foreign key to the member who owns the tool (lender)",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "OpenLoans",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                comment: "Optional notes about the loan",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "LoanDate",
                table: "OpenLoans",
                type: "date",
                nullable: false,
                comment: "Date the tool is handed over to the borrower",
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DueDate",
                table: "OpenLoans",
                type: "date",
                nullable: false,
                comment: "Date the tool is expected to be returned",
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "BorrowerId",
                table: "OpenLoans",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                comment: "Foreign key to the member who borrows the tool",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "OpenLoans",
                type: "int",
                nullable: false,
                comment: "Primary key of the open loan",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ToolId",
                table: "ClosedLoans",
                type: "int",
                nullable: false,
                comment: "Foreign key to the loaned tool",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ReturnDate",
                table: "ClosedLoans",
                type: "date",
                nullable: false,
                comment: "Date the tool was actually returned",
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "ClosedLoans",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                comment: "Foreign key to the member who owns the tool (lender)",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "ClosedLoans",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                comment: "Optional notes about the loan",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "LoanDate",
                table: "ClosedLoans",
                type: "date",
                nullable: false,
                comment: "Date the tool is handed over to the borrower",
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DueDate",
                table: "ClosedLoans",
                type: "date",
                nullable: false,
                comment: "Date the tool is expected to be returned",
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "BorrowerId",
                table: "ClosedLoans",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                comment: "Foreign key to the member who borrows the tool",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ClosedLoans",
                type: "int",
                nullable: false,
                comment: "Primary key of the closed loan",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "Name of the category",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "Optional description of the category",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Categories",
                type: "int",
                nullable: false,
                comment: "Primary key of the category",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "JoinDate",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                comment: "Date the member joined",
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                comment: "Whether the member is active; inactive members' tools are hidden and they cannot borrow",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "Full name of the member",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "Optional home address of the member",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Tools",
                oldComment: "Tool that a member shares with the community");

            migrationBuilder.AlterTable(
                name: "OpenLoans",
                oldComment: "Active loan of a tool that has not been returned yet");

            migrationBuilder.AlterTable(
                name: "ClosedLoans",
                oldComment: "Loan history record created when a tool is returned");

            migrationBuilder.AlterTable(
                name: "Categories",
                oldComment: "Category that groups tools, e.g. Power Tools");

            migrationBuilder.AlterTable(
                name: "AspNetUsers",
                oldComment: "Registered member of the tool library (extends the Identity user)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ReplacementValue",
                table: "Tools",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldComment: "Replacement value of the tool in EUR");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Tools",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldComment: "Foreign key to the member who owns the tool");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tools",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "Name of the tool");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Tools",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512,
                oldNullable: true,
                oldComment: "Optional relative URL of the tool image");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tools",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "Description of the tool");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateAdded",
                table: "Tools",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldComment: "Date the tool was added to the library");

            migrationBuilder.AlterColumn<int>(
                name: "Condition",
                table: "Tools",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Physical condition of the tool (ToolCondition enum value)");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Tools",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Foreign key to the category of the tool");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Tools",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Primary key of the tool")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ToolId",
                table: "OpenLoans",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Foreign key to the loaned tool");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "OpenLoans",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldComment: "Foreign key to the member who owns the tool (lender)");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "OpenLoans",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "Optional notes about the loan");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "LoanDate",
                table: "OpenLoans",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldComment: "Date the tool is handed over to the borrower");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DueDate",
                table: "OpenLoans",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldComment: "Date the tool is expected to be returned");

            migrationBuilder.AlterColumn<string>(
                name: "BorrowerId",
                table: "OpenLoans",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldComment: "Foreign key to the member who borrows the tool");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "OpenLoans",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Primary key of the open loan")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ToolId",
                table: "ClosedLoans",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Foreign key to the loaned tool");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ReturnDate",
                table: "ClosedLoans",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldComment: "Date the tool was actually returned");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "ClosedLoans",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldComment: "Foreign key to the member who owns the tool (lender)");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "ClosedLoans",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "Optional notes about the loan");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "LoanDate",
                table: "ClosedLoans",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldComment: "Date the tool is handed over to the borrower");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DueDate",
                table: "ClosedLoans",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldComment: "Date the tool is expected to be returned");

            migrationBuilder.AlterColumn<string>(
                name: "BorrowerId",
                table: "ClosedLoans",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldComment: "Foreign key to the member who borrows the tool");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ClosedLoans",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Primary key of the closed loan")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "Name of the category");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "Optional description of the category");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Categories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Primary key of the category")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "JoinDate",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldComment: "Date the member joined");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Whether the member is active; inactive members' tools are hidden and they cannot borrow");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "Full name of the member");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "Optional home address of the member");
        }
    }
}
