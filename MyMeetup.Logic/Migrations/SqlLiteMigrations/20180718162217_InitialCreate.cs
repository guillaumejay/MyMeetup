using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyMeetUp.Logic.Migrations.SqlLiteMigrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppParameter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppParameter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    AccessFailedCount = table.Column<int>(nullable: false),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 60, nullable: false),
                    LastName = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meetups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    Title = table.Column<string>(maxLength: 80, nullable: false),
                    StartDate = table.Column<DateTime>(type: "Date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "Date", nullable: false),
                    IsVisible = table.Column<bool>(nullable: false),
                    OpenForRegistrationOn = table.Column<DateTime>(nullable: true),
                    PublicDescription = table.Column<string>(nullable: false),
                    RegisteredDescription = table.Column<string>(nullable: false),
                    TitleImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContenusChartes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    Category = table.Column<string>(maxLength: 80, nullable: true),
                    Content = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    MeetupId = table.Column<int>(nullable: true),
                    Position = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContenusChartes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContenusChartes_Meetups_MeetupId",
                        column: x => x.MeetupId,
                        principalTable: "Meetups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MeetupAdmin",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    MeetupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetupAdmin", x => new { x.UserId, x.MeetupId });
                    table.UniqueConstraint("AK_MeetupAdmin_MeetupId_UserId", x => new { x.MeetupId, x.UserId });
                    table.ForeignKey(
                        name: "FK_MeetupAdmin_Meetups_MeetupId",
                        column: x => x.MeetupId,
                        principalTable: "Meetups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeetupAdmin_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    RencontreUserId = table.Column<int>(nullable: false),
                    MyMeetupUserId = table.Column<int>(nullable: true),
                    RencontreId = table.Column<int>(nullable: false),
                    RegistrationCode = table.Column<string>(maxLength: 20, nullable: true),
                    PaidFees = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registrations_AspNetUsers_MyMeetupUserId",
                        column: x => x.MyMeetupUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registrations_Meetups_RencontreId",
                        column: x => x.RencontreId,
                        principalTable: "Meetups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppParameter",
                columns: new[] { "Id", "CreatedAt", "Title", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2018, 7, 18, 16, 22, 17, 206, DateTimeKind.Utc), "Rencontres Non Scolarisees", new DateTime(2018, 7, 18, 16, 22, 17, 206, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "ContenusChartes",
                columns: new[] { "Id", "Category", "Content", "CreatedAt", "IsActive", "MeetupId", "Position", "UpdatedAt" },
                values: new object[] { 1, "Animaux", "<ul><li>Les chiens sont tolérés, à condition qu'ils restent attachés ou auprès de vous en permanence.</li><li>Ils ne doivent également pas être bruyants.</li></ul>", new DateTime(2018, 7, 18, 16, 22, 17, 205, DateTimeKind.Utc), true, null, 1, new DateTime(2018, 7, 18, 16, 22, 17, 205, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "ContenusChartes",
                columns: new[] { "Id", "Category", "Content", "CreatedAt", "IsActive", "MeetupId", "Position", "UpdatedAt" },
                values: new object[] { 2, "Alcool", "<ul><li>La consommation d’alcool doit être raisonnée, pour toutes les personnes participantes, quel que soit leur âge, et bien sûr, les parents ou les référents sont invités à être attentifs à cette problématique vis-à-vis des personnes dont ils sont responsables.</li></ul>", new DateTime(2018, 7, 18, 16, 22, 17, 205, DateTimeKind.Utc), true, null, 2, new DateTime(2018, 7, 18, 16, 22, 17, 205, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Meetups",
                columns: new[] { "Id", "CreatedAt", "EndDate", "IsVisible", "OpenForRegistrationOn", "PublicDescription", "RegisteredDescription", "StartDate", "Title", "TitleImage", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2018, 7, 18, 16, 22, 17, 204, DateTimeKind.Utc), new DateTime(2018, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2018, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rencontre près de Casteljaloux(47) du 22 au 29 octobre 2018. Situé dans un écrin de forêt, les hébergements se répartissent entre gîtes, landettes, emplacements pour tentes et camions, et quelques yourtes.", @"<div><strong>Toutes les inscriptions (locatif ou camping) doivent se faire uniquement par mail &agrave; : francois.fonseca@solincite.org</strong></div>
<div>Vous devrez lui indiquer vos&nbsp;<strong>noms/pr&eacute;noms/adresse postale/nb d&rsquo;adultes+d&rsquo;enfants.</strong></div>
<div><strong>Il n&rsquo;y a qu&rsquo;un seul interlocuteur par logement : un logement est r&eacute;serv&eacute; par une seule famille, c&rsquo;est elle qui fait la r&eacute;servation et paiera la somme totale au village de vacances. </strong>Vous pouvez donc r&eacute;server &agrave; votre nom et trouver d&rsquo;autres familles pour partager, gr&acirc;ce au document Pad mis &agrave; disposition <strong>:&nbsp;</strong><strong><a href=""https://semestriel.framapad.org/p/LaTaillade_qfmnV6VC4B"">https://semestriel.framapad.org/p/LaTaillade_qfmnV6VC4B</a></strong></div>
<div>Ce document servira &agrave; partager toutes les infos sur la rencontre (logements, covoiturage, activit&eacute;s,&hellip;)</div>", new DateTime(2018, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "La Taillade 2018", "La-Taillade.jpg", new DateTime(2018, 7, 18, 16, 22, 17, 204, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Meetups",
                columns: new[] { "Id", "CreatedAt", "EndDate", "IsVisible", "OpenForRegistrationOn", "PublicDescription", "RegisteredDescription", "StartDate", "Title", "TitleImage", "UpdatedAt" },
                values: new object[] { 2, new DateTime(2018, 7, 18, 16, 22, 17, 205, DateTimeKind.Utc), new DateTime(2019, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Rencontre près de Casteljaloux(47) du 22 au 29 octobre 2018. Situé dans un écrin de forêt, les hébergements se répartissent entre gîtes, landettes, emplacements pour tentes et camions, et quelques yourtes.", @"<div><strong>Toutes les inscriptions (locatif ou camping) doivent se faire uniquement par mail &agrave; : francois.fonseca@solincite.org</strong></div>
<div>Vous devrez lui indiquer vos&nbsp;<strong>noms/pr&eacute;noms/adresse postale/nb d&rsquo;adultes+d&rsquo;enfants.</strong></div>
<div><strong>Il n&rsquo;y a qu&rsquo;un seul interlocuteur par logement : un logement est r&eacute;serv&eacute; par une seule famille, c&rsquo;est elle qui fait la r&eacute;servation et paiera la somme totale au village de vacances. </strong>Vous pouvez donc r&eacute;server &agrave; votre nom et trouver d&rsquo;autres familles pour partager, gr&acirc;ce au document Pad mis &agrave; disposition <strong>:&nbsp;</strong><strong><a href=""https://semestriel.framapad.org/p/LaTaillade_qfmnV6VC4B"">https://semestriel.framapad.org/p/LaTaillade_qfmnV6VC4B</a></strong></div>
<div>Ce document servira &agrave; partager toutes les infos sur la rencontre (logements, covoiturage, activit&eacute;s,&hellip;)</div>", new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "La Taillade Printemps 2019", "La-Taillade.jpg", new DateTime(2018, 7, 18, 16, 22, 17, 205, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "ContenusChartes",
                columns: new[] { "Id", "Category", "Content", "CreatedAt", "IsActive", "MeetupId", "Position", "UpdatedAt" },
                values: new object[] { 3, "Spécifique à la Taillade", "<ul><li>La tradition est née de faire des trous autour du barbecue, il est important de les reboucher au départ des enfants</li></ul>", new DateTime(2018, 7, 18, 16, 22, 17, 205, DateTimeKind.Utc), true, 1, 1, new DateTime(2018, 7, 18, 16, 22, 17, 205, DateTimeKind.Utc) });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContenusChartes_MeetupId",
                table: "ContenusChartes",
                column: "MeetupId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_MyMeetupUserId",
                table: "Registrations",
                column: "MyMeetupUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_RencontreId",
                table: "Registrations",
                column: "RencontreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppParameter");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ContenusChartes");

            migrationBuilder.DropTable(
                name: "MeetupAdmin");

            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Meetups");
        }
    }
}
