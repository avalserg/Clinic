using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ManageUsers.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.ApplicationUserId);
                });

            migrationBuilder.CreateTable(
                name: "Administrators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administrators_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "ApplicationUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateBirthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    CabinetNumber = table.Column<int>(type: "int", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "ApplicationUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateBirthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "ApplicationUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "ApplicationUserId", "ApplicationUserRole", "Login", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("0f8fad5b-d9cb-469f-a165-70867728950a"), 1, "Admin", "$MYHASH$V1$10000$+X4Aw24Ud2+zdOsZVfe7S8tvhB2v4gKHMSrUFhWWVO8yZoSv" },
                    { new Guid("0f8fad5b-d9cb-469f-a165-70867728950b"), 2, "Patient1", "$MYHASH$V1$10000$+X4Aw24Ud2+zdOsZVfe7S8tvhB2v4gKHMSrUFhWWVO8yZoSv" },
                    { new Guid("0f8fad5b-d9cb-469f-a165-70867728950d"), 3, "Doctor1", "$MYHASH$V1$10000$+X4Aw24Ud2+zdOsZVfe7S8tvhB2v4gKHMSrUFhWWVO8yZoSv" }
                });

            migrationBuilder.InsertData(
                table: "Administrators",
                columns: new[] { "Id", "ApplicationUserId", "FirstName", "LastName", "Patronymic" },
                values: new object[] { 1, new Guid("0f8fad5b-d9cb-469f-a165-70867728950a"), "Admin", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Address", "ApplicationUserId", "CabinetNumber", "Category", "DateBirthday", "Experience", "FirstName", "LastName", "Patronymic", "Phone", "Photo" },
                values: new object[] { 3, "Doctor1Address", new Guid("0f8fad5b-d9cb-469f-a165-70867728950d"), 1, "High", new DateTime(2024, 4, 20, 18, 21, 6, 472, DateTimeKind.Local).AddTicks(5705), 1, "Doctor1FirstName", "Doctor1LastName", "Doctor1Patronymic", "Doctor1Phone", null });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "ApplicationUserId", "Avatar", "DateBirthday", "FirstName", "LastName", "Patronymic", "Phone" },
                values: new object[] { 2, "Patient1Address", new Guid("0f8fad5b-d9cb-469f-a165-70867728950b"), "", new DateTime(2024, 4, 20, 18, 21, 6, 472, DateTimeKind.Local).AddTicks(6614), "Patient1FirstName", "Patient1LastName", "Patient1Patronymic", "Patient1Phone" });

            migrationBuilder.CreateIndex(
                name: "IX_Administrators_ApplicationUserId",
                table: "Administrators",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_ApplicationUserId",
                table: "Doctors",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ApplicationUserId",
                table: "Patients",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrators");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");
        }
    }
}
