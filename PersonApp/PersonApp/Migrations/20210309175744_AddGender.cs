using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonApp.Migrations
{
    public partial class AddGender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Person");

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "Person",
                type: "int",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenderName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_GenderId",
                table: "Person",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Gender_GenderId",
                table: "Person",
                column: "GenderId",
                principalTable: "Gender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Gender_GenderId",
                table: "Person");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropIndex(
                name: "IX_Person_GenderId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "Person");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Person",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
