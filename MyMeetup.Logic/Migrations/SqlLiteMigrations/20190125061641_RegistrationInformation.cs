using Microsoft.EntityFrameworkCore.Migrations;

namespace MyMeetUp.Logic.Migrations.SqlLiteMigrations
{
    public partial class RegistrationInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccomodationId",
                table: "Registrations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAdults",
                table: "Registrations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfChildren",
                table: "Registrations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccomodationId",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "NumberOfAdults",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "NumberOfChildren",
                table: "Registrations");
        }
    }
}
