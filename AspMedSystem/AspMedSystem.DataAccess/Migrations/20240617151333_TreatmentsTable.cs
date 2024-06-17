using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspMedSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TreatmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTreatments_Treatment_TreatmentId",
                table: "UserTreatments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Treatment",
                table: "Treatment");

            migrationBuilder.RenameTable(
                name: "Treatment",
                newName: "Treatments");

            migrationBuilder.RenameIndex(
                name: "IX_Treatment_Name",
                table: "Treatments",
                newName: "IX_Treatments_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Treatments",
                table: "Treatments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTreatments_Treatments_TreatmentId",
                table: "UserTreatments",
                column: "TreatmentId",
                principalTable: "Treatments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTreatments_Treatments_TreatmentId",
                table: "UserTreatments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Treatments",
                table: "Treatments");

            migrationBuilder.RenameTable(
                name: "Treatments",
                newName: "Treatment");

            migrationBuilder.RenameIndex(
                name: "IX_Treatments_Name",
                table: "Treatment",
                newName: "IX_Treatment_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Treatment",
                table: "Treatment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTreatments_Treatment_TreatmentId",
                table: "UserTreatments",
                column: "TreatmentId",
                principalTable: "Treatment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
