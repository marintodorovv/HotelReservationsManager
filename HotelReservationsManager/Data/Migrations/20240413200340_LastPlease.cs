using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservationsManager.Data.Migrations
{
    public partial class LastPlease : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Reservations_ReservationID",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_ReservationID",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ReservationID",
                table: "Clients");

            migrationBuilder.CreateTable(
                name: "ClientReservation",
                columns: table => new
                {
                    ClientsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientReservation", x => new { x.ClientsID, x.ReservationsID });
                    table.ForeignKey(
                        name: "FK_ClientReservation_Clients_ClientsID",
                        column: x => x.ClientsID,
                        principalTable: "Clients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientReservation_Reservations_ReservationsID",
                        column: x => x.ReservationsID,
                        principalTable: "Reservations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientReservation_ReservationsID",
                table: "ClientReservation",
                column: "ReservationsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientReservation");

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationID",
                table: "Clients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ReservationID",
                table: "Clients",
                column: "ReservationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Reservations_ReservationID",
                table: "Clients",
                column: "ReservationID",
                principalTable: "Reservations",
                principalColumn: "ID");
        }
    }
}
