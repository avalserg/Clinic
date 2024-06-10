using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientTickets.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientTickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateAppointment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HasDoctorVisit = table.Column<bool>(type: "bit", nullable: false),
                    DoctorFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorPatronymic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CabinetNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorSpeciality = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientTickets", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientTickets");
        }
    }
}
