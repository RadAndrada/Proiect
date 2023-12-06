using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proiect.Migrations
{
    /// <inheritdoc />
    public partial class Payment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Category_CategoryID",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Event_CategoryID",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Event",
                newName: "PaymentID");

            migrationBuilder.CreateTable(
                name: "CategoryEvent",
                columns: table => new
                {
                    CategoriesID = table.Column<int>(type: "int", nullable: false),
                    EventsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEvent", x => new { x.CategoriesID, x.EventsID });
                    table.ForeignKey(
                        name: "FK_CategoryEvent_Category_CategoriesID",
                        column: x => x.CategoriesID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryEvent_Event_EventsID",
                        column: x => x.EventsID,
                        principalTable: "Event",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipantID = table.Column<int>(type: "int", nullable: true),
                    EventID = table.Column<int>(type: "int", nullable: true),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Payment_Event_EventID",
                        column: x => x.EventID,
                        principalTable: "Event",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Payment_Participant_ParticipantID",
                        column: x => x.ParticipantID,
                        principalTable: "Participant",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEvent_EventsID",
                table: "CategoryEvent",
                column: "EventsID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_EventID",
                table: "Payment",
                column: "EventID",
                unique: true,
                filter: "[EventID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_ParticipantID",
                table: "Payment",
                column: "ParticipantID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryEvent");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Participant");

            migrationBuilder.RenameColumn(
                name: "PaymentID",
                table: "Event",
                newName: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Event_CategoryID",
                table: "Event",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Category_CategoryID",
                table: "Event",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "ID");
        }
    }
}
