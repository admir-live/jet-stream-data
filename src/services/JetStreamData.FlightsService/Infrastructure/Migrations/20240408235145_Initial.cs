using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JetStreamData.FlightsService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FlightInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Flight_Number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Flight_Status = table.Column<int>(type: "int", nullable: true),
                    Flight_CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Flight_ModifiedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Flight_Id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DepartureAirport_Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartureAirport_IataCode = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartureAirport_IcaoCode = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartureAirport_Timezone = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartureAirport_CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DepartureAirport_ModifiedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ArrivalAirport_Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ArrivalAirport_IataCode = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ArrivalAirport_IcaoCode = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ArrivalAirport_Timezone = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ArrivalAirport_CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ArrivalAirport_ModifiedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Airline_Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Airline_Iata = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Airline_Icao = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Airline_CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Airline_ModifiedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DepartureSchedule_Scheduled = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DepartureSchedule_Estimated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DepartureSchedule_Actual = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DepartureSchedule_CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DepartureSchedule_ModifiedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ArrivalSchedule_Scheduled = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ArrivalSchedule_Estimated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ArrivalSchedule_Actual = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ArrivalSchedule_CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ArrivalSchedule_ModifiedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RowVersion = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightInformation", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightInformation");
        }
    }
}
