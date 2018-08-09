using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyMeetUp.Logic.Migrations.SqlLiteMigrations
{
    public partial class AddingRegistrationInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Registrations",
                maxLength: 250,
                nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "ReferentUserId",
            //    table: "Registrations",
            //    nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegistrationStatus",
                table: "Registrations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(
                "alter table Registrations add column ReferentUserId integer REFERENCES AspNetUsers(Id)");
           

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_ReferentUserId",
                table: "Registrations",
                column: "ReferentUserId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Registrations_AspNetUsers_ReferentUserId",
            //    table: "Registrations",
            //    column: "ReferentUserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_AspNetUsers_ReferentUserId",
                table: "Registrations");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_ReferentUserId",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "ReferentUserId",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "RegistrationStatus",
                table: "Registrations");

          
        }
    }
}
