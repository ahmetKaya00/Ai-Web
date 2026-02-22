using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Basic.Migrations
{
    /// <inheritdoc />
    public partial class ImageColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Bootcamps",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Bootcamps");
        }
    }
}
