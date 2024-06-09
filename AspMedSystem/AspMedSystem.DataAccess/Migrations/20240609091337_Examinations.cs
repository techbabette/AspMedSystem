using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspMedSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Examinations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExaminationTerms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExaminerId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationTerms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminationTerms_Users_ExaminerId",
                        column: x => x.ExaminerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Examinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExaminationTermId = table.Column<int>(type: "int", nullable: false),
                    ExamineeId = table.Column<int>(type: "int", nullable: false),
                    Canceled = table.Column<bool>(type: "bit", nullable: false),
                    Perfomed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examinations_ExaminationTerms_ExaminationTermId",
                        column: x => x.ExaminationTermId,
                        principalTable: "ExaminationTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examinations_Users_ExamineeId",
                        column: x => x.ExamineeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_ExaminationTermId",
                table: "Examinations",
                column: "ExaminationTermId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_ExamineeId",
                table: "Examinations",
                column: "ExamineeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationTerms_ExaminerId",
                table: "ExaminationTerms",
                column: "ExaminerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Examinations");

            migrationBuilder.DropTable(
                name: "ExaminationTerms");
        }
    }
}
