using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommuniLend.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImageUrlAsIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tools_ImageUrl",
                table: "Tools",
                column: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tools_ImageUrl",
                table: "Tools");
        }
    }
}
