using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Basic.Migrations
{
    /// <inheritdoc />
    public partial class InitialBootcampKayitColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BootcampKayit_BootcampId",
                table: "BootcampKayit",
                column: "BootcampId");

            migrationBuilder.CreateIndex(
                name: "IX_BootcampKayit_OgrenciId",
                table: "BootcampKayit",
                column: "OgrenciId");

            migrationBuilder.AddForeignKey(
                name: "FK_BootcampKayit_Bootcamps_BootcampId",
                table: "BootcampKayit",
                column: "BootcampId",
                principalTable: "Bootcamps",
                principalColumn: "BootcampId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BootcampKayit_Ogrenciler_OgrenciId",
                table: "BootcampKayit",
                column: "OgrenciId",
                principalTable: "Ogrenciler",
                principalColumn: "OgrenciId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BootcampKayit_Bootcamps_BootcampId",
                table: "BootcampKayit");

            migrationBuilder.DropForeignKey(
                name: "FK_BootcampKayit_Ogrenciler_OgrenciId",
                table: "BootcampKayit");

            migrationBuilder.DropIndex(
                name: "IX_BootcampKayit_BootcampId",
                table: "BootcampKayit");

            migrationBuilder.DropIndex(
                name: "IX_BootcampKayit_OgrenciId",
                table: "BootcampKayit");
        }
    }
}
