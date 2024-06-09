using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspMedSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Counterindications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TreatmentCounterindications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TreatmentId = table.Column<int>(type: "int", nullable: false),
                    CounterIndicatedTreatmentId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentCounterindications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentCounterindications_Treatment_CounterIndicatedTreatmentId",
                        column: x => x.CounterIndicatedTreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TreatmentCounterindications_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentCounterindications_CounterIndicatedTreatmentId",
                table: "TreatmentCounterindications",
                column: "CounterIndicatedTreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentCounterindications_TreatmentId",
                table: "TreatmentCounterindications",
                column: "TreatmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TreatmentCounterindications");
        }
    }
}
