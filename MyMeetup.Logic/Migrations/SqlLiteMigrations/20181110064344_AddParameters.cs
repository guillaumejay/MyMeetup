using Microsoft.EntityFrameworkCore.Migrations;

namespace MyMeetUp.Logic.Migrations.SqlLiteMigrations
{
    public partial class AddParameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HomeContent",
                table: "AppParameters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeImage",
                table: "AppParameters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeTitle",
                table: "AppParameters",
                maxLength: 120,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomeContent",
                table: "AppParameters");

            migrationBuilder.DropColumn(
                name: "HomeImage",
                table: "AppParameters");

            migrationBuilder.DropColumn(
                name: "HomeTitle",
                table: "AppParameters");
        }
    }
}
