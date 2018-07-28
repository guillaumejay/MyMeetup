using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyMeetUp.Logic.Migrations.SqlServerMigrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppParameters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    Title = table.Column<string>(maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppParameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                name: "CharterContents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    Category = table.Column<string>(maxLength: 80, nullable: false),
                    Content = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    MeetupId = table.Column<int>(nullable: true),
                    Position = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharterContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharterContents_Meetups_MeetupId",
                        column: x => x.MeetupId,
                        principalTable: "Meetups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MeetupAdmins",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    MeetupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetupAdmins", x => new { x.UserId, x.MeetupId });
                    table.UniqueConstraint("AK_MeetupAdmins_MeetupId_UserId", x => new { x.MeetupId, x.UserId });
                    table.ForeignKey(
                        name: "FK_MeetupAdmins_Meetups_MeetupId",
                        column: x => x.MeetupId,
                        principalTable: "Meetups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeetupAdmins_AspNetUsers_UserId",
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
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UserId = table.Column<int>(nullable: false),
                    MeetupId = table.Column<int>(nullable: false),
                    RegistrationCode = table.Column<string>(maxLength: 20, nullable: true),
                    PaidFees = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registrations_Meetups_MeetupId",
                        column: x => x.MeetupId,
                        principalTable: "Meetups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registrations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CharterContents",
                columns: new[] { "Id", "Category", "Content", "CreatedAt", "IsActive", "MeetupId", "Position", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Communication sur le respect des lieux", "Chaque membre de votre famille, présent à la rencontre, doit être informé que le respect des lieux est important pour que nous puissions revenir. Aussi merci de nous prévenir en cas d’éventuels dégâts pour montrer aux gérants notre implication dans la remise en état des lieux.", new DateTime(2018, 7, 28, 4, 50, 26, 759, DateTimeKind.Utc), true, null, 1, new DateTime(2018, 7, 28, 4, 50, 26, 759, DateTimeKind.Utc) },
                    { 2, "Animaux", "Les chiens sont tolérés, à condition qu'ils restent attachés ou auprès de vous en permanence.<br/>Ils ne doivent également pas être bruyants.", new DateTime(2018, 7, 28, 4, 50, 26, 759, DateTimeKind.Utc), true, null, 3, new DateTime(2018, 7, 28, 4, 50, 26, 759, DateTimeKind.Utc) },
                    { 3, "Participation financière", " Chaque famille participante devra régler 3€ de participation à Rencontres Nonscos : ces paiements permettront à l'association de couvrir ses dépenses d'existence (assurance notamment)", new DateTime(2018, 7, 28, 4, 50, 26, 759, DateTimeKind.Utc), true, null, 2, new DateTime(2018, 7, 28, 4, 50, 26, 759, DateTimeKind.Utc) },
                    { 4, "Alcool", "La consommation d’alcool doit être raisonnée, pour toutes les personnes participantes, quel que soit leur âge, et bien sûr, les parents ou les référents sont invités à être attentifs à cette problématique vis-à-vis des personnes dont ils sont responsables.", new DateTime(2018, 7, 28, 4, 50, 26, 759, DateTimeKind.Utc), true, null, 4, new DateTime(2018, 7, 28, 4, 50, 26, 759, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Meetups",
                columns: new[] { "Id", "CreatedAt", "EndDate", "IsVisible", "OpenForRegistrationOn", "PublicDescription", "RegisteredDescription", "StartDate", "Title", "TitleImage", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2018, 7, 28, 4, 50, 26, 758, DateTimeKind.Utc), new DateTime(2018, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2018, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), @"Rencontre près de Casteljaloux(47). Situé dans un écrin de forêt, les hébergements se répartissent entre gîtes, landettes, emplacements pour tentes et camions, et quelques yourtes.<br/><div>
<h2><u>Comment s'inscrire &agrave; la rencontre ?</u></h2>
</div>
<div>Ne peuvent s'inscrire &agrave; cette rencontre que les personnes qui s'engagent &agrave; respecter la charte mise en place.</div>
<div><strong>Proc&eacute;dure&nbsp;</strong>:</div>
<div>1. Vous lisez l'engagement que vous demande la charte</div>
<div>2. Si la charte vous convient : vous vous engagez &agrave; la respecter en la validant, la signant num&eacute;riquement et en nous donnant vos coordonn&eacute;es : le tout nous sera adress&eacute; directement.</div>", "<div><strong>Toutes les inscriptions (locatif ou camping) doivent se faire uniquement par mail <div><strong>", new DateTime(2018, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "La Taillade 2018", "La-Taillade.jpg", new DateTime(2018, 7, 28, 4, 50, 26, 758, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "CharterContents",
                columns: new[] { "Id", "Category", "Content", "CreatedAt", "IsActive", "MeetupId", "Position", "UpdatedAt" },
                values: new object[] { 5, "Spécifique à la Taillade", "La tradition est née de faire des trous autour du barbecue, il est important de les reboucher au départ des enfants", new DateTime(2018, 7, 28, 4, 50, 26, 759, DateTimeKind.Utc), true, 1, 1, new DateTime(2018, 7, 28, 4, 50, 26, 759, DateTimeKind.Utc) });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CharterContents_MeetupId",
                table: "CharterContents",
                column: "MeetupId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_MeetupId",
                table: "Registrations",
                column: "MeetupId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_UserId",
                table: "Registrations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppParameters");

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
                name: "CharterContents");

            migrationBuilder.DropTable(
                name: "MeetupAdmins");

            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Meetups");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
